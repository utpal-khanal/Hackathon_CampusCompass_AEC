using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public float minAutoMouseLook = 45f;



    public float rotationSpeed;

    Vector3 targetPos;

    MouseLook mouseLook;

    private void Start()
    {
        mouseLook = GetComponent<MouseLook>();
    }

    public void setCamera(Transform pos)
    {
        target = pos;


    }

    // Update is called once per frame
    void Update()
    {
        if (!mouseLook.isMouseLookable)
        {
            targetPos = target.position;

            Quaternion rotTarget = Quaternion.LookRotation(targetPos - player.transform.position);
            Quaternion rotTargetx = Quaternion.LookRotation(targetPos - transform.position);

            Quaternion rot = Quaternion.Slerp(player.transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);
            Quaternion rotx = Quaternion.Slerp(transform.rotation, rotTargetx, rotationSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);

            Vector3 rotationY = rot.eulerAngles;
            rotationY = new Vector3(player.transform.eulerAngles.x, rotationY.y, player.transform.eulerAngles.z);
            player.transform.eulerAngles = rotationY;

            Vector3 rotationX = rotx.eulerAngles;

            float xAngle = rotationX.x;
            if(xAngle >= 0 && xAngle <= minAutoMouseLook)
            {
                xAngle = Mathf.Clamp(xAngle, 0, minAutoMouseLook);

            }
            else if(xAngle >= 360-minAutoMouseLook && xAngle <= 360)
            {
                xAngle = Mathf.Clamp(xAngle, 360- minAutoMouseLook, 360);

            }





            rotationX = new Vector3(xAngle, transform.eulerAngles.y, transform.eulerAngles.z);            

           transform.eulerAngles = rotationX;




            //player.Rotate(0, rotation.y, 0); 

            // transform.LookAt( target.position, Vector3.up);
        }


    }
}
