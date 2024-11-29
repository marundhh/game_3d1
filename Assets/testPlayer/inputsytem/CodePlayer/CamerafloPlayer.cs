using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerafloPlayer : MonoBehaviour
{
    [SerializeField] Transform followTaget;
    [SerializeField] float rotationSpeed =2f;
    [SerializeField] float disstance = 5;
    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;
    [SerializeField] Vector2 framingOffset;

    [SerializeField] bool invertX;
    [SerializeField] bool invertY;
    float rotationX;
    float rotationY;

    float inverXVal;
    float inverYVal;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;   
    }

    private void Update()
    {
        inverXVal = (invertX) ? -1 : 1;
        inverYVal = (invertY) ? -1 : -1;

        rotationX += Input.GetAxis("Mouse Y") * inverYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
        rotationY += Input.GetAxis("Mouse X") * inverXVal * rotationSpeed;
        
        var tagetRotation = Quaternion.Euler(0, rotationY, 0);

        var foscusPosition = followTaget.position + new Vector3(framingOffset.x, framingOffset.y);

        transform.position = foscusPosition -  tagetRotation * new Vector3(0, 0,disstance);
        transform.rotation = tagetRotation;
    }

}