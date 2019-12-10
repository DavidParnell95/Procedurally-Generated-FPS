using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensetivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        //Lock cursor to the game 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")* mouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")* mouseSensetivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);//Lock up nad down rotation to 180 degrees

        playerBody.Rotate(Vector3.up * mouseX);//look left and right
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
