using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TownTerrain : MonoBehaviour
{
    public int xWidth;
    public int yWidth;
    public Tile[] tileArray;
    Grid tileGrid;
    [SerializeField]
    Tilemap groundLayer;
    
    // Start is called before the first frame update
    void Start()
    {
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
            }
        }
        GenerateRiver();
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
            else if (startPoint > 16)
                startPoint -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
