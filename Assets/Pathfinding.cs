using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [Header("Pathfinding")]
    public GameObject pathPrefab;
    public Transform agentTransform;
    public Transform targetTransform;

    [Header("Grid")]
    public LayerMask obstacleLayer;
    public float deltaBetweenNodes;
    public Transform mapBottomLeft;
    public Transform mapTopRight;

    Grid grid;
    Node finalPathNode;
    Vector2 agentPoint;
    Vector2 targetPoint;
    List<GameObject> pathObjects;

    // Start is called before the first frame update
    void Start()
    {
        pathObjects = new List<GameObject>();
        grid = new Grid(mapBottomLeft.position, mapTopRight.position, deltaBetweenNodes, obstacleLayer);
        finalPathNode = FindPathAStar();
    }

    // Update is called once per frame
    void Update()
    {
        GetAgentAndTargetPositions();
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            grid = new Grid(mapBottomLeft.position, mapTopRight.position, deltaBetweenNodes, obstacleLayer);
            finalPathNode = FindPathAStar();
        }
        
    }

    public void DrawPath()
    {
        Node node = finalPathNode;

        foreach (GameObject obj in pathObjects)
        {
            Destroy(obj);
        }

        pathObjects.Clear();

        Gizmos.color = Color.blue;
        while (node.Parent != null)
        {
            GameObject obj = Instantiate(pathPrefab, node.WorldPos, Quaternion.identity);
            obj.transform.localScale = Vector2.one * deltaBetweenNodes;
            pathObjects.Add(obj);
            node = node.Parent;
        }
    }

    void GetAgentAndTargetPositions()
    {
        agentPoint = agentTransform.position;
        targetPoint = targetTransform.position;
    }

    Node FindPathAStar()
    {
        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        Node startNode = grid.GetNodes()[(int)((agentPoint.x - mapBottomLeft.position.x) / deltaBetweenNodes), (int)((agentPoint.y - mapBottomLeft.position.y) / deltaBetweenNodes)];
        Node targetNode = grid.GetNodes()[(int)((targetPoint.x - mapBottomLeft.position.x) / deltaBetweenNodes), (int)((targetPoint.y - mapBottomLeft.position.y) / deltaBetweenNodes)];

        open.Add(startNode);
        int i = 0;
        while (open.Count > 0)
        {
            Node currNode = GetSmallestFCost(open);
            open.Remove(currNode);
            closed.Add(currNode);

            if (currNode.Equals(targetNode))
            {
                return currNode;
            }

            i++;

            List<Node> neighbours = grid.GetNeighborNodes(currNode);

            foreach (Node n in neighbours)
            {
                if (n.Traversable && !closed.Contains(n))
                {
                    int newFCost = currNode.GCost + grid.GetDistance(currNode, n) + grid.GetDistance(n, targetNode);
                    if (!open.Contains(n) || newFCost < n.GetFCost())
                    {
                        n.GCost = currNode.GCost + grid.GetDistance(currNode, n);
                        n.HCost = grid.GetDistance(n, targetNode);
                        n.Parent = currNode;

                        if (!open.Contains(n))
                        {
                            open.Add(n);
                        }
                    }
                }
            }
        }

        return null;
    }

    Node GetSmallestFCost(List<Node> nodes)
    {
        Node result = nodes[0];

        foreach (Node n in nodes)
        {
            if (n.GetFCost() < result.GetFCost() || (n.GetFCost() == result.GetFCost() && n.HCost < result.HCost))
            {
                result = n;
            }
        }

        return result;
    }
}
