using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerManager : MonoBehaviour
{
  [SerializeField] Tilemap targetTilemap;
    Tile tile;
    public Vector3Int markedCellPosition;
  Vector3Int oldCellPosition;
  bool show;
    [SerializeField] Tile basetile;

    private void Update()
    {

    if(show == false )
    {
        return;
    }
    targetTilemap.SetTile(oldCellPosition, null);
    targetTilemap.SetTile(markedCellPosition, tile);
    oldCellPosition = markedCellPosition;
    }
  internal void Show(bool selectable)
  {
    show = selectable;
    targetTilemap.gameObject.SetActive(show);
  }
    public void ChangeMarker(Tile newMarker)
    {
        if (newMarker)
        {
            tile = new Tile(); tile = newMarker;
        }
        else
        {
            tile = basetile; 
        }
       
    } 
}
