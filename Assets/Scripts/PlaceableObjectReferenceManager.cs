using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectReferenceManager : MonoBehaviour
{
   public PlaceableObjectManager placeableObjectsManager;

    public void Place(Item item, Vector3Int pos)
    {
        if (placeableObjectsManager == null)
        {
            Debug.LogWarning("adssad");
            return;
        }
        placeableObjectsManager.Place(item, pos);
    }
}
