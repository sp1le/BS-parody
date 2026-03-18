using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitcher : MonoBehaviour
{
    public Button button1;
    public Button button2;

    public Sprite button1NormalSprite;
    public Sprite button1PressedSprite;


    public Sprite button2NormalSprite;
    public Sprite button2PressedSprite;

    private void Start()
    {
       
        SetButtonState(button1, button1NormalSprite, button1PressedSprite, true);
        SetButtonState(button2, button2NormalSprite, button2PressedSprite, false);

        button1.onClick.AddListener(() => OnButtonClicked(button1, button2));
        button2.onClick.AddListener(() => OnButtonClicked(button2, button1));
    }

    private void OnButtonClicked(Button clickedButton, Button otherButton)
    {
        if (clickedButton == button1)
        {
            SetButtonState(button1, button1NormalSprite, button1PressedSprite, true);
            SetButtonState(button2, button2NormalSprite, button2PressedSprite, false);
        }
        else
        {
            SetButtonState(button2, button2NormalSprite, button2PressedSprite, true);
            SetButtonState(button1, button1NormalSprite, button1PressedSprite, false);
        }
    }

    private void SetButtonState(Button button, Sprite normalSprite, Sprite pressedSprite, bool isPressed)
    {
        var buttonImage = button.GetComponent<Image>();
        buttonImage.sprite = isPressed ? pressedSprite : normalSprite;
        button.interactable = !isPressed; 
    }
}
