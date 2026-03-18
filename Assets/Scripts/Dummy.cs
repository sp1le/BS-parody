using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private GameObject targetCylinder;
    private void Awake()
    {
        targetCylinder.SetActive(false);
    }

    public void SetTargetStatus(bool isTarget)
    {
        targetCylinder.SetActive(isTarget);
    }
}
