using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPathfinding
{
    //A* algorithm taken from older project, adapted from https://unitycodemonkey.com/video.php?v=alU04hvz6L4
    private const int straightCost = 10;
    private const int diagonalCost = 14;

    //stores the 2D array of nodes
    private Node[,] nodeMap;
    //this is the open list of nodes to traverse
    private List<Node> openList;
    //this is the list of nodes that are not going to be visited
    private List<Node> closedList;

    //dimensions of the grid
    private int width;
    private int height;

    public Node parent;

    /// <summary>
    /// this finds a list of nodes which comprises the path from the start point to the end point
    /// </summary>
    /// <param name="logicMap">This is the 2D array of nodes being passed in</param>
    /// <param name="start">These are the starting coordinates</param>
    /// <param name="end">These are the ending coordinates</param>
    /// <returns>Returns a list of nodes which makes up the ordered path</returns>
    public List<Node> FindPath(Node[,] logicMap, Vector2 start, Vector2 end)
    {
        //assign the node map from the argument
        nodeMap = logicMap;
        //get dimensions
        width = logicMap.GetLength(0);
        height = logicMap.GetLength(1);
        //this is the starting node
        Node nodeStart = logicMap[(int)start.x, (int)start.y];
        //this is the desination node
        Node nodeEnd = logicMap[(int)end.x, (int)end.y];
        //initialise open and closed list
        openList = new List<Node>() { nodeStart };
        closedList = new List<Node>();

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //Debug.Log(x + ", " + y);
                //Debug.Log(logicMap[x,y]);
                Node pathNode = nodeMap[x, y];
                pathNode.gCost = int.MaxValue;
                pathNode.GetFCost();
                pathNode.parent = null;
            }
        }

        //get initial total cost
        nodeStart.gCost = 0;
        nodeStart.hCost = GetDistanceCost(nodeStart, nodeEnd);
        nodeStart.GetFCost();

        while (openList.Count > 0)
        {
            //head to the next adjacent cheapest node
            Node currentNode = CheapestFNode(openList);
            //if the current node is the final node, we've reached the end and can construct the path
            if (currentNode == nodeEnd)
            {
                return ConstructPath(nodeEnd);
            }
            //if node was visited, add it to the closed list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(Node adjacentNode in GetAdjacentNodes(currentNode))
            {
                //if the node is on the closed list, don't consider it
                if (closedList.Contains(adjacentNode))
                    continue;
                //if the node is impassable, add it to the closed list
                if (adjacentNode.impassable)
                {
                    closedList.Add(adjacentNode);
                    continue;
                }

                //the gcost being calculated is the total cost of the move, multiplied by the cost of the terrain
                int potentialGCost = currentNode.gCost + GetDistanceCost(currentNode, adjacentNode)*adjacentNode.traversalCost;

                //assign the estimated gcost to the surrounding nodes so that they can be evaluateds
                if(potentialGCost < adjacentNode.gCost)
                {
                    adjacentNode.parent = currentNode;
                    adjacentNode.gCost = potentialGCost;

                    adjacentNode.hCost = GetDistanceCost(adjacentNode, nodeEnd);
                    adjacentNode.GetFCost();

                    //if the considered node is not in the open list yet, add it
                    if (!openList.Contains(adjacentNode))
                    {
                        openList.Add(adjacentNode);
                    }
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Assembles the list which makes up the path by following the parents up from the final node
    /// </summary>
    /// <param name="endNode">This is the final node in the path</param>
    /// <returns>returns a list of nodes in the path order</returns>
    private List<Node> ConstructPath(Node endNode)
    {
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node currentNode = endNode;
        //The starting node is the only one without a parent, so it loops until it reaches the final stage
        while(currentNode.parent != null)
        {
            path.Add(currentNode.parent);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    /// <summary>
    /// gets the adjacent nodes based on array index
    /// </summary>
    /// <param name="currentNode">This is the selected node</param>
    /// <returns>returns a list of adjacent nodes</returns>
    private List<Node> GetAdjacentNodes(Node currentNode)
    {
        List<Node> adjacentList = new List<Node>();
        if(currentNode.xPos - 1 >= 0)
        {
            //the below initials are compass directions for what the index is selected
            //w
            adjacentList.Add(nodeMap[currentNode.xPos - 1, currentNode.yPos]);
            //sw
            if (currentNode.yPos - 1 >= 0)
                adjacentList.Add(nodeMap[currentNode.xPos - 1, currentNode.yPos - 1]);
            //nw
            if (currentNode.yPos + 1 < height)
                adjacentList.Add(nodeMap[currentNode.xPos - 1, currentNode.yPos + 1]);
        }        
        if(currentNode.xPos + 1 < width)
        {
            //e
            adjacentList.Add(nodeMap[currentNode.xPos + 1, currentNode.yPos]);
            //se
            if (currentNode.yPos - 1 >= 0)
                adjacentList.Add(nodeMap[currentNode.xPos + 1, currentNode.yPos - 1]);
            //ne
            if (currentNode.yPos + 1 < height)
                adjacentList.Add(nodeMap[currentNode.xPos + 1, currentNode.yPos + 1]);
        }
        //n
        if (currentNode.yPos - 1 >= 0)
            adjacentList.Add(nodeMap[currentNode.xPos, currentNode.yPos - 1]);
        //s
        if (currentNode.yPos + 1 < height)
            adjacentList.Add(nodeMap[currentNode.xPos, currentNode.yPos + 1]);

        return adjacentList;
    }

    /// <summary>
    /// Gets the total distance cost between two nodess
    /// </summary>
    /// <param name="a">this is the start node</param>
    /// <param name="b">this is the destation node</param>
    /// <returns>returns an int with the distance between the two nodes</returns>
    private int GetDistanceCost(Node a, Node b)
    {
        int xDistance = Mathf.Abs(a.xPos - b.xPos);
        int yDistance = Mathf.Abs(a.yPos - b.yPos);
        int remainder = Mathf.Abs(xDistance - yDistance);
        return diagonalCost * Mathf.Min(xDistance, yDistance) + straightCost * remainder;
    }

    /// <summary>
    /// Finds the cheapest f cost amongst a list of nodes
    /// </summary>
    /// <param name="nodeList">This is the list of nodes being passed in to find the cheapest one</param>
    /// <returns>returns the node with the lowest f cost</returns>
    private Node CheapestFNode(List<Node> nodeList)
    {
        Node cheapestF = nodeList[0];
        for(int i = 1; i < nodeList.Count; i++)
        {
            if(nodeList[i].fCost < cheapestF.fCost)
            {
                cheapestF = nodeList[i];
            }
        }
        return cheapestF;
    }
}
