using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Node
{
    public int xPos;
    public int yPos;

    public bool impassable;
    public int traversalCost;

    public Node parent;

    public int gCost;
    public int hCost;
    public int fCost;

    public void GetFCost()
    {
        fCost = gCost + hCost;
    }
    public Node(int x, int y, bool obstacle, int cost)
    {
        xPos = x;
        yPos = y;
        impassable = obstacle;
        traversalCost = cost;
    }
}
