using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PropPlacementManager : MonoBehaviour
{
    DungeonData dungeonData;

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
        dungeonData = FindObjectOfType<DungeonData>();
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
            //We want to place only certain quantity of each prop
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
        if (dungeonData == null)
            return;
        foreach (Room room in dungeonData.Rooms)
        {
            if (roomEnemiesCount>0)
            {
            int enemiesCount = UnityEngine.Random.Range(3, roomEnemiesCount);
            roomEnemiesCount -= enemiesCount;
            PlaceEnemies(room, enemiesCount);
            }

            //Place props place props in the corners
            List<Prop> cornerProps = propsToPlace.Where(x => x.Corner).ToList();
            PlaceCornerProps(room, cornerProps);

            //Place props near LEFT wall
            List<Prop> leftWallProps = propsToPlace.Where(x => x.NearWallLeft).ToList();
            PlaceProps(room, leftWallProps, room.NearWallTilesLeft, PlacementOriginCorner.Left);

            //Place props near RIGHT wall
            List<Prop> rightWallProps = propsToPlace.Where(x => x.NearWallRight).ToList();
            PlaceProps(room, rightWallProps, room.NearWallTilesRight, PlacementOriginCorner.Right);

            //Place props near UP wall
            List<Prop> topWallProps = propsToPlace.Where(x => x.NearWallUP).ToList();
            PlaceProps(room, topWallProps, room.NearWallTilesUp, PlacementOriginCorner.Top);

            //Place props near DOWN wall
            List<Prop> downWallProps = propsToPlace.Where(x => x.NearWallDown).ToList();           
            //.OrderByDescending(x => x.PropSize.x * x.PropSize.y)
             PlaceProps(room, downWallProps, room.NearWallTilesDown, PlacementOriginCorner.Bottom);

            //Place inner props
            List<Prop> innerProps = propsToPlace.Where(x => x.Inner).ToList();
            PlaceInnerProps(room, innerProps, room.InnerTiles, PlacementOriginCorner.Inner);
        }

    }

    /// <param name="room"></param>
    /// <param name="wallProps">Props that we should try to place</param>
    /// <param name="availableTiles">Tiles that are near the specific wall</param>
    /// <param name="placement">How to place bigger props. Ex near top wall we want to start placemt from the Top corner and find if there are free spaces below</param>
    private void PlaceProps(Room room, List<Prop> props, HashSet<Vector2Int> availableTiles, PlacementOriginCorner placement)
    {
        //Remove path positions from the initial nearWallTiles to ensure the clear path to traverse dungeon
        HashSet<Vector2Int> tempPositons = new HashSet<Vector2Int>(availableTiles);

        tempPositons.ExceptWith(dungeonData.Path);

        //We will try to place all the props
        foreach (Prop propToPlace in props)
        {
            //We want to place only certain quantity of each prop
            int quantity= UnityEngine.Random.Range(propToPlace.PlacementQuantityMin, propToPlace.PlacementQuantityMax + 1);

            for (int i = 0; i < quantity; i++)
            {
                //remove taken positions
                tempPositons.ExceptWith(room.PropPositions);
                //shuffel the positions
                List<Vector2Int> availablePositions = tempPositons.ToList();
                //If placement has failed there is no point in trying to place the same prop again
                if (TryPlacingPropBruteForce(room, propToPlace, availablePositions, placement) == false)
                    break;
            }

        }
    }

    /// <summary>
    /// Tries to place the Prop using brute force (trying each available tile position)
    /// </summary>
    /// <param name="room"></param>
    /// <param name="propToPlace"></param>
    /// <param name="availablePositions"></param>
    /// <param name="placement"></param>
    /// <returns>False if there is no space. True if placement was successful</returns>
    private bool TryPlacingPropBruteForce( Room room, Prop propToPlace, List<Vector2Int> availablePositions, PlacementOriginCorner placement)
    {
        //try placing the objects starting from the corner specified by the placement parameter
        for (int i = 0; i < availablePositions.Count; i++)
        {
            //select the specified position (but it can be already taken after placing the corner props as a group)
            Vector2Int position = availablePositions[i];
            if (room.PropPositions.Contains(position))
                continue;

            //check if there is enough space around to fit the prop
            List<Vector2Int> freePositionsAround
                = TryToFitProp(propToPlace, availablePositions, position, placement);

            //If we have enough spaces place the prop
            if (freePositionsAround.Count == propToPlace.PropSize.x * propToPlace.PropSize.y)
            {
                //Place the gameobject
                PlacePropGameObjectAt(room, position, propToPlace);
                //Lock all the positions recquired by the prop (based on its size)
                foreach (Vector2Int pos in freePositionsAround)
                {
                    //Hashest will ignore duplicate positions
                    room.PropPositions.Add(pos);
                }
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if the prop will fit (accordig to it size)
    /// </summary>
    /// <param name="prop"></param>
    /// <param name="availablePositions"></param>
    /// <param name="originPosition"></param>
    /// <param name="placement"></param>
    /// <returns></returns>
    private List<Vector2Int> TryToFitProp(
        Prop prop,
        List<Vector2Int> availablePositions,
        Vector2Int originPosition,
        PlacementOriginCorner placement)
    {
        List<Vector2Int> freePositions = new();

        //Perform the specific loop depending on the PlacementOriginCorner
        if (placement == PlacementOriginCorner.Left)
        {
            for (int xOffset = 0; xOffset < prop.PropSize.x; xOffset++)
            {
                for (int yOffset = 0; yOffset < prop.PropSize.y; yOffset++)
                {
                    Vector2Int tempPos = originPosition + new Vector2Int(xOffset, yOffset);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }
        else if (placement == PlacementOriginCorner.Right)
        {
            for (int xOffset = -prop.PropSize.x + 1; xOffset <= 0; xOffset++)
            {
                for (int yOffset = 0; yOffset < prop.PropSize.y; yOffset++)
                {
                    Vector2Int tempPos = originPosition + new Vector2Int(xOffset, yOffset);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }
        else if (placement == PlacementOriginCorner.Top)
        {
            for (int xOffset = 0; xOffset < prop.PropSize.x; xOffset++)
            {
                for (int yOffset = -prop.PropSize.y + 1; yOffset <= 0; yOffset++)
                {
                    Vector2Int tempPos = originPosition + new Vector2Int(xOffset, yOffset);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }
        else
        {
            for (int xOffset = -prop.PropSize.x + 1; xOffset <= 0; xOffset++)
            {
                for (int yOffset = -prop.PropSize.y + 1; yOffset <= 0; yOffset++)
                {
                    var aux = UnityEngine.Random.Range(0, availablePositions.Count);


                    Vector2Int tempPos = originPosition + availablePositions.ElementAt(aux);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }

        return freePositions;
    }

    /// <summary>
    /// Places props in the corners of the room
    /// </summary>
    /// <param name="room"></param>
    /// <param name="cornerProps"></param>
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

    /// <summary>
    /// Place a prop as a new GameObject at a specified position
    /// </summary>
    /// <param name="room"></param>
    /// <param name="placementPostion"></param>
    /// <param name="propToPlace"></param>
    /// <returns></returns>
    private GameObject PlacePropGameObjectAt(Room room, Vector2Int placementPostion, Prop propToPlace)
    {
        //Instantiat the prop at this positon
        GameObject prop = Instantiate(propToPlace.PropPrefab);
       // SpriteRenderer propSpriteRenderer = prop.GetComponentInChildren<SpriteRenderer>();

        ////set the sprite
        //propSpriteRenderer.sprite = propToPlace.PropSprite;

        //Add a collider
        //CapsuleCollider2D collider
        //    = prop.gameObject.AddComponent<CapsuleCollider2D>();
        //collider.offset = Vector2.zero;
        //if (propToPlace.PropSize.x > propToPlace.PropSize.y)
        //{
        //    collider.direction = CapsuleDirection2D.Horizontal;
        //}
        //Vector2 size
        //    = new Vector2(propToPlace.PropSize.x * 0.8f, propToPlace.PropSize.y * 0.8f);
        //collider.size = size;

        prop.transform.localPosition = (Vector2)placementPostion;

        //Save the prop in the room data (so in the dunbgeon data)
        room.PropPositions.Add(placementPostion);
        room.PropObjectReferences.Add(prop);
        return prop;
    }

    private void OnDestroy()
    {
        dungeonData.Reset();
    }
}

/// <summary>
/// Where to start placing the prop ex. start at BottomLeft corner and search 
/// if there are free space to the Right and Up in case of placing a biggex prop
/// </summary>
public enum PlacementOriginCorner
{
    Bottom,
    Left,
    Top,
    Right,
    Inner
}