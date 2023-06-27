using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Item")]

public class Item : ScriptableObject
{
    public string Name;
    public bool stackable;
    public Sprite icon ;
    public Tile tile;
    public ToolAction onAction;
    public ToolAction onTileMapAction;
    public ToolAction onItemUsed;
    public ToolAction onStatsUsed;
    public ToolAction onCombat;
    public Crop crop;
    public GameObject itemPrefab;
    public bool iconHighlight;
    public int id;
}
