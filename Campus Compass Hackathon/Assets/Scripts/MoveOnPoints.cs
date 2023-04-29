using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPoints : MonoBehaviour
{
    public List<GameObject> points;
    public float speed = 10;
    public float minLookDistance = 5;
    public int currentNodeIndex = 0;
    [SerializeField] TargetIndigator targetIndigator;
    [SerializeField] CameraLook cameraLook;
    [SerializeField] GameObject destinationReachedText;

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
    private void Start()
    {
        currentNodeIndex = 0;
        destinationReachedText.SetActive(false);
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
            transform.position = Vector3.MoveTowards(transform.position, points[currentNodeIndex].transform.position, ySpeed);
            distance = Vector3.Distance(transform.position, points[currentNodeIndex].transform.position);

        }
        else
        {
            if (currentNodeIndex > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[currentNodeIndex-1].transform.position, -ySpeed);
                distance = Vector3.Distance(transform.position, points[currentNodeIndex-1].transform.position);

            }
        }





        if (distance <= minLookDistance)
        {
            if (currentNodeIndex < points.Count - 1)
            {
                targetIndigator.setTarget(points[currentNodeIndex + 1].transform);
                 cameraLook.setCamera(points[currentNodeIndex+1].transform);

            }

            if (distance <= 0.1f)
            {
                if (currentNodeIndex >= 0 || currentNodeIndex < points.Count - 1)
                {
                    if (ySpeed >= 0)
                    {
                        if (currentNodeIndex < points.Count - 1)
                            currentNodeIndex++;
                    }
                    else
                    {
                        if (currentNodeIndex > 0)
                        {
                            currentNodeIndex--;
                        }
                    }
                    // cameraLook.setCamera(points[i].transform);
                    //Debug.Log(ySpeed);

                }
            }

            if(currentNodeIndex == points.Count - 1)
            {

                destinationReachedText.SetActive(true);

            }
            else
            {
                destinationReachedText.SetActive(false);
            }




        }
       
    }
}
