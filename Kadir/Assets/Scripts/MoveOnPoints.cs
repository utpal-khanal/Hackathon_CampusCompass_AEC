using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPoints : MonoBehaviour
{
    public List<GameObject> points;
    public float speed = 10;
    public float minLookDistance = 5;
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

    public void SetWayPoints(List<GameObject> pointsList)
    {
        points.Clear();
        points = pointsList;
    }

    // Update is called once per frame
    void Update()
    {
        float ySpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime ;
        float distance=1;
        //  ySpeed = Math.Clamp(ySpeed, 0f, 1f);
        if (ySpeed >= 0)
        {
            /*if(i< points.Count) 
            { 

            }*/
            transform.position = Vector3.MoveTowards(transform.position, points[i].transform.position, ySpeed);
            distance = Vector3.Distance(transform.position, points[i].transform.position);

        }
        else
        {
            if (i > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[i-1].transform.position, -ySpeed);
                distance = Vector3.Distance(transform.position, points[i-1].transform.position);

            }
        }





        if (distance <= minLookDistance)
        {
            if (i < points.Count - 1)
            {
                targetIndigator.setTarget(points[i + 1].transform);
                 cameraLook.setCamera(points[i+1].transform);

            }

            if (i >= 0 || i < points.Count - 1)
            {
                if (ySpeed >= 0)
                {
                    if (i < points.Count - 1)
                        i++;
                }
                else
                {
                    if (i > 0)
                    {
                        i--;
                    }
                }
                // cameraLook.setCamera(points[i].transform);
                //Debug.Log(ySpeed);

            }

            if (distance <= 0.1f)
            {
                Debug.Log("reached goal");
            }
        }
       
    }
}
