using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using Photon.Pun;

public class Aim : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private List<GameObject> allTargets;
    [SerializeField] private GameObject targetCylinder;
    [SerializeField] private float range;
    private InputController inputs;
    private PhotonView pv;
    private CharacterController controller;
    private GameObject targetObj;
    private bool canSearch = true;
    private int targetCount;

    private Animator animator;
    private bool isAttack;
    public Coroutine attackCoroutine;

    [Header("Test mode")]
    [SerializeField] private SwitchAttack modeAttack;

    private void Awake()
    {
        inputs = new InputController();
        controller = GetComponent<CharacterController>();
        pv = GetComponentInParent<PhotonView>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputs.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        inputs.CharacterControls.Disable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
  
    private void Start()
    {
        if (!pv.IsMine) return;
        targetCylinder.SetActive(false);

        inputs.CharacterControls.ChangeTarget.started += SelectNewTarget;
        inputs.CharacterControls.Fire.started += OnFire;
    }

    private void FixedUpdate()
    {
        if (!pv.IsMine) return;
        SelectTarget();
        PlayerRotateOnAttack();
    }
    public void SetTargetStatus(bool isTarget)
    {
        targetCylinder.SetActive(isTarget);
    }

    private void SelectTarget()
    {
        if(controller.velocity == Vector3.zero)
        {
            if (canSearch)
                InvokeRepeating(nameof(DistToTarget), 0f, 0.5f);
        }
        else
        {
            if(targetObj != null)
            {
            targetObj.GetComponent<Aim>().SetTargetStatus(false);
                targetObj = null;
            }
            canSearch = true;
            CancelInvoke();
        }
    }
    private void DistToTarget()
    {
        canSearch = false;
        allTargets.Clear();

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, range, transform.position, range);
        foreach(RaycastHit hit in hits)
        {
            GameObject tempObj = hit.collider.gameObject;
            if (tempObj.TryGetComponent<CharacterController>(out CharacterController cntrl)
                && !tempObj.GetComponentInParent<PhotonView>().IsMine) // added
            {

                bool isMineTeam = pv.Owner.CustomProperties.TryGetValue("Team", out object mineTeam);
                bool isOtherTeam = tempObj.GetComponentInParent<PhotonView>().Owner.CustomProperties.TryGetValue("Team", out object otherTeam);
                if (isMineTeam && isOtherTeam)
                    if (mineTeam.ToString() != otherTeam.ToString())
                        allTargets.Add(tempObj);
            }
            else continue;
        }
        SelectNewTarget();
    }
    private void SelectNewTarget()
    {
        foreach(GameObject target in allTargets)
        {
            
            target.GetComponent<Aim>().SetTargetStatus(false);
        }
        if(targetCount >= allTargets.Count)
        {
            targetCount = 0;
        }
        if(allTargets.Count > 0)
        {
            targetObj = allTargets[targetCount];
           targetObj.GetComponent<Aim>().SetTargetStatus(true);
        }
    }
    private void SelectNewTarget(InputAction.CallbackContext context)
    {
        targetCount++;
        foreach (GameObject target in allTargets)
        {
            target.GetComponent<Aim>().SetTargetStatus(false);
        }
        if (targetCount >= allTargets.Count)
        {
            targetCount = 0;
        }
        if (allTargets.Count > 0)
        {
            targetObj = allTargets[targetCount];
          targetObj.GetComponent<Aim>().SetTargetStatus(true);
        }
    }

    public IEnumerator ProceedAttack()
    {
        isAttack = true;
        animator.SetTrigger("isRangeAttack");
        yield return new WaitForSeconds(0.8f);
        isAttack = false;
        if (targetObj != null) {
            Vector3 dir = targetObj.transform.position - transform.position;
            GameObject temp = PhotonNetwork.Instantiate(
                                                        Path.Combine("CloudBall"),
                                                        bulletSpawnPos.position,
                                                        Quaternion.identity);
            temp.GetComponent<Bullet>().StartMove(dir, targetObj);
            temp.transform.LookAt(targetObj.transform);
        }
    }

    private void RangeAttack()
    {
        Vector3 dir = targetObj.transform.position - transform.position;
        GameObject temp = PhotonNetwork.Instantiate(
                                                    Path.Combine("CloudBall"),
                                                    bulletSpawnPos.position,
                                                    Quaternion.identity);
        temp.GetComponent<Bullet>().StartMove(dir, targetObj);
        temp.transform.LookAt(targetObj.transform);
    }

    public IEnumerator ProceedMeeleAttack()
    {
        isAttack = true;
        animator.SetTrigger("isMeleeAttack");
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
       
        if(targetObj != null && !isAttack && !modeAttack.isAttackMeele)
        {
            StartCoroutine(ProceedAttack());
        }
        else if(targetObj != null && !isAttack && modeAttack.isAttackMeele)
        {
            StartCoroutine(ProceedMeeleAttack());
        }
    }

    private void PlayerRotateOnAttack()
    {
        if(isAttack && targetObj != null)
        {
            Vector3 dir = targetObj.transform.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            Quaternion rotateDir = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 10f);
            transform.rotation = rotateDir;
        }
    }
}
