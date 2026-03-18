using UnityEngine;
using Photon.Pun;


public class Staff : MonoBehaviour
{
    [SerializeField] private BulletInfo info;
    [SerializeField] private Animator animator;
    private PhotonView pv;
    private bool hitCounter = false;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;
        if (other.CompareTag("Player") && animator.GetCurrentAnimatorStateInfo(0).IsName("MeeleAttack") && !hitCounter)
        {
            bool isMineTeam = pv.Owner.CustomProperties.TryGetValue("Team", out object mineTeam);
            bool isOtherTeam = other.GetComponentInParent<PhotonView>().Owner.CustomProperties.TryGetValue("Team", out object otherTeam);
//            print($"{mineTeam.ToString()},{otherTeam.ToString()}");
            if (isMineTeam && isOtherTeam)
                if (mineTeam.ToString() != otherTeam.ToString())
                {
                    other.GetComponentInParent<PlayerSetting>().TakeDamage(info.damage);
                    hitCounter = true;
                    Invoke(nameof(ResetAttack), 1f);
                }
        }
    }
    private void ResetAttack()
    {
        hitCounter = false;
    }

}
