using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class NodePlacingSystem : MonoBehaviour
{
    public GameObject node;
    public LayerMask layerMask;
    public float nodePlacingDistance = 5;

    private bool firstTime = true;
    private Vector3 staringPostion;
    private Vector3 previouPlacedPosition;
    // Start is called before the first frame update
    void Start()
    {
        firstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitData, Mathf.Infinity, layerMask))
            {


                if (firstTime)
                {
                    previouPlacedPosition = hitData.point;
                    staringPostion = hitData.point;
                    Instantiate(node, hitData.point, Quaternion.identity);
                    firstTime = false;
                }
                else  //if(Vector3.Distance(previouPlacedPosition, hitData.point) < nodePlacingDistance)
                {

                    Vector3 direction = Vector3.Normalize(hitData.point - previouPlacedPosition);
                    staringPostion +=  direction * nodePlacingDistance;
                    Vector3 instantiatePos = staringPostion ;
                    Instantiate(node, instantiatePos , Quaternion.identity);
                    previouPlacedPosition = hitData.point;
                   
                    

                   // Debug.Log(hitData.point);
                }

                                  
            }
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            if (Physics.Raycast(ray, out hitData, Mathf.Infinity, layerMask))
            {
                /*if(hitData.transform.TryGetComponent(out Node node))
                {
                    staringPostion = node.transform.position;
                }
                */
                    staringPostion = hitData.point;
                
                Debug.Log("pressed starting" + staringPostion);
                previouPlacedPosition = staringPostion;


            }
        }
        
    }

}
