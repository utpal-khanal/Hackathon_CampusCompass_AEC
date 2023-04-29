using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 10f;
    public float minMouseLook = 45;

    public Transform playerBody;
    public bool isMouseLookable = true;
    public float timeBeforeReposition = 1f;

    float xRotation = 0f;
    Vector3 yRotation;

    float prevX;
    float prevY;
    float initialTimeBeforeRepos;

    CameraLook cl;
    
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        initialTimeBeforeRepos = timeBeforeReposition;
        cl = GetComponent<CameraLook>();


        prevX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        prevY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    }

    
    
    // Update is called once per frame
    void Update()
    {
        

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Debug.Log(mouseY);
        if(prevX != mouseX || prevY != mouseY)
        {
            isMouseLookable = true;
            timeBeforeReposition = initialTimeBeforeRepos;

            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -minMouseLook, minMouseLook);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);            

            yRotation = Vector3.up * mouseX;

            playerBody.Rotate(yRotation);

          // transform.Rotate(xRotation);
            


            prevX = mouseX;
            prevY = mouseY;
        }
        else
        {
            timeBeforeReposition -= Time.deltaTime;
            if(timeBeforeReposition <= 0)
            {
                isMouseLookable = false;

                // xRotation = transform.localEulerAngles.x;
                // xRotation = Mathf.Clamp(transform.localEulerAngles.x, -45f, 45f);
                float xAngle = transform.localEulerAngles.x;

                if (xAngle >= 0 && xAngle <= minMouseLook)
                {
                    xAngle = Mathf.Clamp(xAngle, 0, minMouseLook);
                }
                else if (xAngle >= 360 - minMouseLook && xAngle <= 360)
                {
                    xAngle =  xAngle - 360;
                }

                xRotation = xAngle;


                timeBeforeReposition = 0;


            }
        }



       
    }
}
