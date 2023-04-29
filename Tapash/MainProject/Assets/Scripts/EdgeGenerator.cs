using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;



public class EdgeGenerator : MonoBehaviour
{
    GameObject firstNode;
    GameObject secondNode;
    [SerializeField] MouseLook mouseLook;

    [SerializeField]  GameObject link;
    [SerializeField] float animationTime = 0.001f;
    bool edgeVisible = true;

    float totalLength = 0;
    float sumOfDeltaLengths = 0;
    bool activated = false;

    bool chainPlacing = false;
 
    GameObject edge;

    public static UnityAction<bool> SetEdgeVisibilityEvent;

    int timesClicked = 0;
    bool isAddEdge = false;
    
    void Start()
    {
        //CreaterEdgeFor(firstNode,secondNode);
    }

    private void OnEnable()
    {
        Node.nodeClicked += ClickedNode;
    }

    private void OnDisable()
    {
        Node.nodeClicked -= ClickedNode;
    }
    
    public void ClickedNode(GameObject clickedNode)
    {        
        if(chainPlacing == true )
        {            
            timesClicked++;
        
            if (timesClicked == 1)
            {
                firstNode = clickedNode;
                firstNode.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            else if (timesClicked >= 2)
            {
                secondNode = clickedNode;
                if (firstNode != secondNode)
                {

                    Node a = firstNode.GetComponent<Node>();
                    Node b = secondNode.GetComponent<Node>();

                    if (!a.myAdjacentNodeList.Contains(b) && !b.myAdjacentNodeList.Contains(a))
                    {
                        b.myAdjacentNodeList.Add(a);
                        a.myAdjacentNodeList.Add(b);


                        secondNode.GetComponent<MeshRenderer>().material.color = Color.green;
                        CreaterEdgeFor(firstNode, secondNode);

                        firstNode = secondNode;
                        secondNode.GetComponent<MeshRenderer>().material.color = Color.green;

                    }
                    else
                    {
                        Debug.LogWarning("Parallel Edge creation failed, gutely");

                    }
                }
                else
                {
                    Debug.LogWarning("Created Self Loop... gutley");
                }
                
            }


        }
        




    }


    public GameObject CreaterEdgeFor(GameObject a, GameObject b)
    {
        //totalLength = Vector3.Distance(a.transform.position, b.transform.position);
        edge = Instantiate(link, a.transform.position, Quaternion.identity);
        Edge e = edge.GetComponent<Edge>();
        edge.transform.LookAt(b.transform);
        edge.transform.parent = a.transform.parent;
        e.firstNode = a;
        e.secondNode = b;
        totalLength = Vector3.Distance(e.firstNode.transform.position, e.secondNode.transform.position);
        
        

        e.StartLinkAnimationCoroutine();
        return edge;
    }

    void Update()
    {
        if(activated)
        {
            Edge e = edge.GetComponent<Edge>();
            // edge.transform.position = firstNode.transform.position;
            edge.transform.position = e.firstNode.transform.position;
            //totalLength = Vector3.Distance(firstNode.transform.position, secondNode.transform.position);
            totalLength = Vector3.Distance(e.firstNode.transform.position, e.secondNode.transform.position);
           // edge.transform.LookAt(secondNode.transform);
            edge.transform.LookAt(e.secondNode.transform);
           // edge.transform.LookAt(edge.GetComponent<Edge>().secondNode.transform);
            edge.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, totalLength);
            
        }

       
        if(Input.GetKeyDown(KeyCode.Space))
        {
            chainPlacing = true;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            chainPlacing = false;
        }
        if(!chainPlacing)
        {            
                firstNode = null;
                secondNode = null;

                timesClicked = 0;            
        }
        //  Debug.Log($"isadad{isAddEdge}");
        //Debug.Log(timesClicked);

        if (Input.GetKeyDown(KeyCode.V))
        {
            edgeVisible = !edgeVisible;
            SetEdgeVisibilityEvent?.Invoke(!edgeVisible);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            mouseLook.enabled = !mouseLook.enabled;
        }

    }

    /*IEnumerator LinkAnimation()
    {
        while(true)
        {
            totalLength = Vector3.Distance(firstNode.transform.position, secondNode.transform.position);
            edge.transform.LookAt(secondNode.transform);
            if (sumOfDeltaLengths <= totalLength)
            {
                sumOfDeltaLengths = sumOfDeltaLengths + 0.2f;
                edge.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, sumOfDeltaLengths);
            }
            else
            {
                activated = true;
                edge.GetComponent<Edge>().activated = activated;
                StopAllCoroutines();
            } 
            yield return new WaitForSeconds(animationTime);
        }


    }*/
}
