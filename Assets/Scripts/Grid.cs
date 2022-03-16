using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    Node[,] nodes;

    public Grid(Vector2 mapBottomLeft, Vector2 mapTopRight, float deltaBetweenNodes, LayerMask obstacleLayer)
    {
        GenerateGrid(mapBottomLeft, mapTopRight, deltaBetweenNodes, obstacleLayer);
    }

    public Node[,] GetNodes()
    {
        return nodes;
    }

    public List<Node> GetNeighborNodes(Node node)
    {
        List<Node> nodeList = new List<Node>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (node.X + i > -1 && node.X + i < nodes.GetLength(0) && node.Y + j > -1 && node.Y + j < nodes.GetLength(1) && (i != 0 || j != 0))
                {
                    nodeList.Add(nodes[node.X + i, node.Y + j]);
                }
            }
        }
        return nodeList;
    }

    public int GetDistance(Node n1, Node n2)
    {
        int dstX = Mathf.Abs(n1.X - n2.X);
        int dstY = Mathf.Abs(n1.Y - n2.Y);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }

    void GenerateGrid(Vector2 mapBottomLeft, Vector2 mapTopRight, float deltaBetweenNodes, LayerMask obstacleLayer)
    {
        int xNodes = Mathf.FloorToInt((mapTopRight.x - mapBottomLeft.x) / deltaBetweenNodes);
        int yNodes = Mathf.FloorToInt((mapTopRight.y - mapBottomLeft.y) / deltaBetweenNodes);

        nodes = new Node[xNodes + 1, yNodes + 1];

        for (int i = 0; i < xNodes + 1; i++)
        {
            for (int j = 0; j < yNodes + 1; j++)
            {
                nodes[i, j] = new Node(i, j, deltaBetweenNodes, mapBottomLeft);

                Vector2 pos = nodes[i, j].WorldPos;
                nodes[i, j].Traversable = !Physics2D.OverlapCircle(pos, deltaBetweenNodes / 2, obstacleLayer);
            }
        }
    }

}
