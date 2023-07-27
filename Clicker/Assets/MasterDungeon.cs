using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDungeon : MonoBehaviour
{
    /* Dungeon idea:
     * linear room structure - array? List?
     * room contents are randomised, except bosses or key characteristic rooms
     * enemies should be clickable to damage them
     * hero passively moves through dungeon and attacks enemies and uses special abilities
     */

    public DungeonRoom[] dungeonRooms;

    private void GenerateDungeon(int numberOfRooms, float emptyRoomProb, float lootRoomProb, float combatRoomProb, int restRoomInterval)
    {
        dungeonRooms = new DungeonRoom[numberOfRooms];
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
        rest
    }

    bool hasEnemies;


}
