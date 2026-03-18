using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Task2 : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private InputController controller;
    private Rigidbody rb;
    private bool isWalkTask2;

    private void Awake()
    {
        controller = new InputController();
        rb = GetComponent<Rigidbody>();


        controller.Test.Task2.started += Task2Move;
        controller.Test.Task2.performed += Task2Move;
        controller.Test.Task2.canceled += Task2Move;

    }

    private void OnEnable()
    {
        controller.Enable();
    }
    private void OnDisable()
    {
        controller.Disable();
    }
    private void Task2Move(InputAction.CallbackContext context)
    {
        rb.velocity = Vector3.right * speed;
    }

    void Update()
    {
           
    }
}
