using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapCropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] TileBase basee;


    Tilemap targetTilemap;
    [SerializeField] GameObject cropsSpritePrefab;
    [SerializeField] CropContainer container ;

    private void Start()
    {
        GameManager.instance.GetComponent<CropsManager>().cropsManager = this;
        targetTilemap = GetComponent<Tilemap>();
        onTimeTick += Tick;
        Init();
        VisualizeMap();
    
    }
  
    private void VisualizeMap()
    {
        for(int i = 0; i<container.crops.Count; i++)
        {
            VisualizeTile(container.crops[i]);
        }
    }
    private void OnDestroy()
    {
        for(int i=0; i< container.crops.Count;i++)
        {
            container.crops[i].renderer = null ;
        }
    }
    public void Tick()
    {
        if (targetTilemap == null) { return; }
            foreach (CropTile cropTile in container.crops )
        {
            if(cropTile.crop == null) {continue; }

             if (cropTile.Complete)
            {
              
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
        return container.Get(position) != null;
    }


    public void Plow(Vector3Int position)
            
    {
        if(Check(position) == true ) { return; }
        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        CropTile tile = container.Get(position);
        if(tile == null ) { return ;}

        targetTilemap.SetTile(position, seeded);
        tile.crop = toSeed;
    }
    
    private void CreatePlowedTile(Vector3Int position)
    {
     
        CropTile crop = new CropTile();
        container.Add(crop);
        crop.position = position;
        VisualizeTile(crop);
        targetTilemap.SetTile(position, plowed);
    }

     public void VisualizeTile(CropTile cropTile)
    {

           // targetTilemap.SetTile(cropTile.position, cropTile.crop != null ? seeded : plowed);
            targetTilemap.SetTile(cropTile.position,  basee);



        if (cropTile.renderer == null)
        {
             GameObject go=Instantiate(cropsSpritePrefab);
             go.transform.position=targetTilemap.CellToWorld(cropTile.position);
             go.transform.position -= Vector3.forward * 0.01f;
             cropTile.renderer=go.GetComponent<SpriteRenderer>();
        }

        bool growing = cropTile.crop != null && cropTile.growTimer >= cropTile.crop.growthStageTime[0];
        cropTile.renderer.gameObject.SetActive(growing);
        if(growing == true)
        {
             cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage-1];
        }    
        
    }
    

    internal void PickUp(Vector3Int gridPosition)
    {
 
        Vector2Int position = (Vector2Int)gridPosition ;

        CropTile tile = container.Get(gridPosition);
        if (tile == null ) { return;}
        if (tile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), tile.crop.yield, tile.crop.count );
         
            tile.Harvested();
            VisualizeTile(tile);
            container.Delete(tile.position);

        }
    }   
}
