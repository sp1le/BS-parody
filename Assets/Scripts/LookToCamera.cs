using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    private Camera mainCam;
    private void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(mainCam.transform.position);
        transform.Rotate(Vector3.up * 180);
    }
}
