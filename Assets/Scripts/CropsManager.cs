
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public  class CropTile
{
    public int growTimer;
    public int growStage;
    public Crop crop;
    public SpriteRenderer renderer;
    public float damage;
    public bool Complete
    {
        get
        {
            if(crop == null ) { return false; }
            return growTimer >= crop.timeToGrow ;
        }
    }
    internal void Harvested()
    {
          growTimer = 0;
          growStage= 0 ;
          crop = null ;
        renderer.gameObject.SetActive(false);
    }
}

public class CropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] GameObject cropsSpritePrefab;


    Dictionary<Vector2Int, CropTile> cropTile;

    private void Start()
    {
        cropTile = new Dictionary<Vector2Int, CropTile>();
        onTimeTick += Tick;
        Init();
         
    }
     public void Tick()
    {
        foreach (CropTile cropTile in cropTile.Values )
        {
            if(cropTile.crop == null) {continue; }

             if (cropTile.Complete)


            {
                Debug.Log("dfsfds");
                continue; 
            }
            cropTile.growTimer += 1;
            if(cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
               
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                cropTile.growStage +=1;

            }
        }    
    }

    public bool Check(Vector3Int position)
    {
       return cropTile.ContainsKey((Vector2Int)position);
    }


    public void Plow(Vector3Int position)
    {
         if(cropTile.ContainsKey((Vector2Int)position))
         {
            return;
         }

         CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
       targetTilemap.SetTile(position, seeded);
        cropTile[(Vector2Int)position].crop = toSeed;
    }
    
    private void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();
        cropTile.Add((Vector2Int)position, crop);

        GameObject go=Instantiate(cropsSpritePrefab);
        go.transform.position=targetTilemap.CellToWorld(position);
        go.SetActive(false);
        crop.renderer=go.GetComponent<SpriteRenderer>();
        targetTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition ;
         if( cropTile.ContainsKey(position) == false) { return;}
         CropTile cropTilee = cropTile[position];
        if (cropTilee.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), cropTilee.crop.yield, cropTilee.crop.count );
            targetTilemap.SetTile(gridPosition, plowed);
            cropTilee.Harvested();
        }
    }
}
