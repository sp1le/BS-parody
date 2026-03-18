using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class SwitchAttack : MonoBehaviour
{
    [SerializeField] private Button meleeAttackBtn;
    [SerializeField] private Button rangeAttackBtn;
    [SerializeField] private Sprite meleeBtnNormal;
    [SerializeField] private Sprite rangeBtnNormal;
    [SerializeField] private Sprite meleeBtnClicked;
    [SerializeField] private Sprite rangeBtnClicked;
    private InputController input;

    [HideInInspector] public bool isAttackMeele = true;
    void Awake()
    {
        input = new InputController();
        SetButtonState(meleeAttackBtn, meleeBtnNormal, meleeBtnClicked, true);
        SetButtonState(rangeAttackBtn, rangeBtnNormal, rangeBtnClicked, false);

        input.CharacterControls.ChangeAttack.started += OnAttackChanged;
       

        input.CharacterControls.ChangeAttack.performed += OnAttackChanged;
        input.CharacterControls.ChangeAttack.canceled += OnAttackChanged;
        
    }
    private void OnAttackChanged(InputAction.CallbackContext context)
    {

        OnButtonClicked(context.action.activeControl.name);
      
    }
    private void OnButtonClicked(string button)
    {
        if(button == "1")
        {
            SetButtonState(meleeAttackBtn, meleeBtnNormal, meleeBtnClicked, true);
            SetButtonState(rangeAttackBtn, rangeBtnNormal, rangeBtnClicked, false);
            isAttackMeele = true;
        }
        else if(button == "2")
        {
            SetButtonState(meleeAttackBtn, meleeBtnNormal, meleeBtnClicked, false);
            SetButtonState(rangeAttackBtn, rangeBtnNormal, rangeBtnClicked, true);
            isAttackMeele = false;
        }
    }

    private void SetButtonState(Button button, Sprite normalSprite, Sprite clickedSprite, bool isClicked)
    {
        Image buttonImg = button.GetComponent<Image>();
        buttonImg.sprite = isClicked ? clickedSprite : normalSprite;
        button.interactable = !isClicked;
    }

    private void OnDisable()
    {
        input.CharacterControls.Disable();
    }

    private void OnEnable()
    {
        input.CharacterControls.Enable();
    }
}
