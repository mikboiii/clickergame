using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDungeon : MonoBehaviour
{
    /* Dungeon idea:
     * linear room structure - array? List?
     * room contents are randomised, except bosses or key characteristic rooms
     * use weighted random selection for this
     * enemies should be clickable to damage them
     * hero passively moves through dungeon and attacks enemies and uses special abilities
     */

    public DungeonRoom[] dungeonRooms;
    //public List<DungeonRoom>
    public int numberOfRooms;
    public float combatRoomWeight;
    public float lootRoomWeight;
    public float totalWeight;

    private void GenerateDungeon()
    {
        dungeonRooms = new DungeonRoom[numberOfRooms];
    }

    private void SelectRoom()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class DungeonRoom
{
    public enum roomType
    {
        empty,
        loot,
        combat,
        boss,
    }

    bool hasEnemies;

    float weight;
}
