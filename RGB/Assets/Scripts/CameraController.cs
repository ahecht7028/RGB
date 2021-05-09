using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity;

    float x;
    float y;
    bool paused;

    //Used to only hold one ball at a time
    public bool holding;

    void Start()
    {
        sensitivity = 3;
        x = transform.localRotation.x;
        y = transform.parent.gameObject.transform.localRotation.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        holding = false;
        paused = false;
    }

    void Update()
    {
        if (!paused)
        {
            x += -Input.GetAxis("Mouse Y") * sensitivity;
            x = Mathf.Clamp(x, -90, 90);
            transform.localRotation = Quaternion.Euler(x, transform.localRotation.y, transform.localRotation.z);

            y += Input.GetAxis("Mouse X") * sensitivity;
            transform.parent.gameObject.transform.localRotation = Quaternion.Euler(transform.parent.gameObject.transform.localRotation.x, y, transform.parent.gameObject.transform.localRotation.z);
        }
    }

    public void Pause()
    {
        paused = true;
    }

    public void UnPause()
    {
        paused = false;
    }
}
