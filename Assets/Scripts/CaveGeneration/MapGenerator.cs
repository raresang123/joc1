using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    CorridorDungeonGenerator generatorMap;
    RoomDataExtractor roomsExtractor;
    PropPlacementManager propsPlacement;
    private void Start()
    {
        generatorMap = FindObjectOfType<CorridorDungeonGenerator>();
        roomsExtractor = gameObject.AddComponent<RoomDataExtractor>();
        propsPlacement = FindObjectOfType<PropPlacementManager>();
        Generate();
    }
    private void Generate()
    {
        generatorMap.GenerateDungeon();
        roomsExtractor.ProcessRooms();
        propsPlacement.ProcessRooms();
    }
}
