using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.Tilemaps;

public class TownTerrain : MonoBehaviour
{
    public int xWidth;
    public int yWidth;
    public Tile[] tileArray;
    Grid tileGrid;
    [SerializeField]
    Tilemap groundLayer;
    Node[,] logicMap;
    int buildingIndex = 0;
    Vector2[] buildingPositions;
    public GameObject building;
    
    
    // Start is called before the first frame update
    void Start()
    {
        logicMap = new Node[xWidth, yWidth];
        buildingPositions = new Vector2[2];
        buildingIndex = 0;
        tileGrid = GetComponent<Grid>();
        GenerateGrid();


    }

    void GenerateGrid()
    {
        groundLayer.size = new Vector3Int(xWidth, yWidth, 1);
        for(int i = 0; i < xWidth; i++)
        {
            for(int j = 0; j < yWidth; j++)
            {
                groundLayer.SetTile(new Vector3Int(i, j, 1),tileArray[0]);
                logicMap[i, j] = new Node(i, j, false, 1);
            }
        }
        GenerateRiver();
        GenerateBuilding();
        buildingIndex += 1;
        GenerateBuilding();
        GeneratePath();
    }
    void GenerateRiver()
    {
        int startPoint = Random.Range(0, xWidth+1);
        for(int i = 0; i < yWidth; i++)
        {
            groundLayer.SetTile(new Vector3Int(startPoint, i, 1), tileArray[1]);
            startPoint += Random.Range(-1, 2);
            if (startPoint < 0)
                startPoint += 1;
            else if (startPoint > xWidth)
                startPoint -= 1;
        }
    }
    void GenerateBuilding()
    {
        bool started = true;
        while (started == true)
        {
            Vector3Int randomPos = new Vector3Int(Random.Range(0, xWidth + 1), Random.Range(0, yWidth + 1), 1);
            if(groundLayer.GetTile(randomPos) == tileArray[0])
            {
                started = false;
                groundLayer.SetTile(randomPos, tileArray[3]);
                logicMap[randomPos.x, randomPos.y] = new Node(randomPos.x, randomPos.y, false, 10);
                buildingPositions[buildingIndex] = new Vector2(randomPos.x, randomPos.y);
                Instantiate(building, tileGrid.CellToLocal(new Vector3Int(randomPos.x, randomPos.y, -2)) + new Vector3(-0.525f,0.375f,-2), Quaternion.identity);
            }
            else
            {
                continue;
            }
        }
        
    }
    void GeneratePath()
    {
        RoadPathfinding pathfind = new RoadPathfinding();
        List<Node> path = pathfind.FindPath(logicMap, buildingPositions[0], buildingPositions[1]);
        for(int i = 1; i < path.Count-1; i++)
        {
            groundLayer.SetTile(new Vector3Int(path[i].xPos, path[i].yPos, 1),tileArray[2]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
