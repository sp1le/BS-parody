using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private CameraFollow playerCam;
    private InputController inputActions;
    private CharacterController controller;
    private Animator animator;
    private Vector2 moveInput;
    private Vector3 moveDir;
    private Quaternion rotateDir;
    private bool isRun, isWalk;
    private PhotonView pv;


    private void Awake()
    {
        pv = GetComponentInParent<PhotonView>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputActions = new InputController();
        inputActions.CharacterControls.Movement.started += OnMovementActions;
        inputActions.CharacterControls.Movement.performed += OnMovementActions;
        inputActions.CharacterControls.Movement.canceled += OnMovementActions;

        inputActions.CharacterControls.Movement.started += OnCameraMove;
        inputActions.CharacterControls.Movement.performed += OnCameraMove;
        inputActions.CharacterControls.Movement.canceled += OnCameraMove;

        inputActions.CharacterControls.Run.started += OnRun;
        inputActions.CharacterControls.Run.canceled += OnRun;



        if (!pv.IsMine)
        {
            Destroy(playerCam.gameObject);
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        
    }



    private void OnCameraMove(InputAction.CallbackContext context)
    {
        playerCam.SetOffset(moveDir);
    }
    private void OnMovementActions(InputAction.CallbackContext context)
    {
        if (GetComponent<Aim>().attackCoroutine != null)
        {
            StopCoroutine(GetComponent<Aim>().attackCoroutine);
        }
        moveInput = context.ReadValue<Vector2>();
        moveDir.x = moveInput.x;
        moveDir.z = moveInput.y;

        isWalk = moveInput.x != 0 || moveInput.y != 0;
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        isRun = context.ReadValueAsButton();
    }
    private void OnDisable()
    {
        inputActions.CharacterControls.Disable();
    }

    private void OnEnable()
    {
        inputActions.CharacterControls.Enable();
    }

    private void PlayerRotate()
    {
        if (moveDir.sqrMagnitude != 0)
        {
            rotateDir = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * rotateSpeed);
            transform.rotation = rotateDir;
        }
    }

    private void AnimateControl()
    {
        animator.SetBool("isWalk", isWalk);
        animator.SetBool("isRun", isRun);

    }

    private void Update()
    {
        if (!pv.IsMine) return;
        AnimateControl();
        PlayerRotate();
    }

    private void FixedUpdate()
    {
        if (!pv.IsMine) return;
        controller.Move(moveDir * Time.fixedDeltaTime);
    }

    public void Respawn()
    {

        controller.enabled = false;
        transform.position = new Vector3(-2f, -2.5f, -11f);
        controller.enabled = true;
    }
}