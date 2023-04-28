using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPoints : MonoBehaviour
{
    public List<GameObject> points;
    public float speed = 10;
    int i = 0;
    [SerializeField] TargetIndigator targetIndigator;
    [SerializeField] CameraLook cameraLook;

    private void OnEnable()
    {
        Node.nodeSpawned += nodeSpawed;
    }

    private void OnDisable()
    {
        Node.nodeSpawned -= nodeSpawed;
    }

    private void nodeSpawed(Node arg0)
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        float ySpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime ;
        //  ySpeed = Math.Clamp(ySpeed, 0f, 1f);
        if (ySpeed >= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[i].transform.position, ySpeed);

        }
        else
        {
            if (i > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[i - 1].transform.position, -ySpeed);

            }
        }



        float distance = Vector3.Distance(transform.position, points[i].transform.position);


        if (distance <= 3)
        {
            if (i < points.Count - 1)
            {
                targetIndigator.setTarget(points[i + 1].transform);

            }


            if (distance <= 0.1f)
            {
                if (i < points.Count - 1)
                {
                    i++;
                    cameraLook.setCamera(points[i].transform);
                }

            }
        }
       
    }
}
