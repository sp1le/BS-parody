using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharGravity : MonoBehaviour
{
    public float gravityScale =10f; // Множитель гравитации
    public Vector3 torque = new Vector3(0, 800, 0);
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(torque, ForceMode.VelocityChange);
    }
    void FixedUpdate()
    {
   
        if (rb != null)
        {
            Vector3 customGravity = Physics.gravity * gravityScale;
            rb.AddForce(customGravity, ForceMode.Acceleration);
           
        }
    }
}
