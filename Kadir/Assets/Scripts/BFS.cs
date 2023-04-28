using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BFS : MonoBehaviour
{
    public Node sourceNode;
    public Node destinationNode;

    public List<Node> graph = new List<Node>();

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

        while (currentNode.parentNode != null)
        {
            GameObject edge = edgeGenerator.CreaterEdgeFor(currentNode.gameObject, currentNode.parentNode.gameObject);
            currentNode = currentNode.parentNode;

            shorestPathEdges.Add(edge);
        }       

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
