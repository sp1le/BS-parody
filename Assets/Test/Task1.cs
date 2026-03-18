using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Task1 : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private InputController controller;
    private Rigidbody rb;
    private bool isWalkTask1;

    private void Awake()
    {
        controller = new InputController();
        rb = GetComponent<Rigidbody>();


        controller.Test.Task1.started += Task1Move;
        controller.Test.Task1.performed += Task1Move;
        controller.Test.Task1.canceled += Task1Move;

    }

    private void OnEnable()
    {
        controller.Enable();
    }
    private void OnDisable()
    {
        controller.Disable();
    }
    private void Task1Move(InputAction.CallbackContext context)
    {
        isWalkTask1 = context.ReadValueAsButton();
    }

    void Update()
    {
        if (isWalkTask1)
        {
            
            Vector3 move = new Vector3(speed * Time.deltaTime, 0, 0);
            rb.MovePosition(rb.position + move);
        }
    }
}
