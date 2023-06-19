using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomDataExtractor : MonoBehaviour
{
    private DungeonData dungeonData;

    public UnityEvent OnFinishedRoomProcessing;

    private void Awake()
    {
        dungeonData = FindObjectOfType<DungeonData>();
    }
    public void ProcessRooms()
    {
        if (dungeonData == null)
            return;
        dungeonData.Path.ExceptWith(dungeonData.PathCorridor);

        foreach (Room room in dungeonData.Rooms)
        {
            room.FloorTiles.ExceptWith(dungeonData.PathCorridor);
            //find corener, near wall and inner tiles
            foreach (Vector2Int tilePosition in room.FloorTiles)
            {
                int neighboursCount = 4;

                if (room.FloorTiles.Contains(tilePosition + Vector2Int.up) == false)
                {
                    room.NearWallTilesUp.Add(tilePosition);
                    neighboursCount--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.down) == false)
                {
                    room.NearWallTilesDown.Add(tilePosition);
                    neighboursCount--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.right) == false)
                {
                    room.NearWallTilesRight.Add(tilePosition);
                    neighboursCount--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.left) == false)
                {
                    room.NearWallTilesLeft.Add(tilePosition);
                    neighboursCount--;
                }

                //find corners
                if (neighboursCount <= 2)
                    room.CornerTiles.Add(tilePosition);

                if (neighboursCount == 4)
                    room.InnerTiles.Add(tilePosition);
            }

            room.NearWallTilesUp.ExceptWith(room.CornerTiles);
            room.NearWallTilesDown.ExceptWith(room.CornerTiles);
            room.NearWallTilesLeft.ExceptWith(room.CornerTiles);
            room.NearWallTilesRight.ExceptWith(room.CornerTiles);
        }

    }
}