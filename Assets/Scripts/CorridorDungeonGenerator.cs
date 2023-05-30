using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLenght = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;
        
    protected override void RunProceduralGeneration()
    {
        CorridorDungeonGeneration(); 
    }
    private void CorridorDungeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        CreateCorridors(floorPositions);
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions)
    {
        var currntPositions = startPosition;

        for(int i = 0; i< corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currntPositions, corridorCount);
            currntPositions = corridor[corridor.Count - 1];
            floorPositions.UnionWith(corridor);
        }

    }
}
