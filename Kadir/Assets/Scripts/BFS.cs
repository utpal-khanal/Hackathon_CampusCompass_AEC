using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BFS : MonoBehaviour
{
    public Node sourceNode;
    public Node destinationNode;
    [SerializeField] MoveOnPoints moveOnPoints;
    [SerializeField] CameraLook cameraLook;
    [SerializeField] TargetIndigator arrowTarget;
    [SerializeField] GameObject player;


    public List<Node> graph = new List<Node>();

    public List<GameObject> shorestPathVertices = new List<GameObject>();

    // Start is called before the first frame update

    List<Node> queueList = new List<Node>();

    EdgeGenerator edgeGenerator;

    List<GameObject> shorestPathEdges = new List<GameObject>();

    private void OnEnable()
    {
        Node.nodeSpawned += addNodeToGraph;
    }
    private void OnDisable()
    {
        Node.nodeSpawned -= addNodeToGraph;

    }

    private void addNodeToGraph(Node arg0)
    {
        graph.Add(arg0);
    }

    void Start()
    {
        edgeGenerator = FindObjectOfType<EdgeGenerator>();

        player.GetComponent<PlayerMove>().enabled = true;
        player.GetComponent<MoveOnPoints>().enabled = false;

        // FindShortestPath();

    }

     [ContextMenu("Find Shortest Path")]

    public void Find()
    {
        if(sourceNode != null && destinationNode != null)
        {
            FindShortestPath(sourceNode, destinationNode);

        }
        else
        {
            Debug.LogWarning("Source or Destination is Null, Gutely.");
        }
    }

    public void FindShortestPath(Node src, Node destination)
    {
        for(int i = 0; i < shorestPathEdges.Count; i++)
        {
            Destroy(shorestPathEdges[i]);
        }

        queueList.Add(src);
        queueList[0].visited = true;


        while (queueList.Count > 0)
        {
            foreach (Node node in queueList[0].myAdjacentNodeList)
            {
                if (!node.visited)
                {

                    node.visited = true;
                    queueList.Add(node);
                    node.parentNode = queueList[0];

                }
            }
            Debug.Log(queueList[0].name);
            queueList.RemoveAt(0);
        }

        Node currentNode = destination;


        EdgeGenerator.SetEdgeVisibilityEvent?.Invoke(false);

        shorestPathEdges = new List<GameObject>();
        shorestPathVertices.Clear();
        shorestPathVertices = new List<GameObject>();

        while (currentNode != null)
        {
            if(currentNode.parentNode != null)
            {
                GameObject edge = edgeGenerator.CreaterEdgeFor(currentNode.gameObject, currentNode.parentNode.gameObject);

                edge.layer = LayerMask.NameToLayer("graph");

                edge.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("graph");
                //shorestPathEdges.Add(edge); // if need edges then add shortest path edges;

            }
            shorestPathVertices.Add(currentNode.gameObject);

            currentNode = currentNode.parentNode;

        }

        moveOnPoints.SetWayPoints(shorestPathVertices);
        cameraLook.setCamera(shorestPathVertices[0].transform);
        arrowTarget.setTarget(shorestPathVertices[0].transform);

        player.GetComponent<MoveOnPoints>().enabled = true;
        player.GetComponent<PlayerMove>().enabled = false;









        

        ResetGraph();

    }

    private void ResetGraph()
    {
        foreach (Node node in graph)
        {
            node.visited = false;
            node.parentNode = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
