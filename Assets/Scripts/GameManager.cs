using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

     private void Awake() 
   
     {
        instance = this;  
     }
     
     public GameObject player; 
     public ItemContainer inventoryContainer;
     public InventoryDragAndDrop1 dragAndDropController;
     public DayTimeController timeController;
     public PlaceableObjectReferenceManager placeableObjects;
    public ScreenTint screeTint;
    public ItemList itemDB;
    public OnScreenMessageSystem messageSystem;

}
