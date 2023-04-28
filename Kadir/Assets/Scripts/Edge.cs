using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Edge : MonoBehaviour
{
    public GameObject firstNode;
    public GameObject secondNode;
    public bool activated = false;
    float animationTime = 0.001f;
    float sumOfDeltaLengths = 0;

    float totalLength = 0;
    MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponentInChildren<MeshRenderer>();
    }

    private void OnEnable()
    {
        EdgeGenerator.SetEdgeVisibilityEvent += SetEdgeVisibility;
    }

    private void OnDisable()
    {
        EdgeGenerator.SetEdgeVisibilityEvent -= SetEdgeVisibility;

    }

    private void SetEdgeVisibility(bool arg0)
    {
        mr.enabled = arg0;
    }



    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            transform.position = firstNode.transform.position;
            totalLength = Vector3.Distance(firstNode.transform.position, secondNode.transform.position);
            transform.LookAt(secondNode.transform);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, totalLength);
        }


    }



    public void StartLinkAnimationCoroutine()
    {
        StartCoroutine(LinkAnimation());
    }

    private IEnumerator LinkAnimation()
    {
        while (true)
        {
            totalLength = Vector3.Distance(firstNode.transform.position, secondNode.transform.position);
            transform.LookAt(secondNode.transform);
            if (sumOfDeltaLengths <= totalLength)
            {
                sumOfDeltaLengths = sumOfDeltaLengths + 0.2f;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, sumOfDeltaLengths);
            }
            else
            {
                activated = true;
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(animationTime);
        }


    }
}
