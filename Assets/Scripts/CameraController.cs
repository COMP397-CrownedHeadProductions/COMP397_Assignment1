using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 distance;
    public bool offSetValues;
    public float mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        distance = player.position - transform.position;
        if (!offSetValues)
        {
            distance = player.position - transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate player avatar along with camera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        player.Rotate(0, mouseX, 0);
        player.Rotate(-mouseY, 0, 0);

        float yAngle = player.eulerAngles.y;
        float xAngle = player.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(xAngle, yAngle, 0);
        transform.position = player.position - (rotation * distance);
        //transform.position = player.position - distance;
        transform.LookAt(player);
    }
}
