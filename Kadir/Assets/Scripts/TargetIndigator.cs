using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndigator : MonoBehaviour
{
    
    public Transform target;
    public float arrowAnimationSpeed = 1f;
    

    public float rotationSpeed;
    Vector3 tpos;

    Animator arrowAnimator;

    private void Start()
    {
        arrowAnimator = GetComponent<Animator>();
    }

    public void setTarget(Transform pos)
    {
        target = pos;


    }

    // Update is called once per frame
    void Update()
    {
        arrowAnimator.speed = arrowAnimationSpeed;

        tpos = target.position;
        tpos += Vector3.up*1;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(tpos-transform.position), rotationSpeed * Time.deltaTime);




    }
}
