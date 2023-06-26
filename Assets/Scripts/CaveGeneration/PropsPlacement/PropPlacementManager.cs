using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PropPlacementManager : MonoBehaviour
{
    CaveData caveData;

    [SerializeField]
    private List<Prop> propsToPlace;

    [SerializeField, Range(0, 1)]
    private float cornerPropPlacementChance = 0.7f;


    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private int roomEnemiesCount;

    private void Awake()
    {
        caveData = FindObjectOfType<CaveData>();
    }

    private void PlaceEnemies(Room room, int enemiesCount)
    {
        for (int k = 0; k < enemiesCount; k++)
        {
            var aux = UnityEngine.Random.Range(0, room.InnerTiles.Count);
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.localPosition =new Vector3( room.InnerTiles.ElementAt(aux).x, room.InnerTiles.ElementAt(aux).y,0);
            room.EnemiesInTheRoom.Add(enemy);  
            room.InnerTiles.Remove(room.InnerTiles.ElementAt(aux));
        }
    }

    private void PlaceInnerProps(Room room, List<Prop> props, HashSet<Vector2Int> availableTiles, PlacementOriginCorner placement)
    {
        foreach (Prop propToPlace in props)
        {

            int quantity = UnityEngine.Random.Range(propToPlace.PlacementQuantityMin, propToPlace.PlacementQuantityMax + 1);

            for (int i = 0; i < quantity; i++)
            {
                try
                {
                var aux = UnityEngine.Random.Range(0, room.InnerTiles.Count);
                PlacePropGameObjectAt(room, room.InnerTiles.ElementAt(aux), propToPlace);
                room.InnerTiles.Remove(room.InnerTiles.ElementAt(aux));
                }
                catch(Exception x) { }
                
            }

        }
    }

    public void ProcessRooms()
    {
        if (caveData == null)
            return;
        foreach (Room room in caveData.Rooms)
        {
            if (roomEnemiesCount>0)
            {
            int enemiesCount = UnityEngine.Random.Range(3, roomEnemiesCount);
            PlaceEnemies(room, enemiesCount);
            }

            List<Prop> cornerProps = propsToPlace.Where(x => x.Corner).ToList();
            PlaceCornerProps(room, cornerProps);

            List<Prop> leftWallProps = propsToPlace.Where(x => x.NearWallLeft).ToList();
            PlaceProps(room, leftWallProps, room.NearWallTilesLeft, PlacementOriginCorner.Left);

            List<Prop> rightWallProps = propsToPlace.Where(x => x.NearWallRight).ToList();
            PlaceProps(room, rightWallProps, room.NearWallTilesRight, PlacementOriginCorner.Right);

            List<Prop> topWallProps = propsToPlace.Where(x => x.NearWallUP).ToList();
            PlaceProps(room, topWallProps, room.NearWallTilesUp, PlacementOriginCorner.Top);

            List<Prop> downWallProps = propsToPlace.Where(x => x.NearWallDown).ToList();           
             PlaceProps(room, downWallProps, room.NearWallTilesDown, PlacementOriginCorner.Bottom);

            List<Prop> innerProps = propsToPlace.Where(x => x.Inner).ToList();
            PlaceInnerProps(room, innerProps, room.InnerTiles, PlacementOriginCorner.Inner);
        }

    }

    private void PlaceProps(Room room, List<Prop> props, HashSet<Vector2Int> availableTiles, PlacementOriginCorner placement)
    {
       
        HashSet<Vector2Int> tempPositons = new HashSet<Vector2Int>(availableTiles);

        tempPositons.ExceptWith(caveData.Path);

        
        foreach (Prop propToPlace in props)
        {
          
            int quantity= UnityEngine.Random.Range(propToPlace.PlacementQuantityMin, propToPlace.PlacementQuantityMax + 1);

            for (int i = 0; i < quantity; i++)
            {
                
                tempPositons.ExceptWith(room.PropPositions);
                
                List<Vector2Int> availablePositions = tempPositons.ToList();

                for (int j = 0; j < availablePositions.Count; j++)
                {
                    Vector2Int position = availablePositions[j];
                    if (room.PropPositions.Contains(position))
                        continue;
                    PlacePropGameObjectAt(room, position, propToPlace);
                    room.PropPositions.Add(position);
                }
            }

        }
    }

    private void PlaceCornerProps(Room room, List<Prop> cornerProps)
    {
        float tempChance = cornerPropPlacementChance;
        foreach (Vector2Int cornerTile in room.CornerTiles)
        {
            if (UnityEngine.Random.value < tempChance)
            {
                Prop propToPlace
                    = cornerProps[UnityEngine.Random.Range(0, cornerProps.Count)];
                PlacePropGameObjectAt(room, cornerTile, propToPlace);
            }
            else
            {
                tempChance = Mathf.Clamp01(tempChance + 0.1f);
            }
        }
    }
    private GameObject PlacePropGameObjectAt(Room room, Vector2Int placementPostion, Prop propToPlace)
    {
        GameObject prop = Instantiate(propToPlace.PropPrefab);
        prop.transform.localPosition = (Vector2)placementPostion;
        room.PropPositions.Add(placementPostion);
        room.PropObjectReferences.Add(prop);
        return prop;
    }

    private void OnDestroy()
    {
        caveData.Reset();
    }
}

public enum PlacementOriginCorner
{
    Bottom,
    Left,
    Top,
    Right,
    Inner
}