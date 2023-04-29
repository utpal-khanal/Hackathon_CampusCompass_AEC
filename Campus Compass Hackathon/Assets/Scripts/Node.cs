using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Node : MonoBehaviour
{
    public string nodeName;
    public static UnityAction<GameObject> nodeClicked;
    public static UnityAction<Node> nodeSpawned;
    [SerializeField] GameObject roomPlate;

    public List<Node> myAdjacentNodeList;
    public Node parentNode = null;
    public bool visited = false;

    // Start is called before the first frame update

 
    void Start()
    {
        nodeSpawned?.Invoke(this);

        if(!string.IsNullOrEmpty(nodeName))
        {
            GameObject rmPlate = Instantiate(roomPlate, transform.position, transform.rotation);
            rmPlate.GetComponent<FaceCamera>().roomPlateText.text = nodeName;

        }



    }

    private void OnEnable()
    {
        
    }


    private void OnMouseDown()
    {
        
        nodeClicked?.Invoke(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
