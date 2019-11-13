 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensetivity = 100f;//Mouse sensetivity variable

    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//Lock cursor to center of screen and hide it
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);//Limit how far the player can look up and down to 90 either way

        transform.localRotation = Quaternion.Euler(xRotation,0,0);//Allow use to look up & down
        playerBody.Rotate(Vector3.up * mouseX);// Rotate side to side along x axis

    }
}
