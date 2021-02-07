using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 distance;
    public bool offSetValues;
    public float mouseSensitivity;
    public Transform pivot;

    public bool invertYAxis;

    //Variables to set max and minimum vertical camera angle
    public float maxCameraAngle;
    public float minCameraAngle;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        distance = player.position - transform.position;
        if (!offSetValues)
        {
            distance = player.position - transform.position;
        }
        
        pivot.transform.position = player.transform.position;
        pivot.transform.parent = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float joyX = Input.GetAxis("RHorizontal") * mouseSensitivity;
        if (mouseX > 0 || mouseX < 0 || joyX > 0 || joyX < 0)
        {
            player.Rotate(0, mouseX, 0);
        }
        
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //Invert Y-axis function
        if (invertYAxis)
        {
            pivot.Rotate(mouseY, 0, 0);
        }
        else
        {
            pivot.Rotate(-mouseY, 0, 0);
        }

        //Set limit to vertical camera rotation
        if(pivot.rotation.eulerAngles.x > maxCameraAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxCameraAngle, 0, 0);
        }
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360 + minCameraAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minCameraAngle, 0, 0);
        }

        float yAngle = player.eulerAngles.y;
        float xAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(xAngle, yAngle, 0);
        transform.position = player.position - (rotation * distance);
            //transform.position = player.position - distance;
        if(transform.position.y < player.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y - 0.5f, transform.position.z);
        }

        transform.LookAt(player);
    }
}
