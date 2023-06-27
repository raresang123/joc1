using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FountainInteract : Interactable, IPersistant
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
     bool opened= false ;
    [SerializeField] ItemContainer itemContainer;
    [SerializeField]
    private List<GameObject> serializedObjectToSetActive;
    [SerializeField]
    private List<GameObject> serializedObjectToSetInactive;
    [SerializeField] Tilemap oldTreesTile;
    [SerializeField] Tile newTreesTile;
    [SerializeField] Tilemap oldTile;
    [SerializeField] Tile newFloorTile;
    [SerializeField] Tile oldFloorTile;
    [SerializeField] Tile oldPiceFloorTile;
    [SerializeField] Tile newPiceFloorTile;
    DayTimeController finalTime;


    private void Paint()
    {
        foreach (var position in oldTreesTile.cellBounds.allPositionsWithin)
           if (oldTreesTile.HasTile(position)) {
                oldTreesTile.SetTile(position, newTreesTile); }
        foreach (var position in oldTile.cellBounds.allPositionsWithin)
            if (oldTile.HasTile(position))
            {
                if (oldTile.GetTile(position) == oldPiceFloorTile)
                {
                    oldTile.SetTile(position, newPiceFloorTile);
                }
                if (oldTile.GetTile(position) == oldFloorTile)
                {
                    oldTile.SetTile(position, newFloorTile);
                }
                
            }

    }

    private void Start()
    {

        if (itemContainer == null)
        {
            Init();
        }
        finalTime=GameManager.FindObjectOfType<DayTimeController>();
    }

    private void Update()
    {
        CheckAllSlotsOccupiedAndSetActive();
        if(CheckAllSlots()==true)
        {
            
            finalTime.ShowFinalTime();
        }
    }

    private void Init()
    {

        itemContainer = (ItemContainer)ScriptableObject.CreateInstance(typeof(ItemContainer));
        itemContainer.Init();

    }

    public override void Interact(PlayerController character)
    {
        if (opened == false)
        {
           Open(character);

        }
        else
        {
            Close(character);
        }
    }
    public void Open(PlayerController character)
    {
        opened = true;
        character.GetComponent<ItemFountainController>().Open(itemContainer, transform);
    }
    public void Close(PlayerController character)
    {
        opened =false;
        character.GetComponent<ItemFountainController>().Close();
    }
   

    [Serializable]
    public class SaveLootItemData
    {
        public int itemId;
        public int count;
        public SaveLootItemData(int id, int c)
        {
            itemId = id;
            count = c;
        }

    }

    [Serializable]
    public class ToSave
    
    {
        [SerializeField]
       
        public List<SaveLootItemData> itemDatas;
        public ToSave()
        {
            itemDatas = new List<SaveLootItemData>();
        }
    }
    public string Read()
    {
        ToSave toSave = new ToSave();
   
        for (int i = 0; i < itemContainer.slots.Count; i++)
        {
            if (itemContainer.slots[i].item == null)
            {
                toSave.itemDatas.Add(new SaveLootItemData(-1, 0));
            }
            else
            {
                toSave.itemDatas.Add(new SaveLootItemData(
                    itemContainer.slots[i].item.id,
                    itemContainer.slots[i].count));
            }
        }
      
        return JsonUtility.ToJson(toSave);
    }

    public void Load(string jsonString)
    {
        if (jsonString == "" || jsonString == "{}" || jsonString == null) { return; }
        if (itemContainer == null)
        {
            Init();
        }
        ToSave toLoad = JsonUtility.FromJson<ToSave>(jsonString);
        for (int i = 0; i < toLoad.itemDatas.Count; i++)
        {
            if (toLoad.itemDatas[i].itemId == -1)
            {
                itemContainer.slots[i].Clear();
            }
            else
            {
                itemContainer.slots[i].item = GameManager.instance.itemDB.items[toLoad.itemDatas[i].itemId - 1];
                itemContainer.slots[i].count = toLoad.itemDatas[i].count;
            }
        }
    }
  
    public void CheckAllSlotsOccupiedAndSetActive()
    {
        for (int i = 0; i < itemContainer.slots.Count; i++)
        {
            if (itemContainer.slots[i].item) {
                if (itemContainer.slots[i].item.Name.Contains("Crystal") || itemContainer.slots[i].item.Name.Contains("Lemn") || itemContainer.slots[i].item.Name.Contains("Piatra"))
                {
                    if (serializedObjectToSetInactive[i])
                    {
                    serializedObjectToSetActive[i].SetActive(true); 
                    serializedObjectToSetInactive[i].SetActive(false);
                    }
                    else
                    {
                        serializedObjectToSetActive[i].SetActive(true);
                        Paint();
                    }

                }
            }
            else
            {
                try{
                serializedObjectToSetActive[i].SetActive(false);
                serializedObjectToSetInactive[i].SetActive(true);
                }
                catch(Exception x) { }
                
            }
            
        }
    }
    public bool CheckAllSlots()
    {
        for (int i = 0; i < itemContainer.slots.Count; i++)
        {
            if (itemContainer.slots[i].item==null)
            {
                
                    return false;
                
            }
            if (itemContainer.slots[i].item.Name.Contains("Crystal")==null && itemContainer.slots[i].item.Name.Contains("Lemn")==null && itemContainer.slots[i].item.Name.Contains("Piatra")==null)
            {
                return false;
            }
        }
        return true;
    }

}
