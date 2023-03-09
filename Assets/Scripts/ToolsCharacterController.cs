using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsCharacterController : MonoBehaviour
{
    PlayerController character;
    Rigidbody2D rgbd2d;
    ToolbarController toolbarController;
   [SerializeField] float offsetDistance = 1f;
   [SerializeField] float sizeOfInteractableArea = 1.2f; 
   [SerializeField] MarkerManager MarkerManager;
   [SerializeField] TileMapReadController tileMapReadController;
   [SerializeField] float maxDistance = 1.5f;
   [SerializeField] CropsManager cropsManager;
   [SerializeField] TileData plowableTiles;
   

    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake() 
    {
        character = GetComponent<PlayerController>();
        rgbd2d = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<ToolbarController>();
    }
    private void Update()
    {   
        SelectTile();
        CanSelectCheck();
        Marker();
        if (Input.GetMouseButtonDown(0))
        {
            if(UseToolWorld() == true)
            {
                return;
            }
            UseToolGrid();

        }
    }
    private void SelectTile()
    {
       selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);

    }

     void CanSelectCheck()
     {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);   
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        MarkerManager.Show(selectable);
     }

    private void Marker()

    {
       
       MarkerManager.markedCellPosition =  selectedTilePosition;
    }

    private bool UseToolWorld()
    {
        Vector2 position = rgbd2d.position + character.movement * offsetDistance;
        Item item=toolbarController.GetItem;
        if (item == null) { return false;}
        if (item.onAction == null) { return false;}
        bool complete=item.onAction.OnApply(position);
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        //foreach (Collider2D c in colliders)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //        if (c.OverlapPoint(mousePosition))
        //        {
        //            ToolHit hit = c.GetComponent<ToolHit>();
        //            if (hit != null)
        //            {
        //                hit.Hit();
        //               return true;
        //            }
        //        }
        //    }

        //}
        return complete;
    }

    private void UseToolGrid()
    {
         if(selectable == true)
         {

            Item item = toolbarController.GetItem;
            if (item == null) { return ; }
            if (item.onTileMapAction == null) { return; }
            bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition,tileMapReadController);
            // TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);
            //TileData tileData = tileMapReadController.GetTileData(tileBase);


            //if( tileData != plowableTiles)
            //{
            //    return;
            //}

            //if(cropsManager.Check(selectedTilePosition))
            //{
            //    cropsManager.Seed(selectedTilePosition);
            //}
            //else
            //{
            //    cropsManager.Plow(selectedTilePosition);
            //}

        }
    }
}

