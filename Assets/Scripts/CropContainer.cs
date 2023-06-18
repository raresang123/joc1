using System.Collections;

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Crops Container")]
public class CropContainer : ScriptableObject
{
   public List<CropTile> crops;
    public CropTile Get(Vector3Int position)
    {
        return crops.Find(x => x.position == position);
    }

    public void Delete(Vector3Int position)
    {
        int index= crops.FindIndex(x => x.position == position);
        crops.RemoveAt(index);
    }

    public void Add(CropTile crop) 
    {
      crops.Add(crop);
    }
        
}
