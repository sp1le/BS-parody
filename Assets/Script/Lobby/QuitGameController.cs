using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuitGameController : MonoBehaviour
{
    [SerializeField] private GameObject quitGameCanvas;
    private InputController inputActions;
    private bool isquitGame;
   
    private void Awake()
    {
        inputActions = new InputController();
    }
    void Start()
    {
        quitGameCanvas.SetActive(false);
        inputActions.GUIControls.QuitGame.started += QuitGameCallback;
        inputActions.GUIControls.QuitGame.canceled += QuitGameCallback;

    }

    private void QuitGameCallback(InputAction.CallbackContext context)
    {
        bool val = context.ReadValueAsButton();
        if (val) isquitGame = !isquitGame;
        quitGameCanvas.SetActive(isquitGame);
      
    }
    public void CloseQuitModal()
    {
        quitGameCanvas.SetActive(!isquitGame);
        isquitGame = !isquitGame;
    }
    public void QuitGame()
    {
        print("Quit");
        Application.Quit();
    }

    private void OnEnable()
    {
        inputActions.GUIControls.Enable();
    }

    private void OnDisable()
    {
        inputActions.GUIControls.Disable(); 
    }
}
