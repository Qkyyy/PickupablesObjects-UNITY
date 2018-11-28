using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float playerSpeed = 400f;
    public float mouseSensitivity = 1f;


    float mouseUpDown = 0.0f;
    float mouseRangeUpDown = 90f;

    public bool canLookAround = true;

    CharacterController characterController;

    private void Start()
    {
        characterController = transform.GetComponent<CharacterController>();
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;

        Vector3 move = new Vector3(x, 0, z);
        move = transform.rotation * move;
        characterController.Move(move * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        if (canLookAround == true)
        {
            float mouseLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
            transform.Rotate(0, mouseLeftRight, 0);

            mouseUpDown -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            mouseUpDown = Mathf.Clamp(mouseUpDown, -mouseRangeUpDown, mouseRangeUpDown);
            Camera.main.transform.localRotation = Quaternion.Euler(mouseUpDown, 0, 0);

        }
    }
}
