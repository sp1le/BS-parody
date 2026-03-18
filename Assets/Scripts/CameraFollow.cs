using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float camHeight;
    [SerializeField] private float rearDistance;
    private Vector3 cameraOffset;
    private Vector3 curDir;
    private void Start()
    {
        transform.position = new Vector3(mainCharacter.transform.position.x,
            mainCharacter.transform.position.y + camHeight,
            mainCharacter.transform.position.z - rearDistance);
        transform.rotation = Quaternion.LookRotation(mainCharacter.transform.position- transform.position);
    }
    
    private void Update()
    {
        CameraMove();
    }

    public void SetOffset(Vector3 offset)
    {
        if(offset.z < 0)
        {
            cameraOffset = offset * 3; // рух вниз
        }
        else if (offset.z > 0)
        {
            cameraOffset = offset * 2; // рух вгору
        }
        else
        {
            cameraOffset = offset * 1; // рух вліво-вправо
        }
    }

    private void CameraMove()
    {
        curDir = new Vector3(mainCharacter.transform.position.x + cameraOffset.x,
            mainCharacter.transform.position.y + camHeight,
            mainCharacter.transform.position.z - rearDistance + cameraOffset.z);

        transform.position = Vector3.Lerp(transform.position, curDir, Time.deltaTime * moveSpeed);
    }
}
