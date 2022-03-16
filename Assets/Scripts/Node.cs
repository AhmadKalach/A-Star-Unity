using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    bool traversable;
    int x;
    int y;
    int gCost;
    int hCost;
    Node parent;
    Vector2 worldPos;

    public Node Parent { get => parent; set => parent = value; }
    public int HCost { get => hCost; set => hCost = value; }
    public int GCost { get => gCost; set => gCost = value; }
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public Vector2 WorldPos { get => worldPos;}
    public bool Traversable { get => traversable; set => traversable = value; }

    public Node(int x, int y, float deltaBetweenNodes, Vector2 mapBottomLeft)
    {
        this.x = x;
        this.y = y;
        this.worldPos = new Vector2(x, y) * deltaBetweenNodes + mapBottomLeft;
    }

    public int GetFCost()
    {
        return GCost + HCost;
    }
}
