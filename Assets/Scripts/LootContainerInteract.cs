using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable, IPersistant
 {
[SerializeField] GameObject closedChest;
[SerializeField] GameObject openedChest;
[SerializeField] bool opened;
[SerializeField] ItemContainer itemContainer;

    private void Start()
    {
        if (itemContainer == null)
        {
            Init();
        }
    }

    private void Init()
    {

        itemContainer = (ItemContainer)ScriptableObject.CreateInstance(typeof(ItemContainer));
        itemContainer.Init();

    }

    public override void Interact(PlayerController character)
 {
     if(opened == false)
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
        closedChest.SetActive(false);
        openedChest.SetActive(true);
        character.GetComponent<ItemContainerInteractController>().Open(itemContainer, transform);
    }
    public void Close(PlayerController character)
    {
        opened = false;
        closedChest.SetActive(true);
        openedChest.SetActive(false);
        character.GetComponent<ItemContainerInteractController>().Close();
    }

    [Serializable]
    public class SaveLootItemData
    {
        public int itemId;
        public int count;
        public SaveLootItemData(int id,int c)
        {
            itemId = id;
            count = c;
        }
    }

    [Serializable]
    public class ToSave
    {
        public List<SaveLootItemData> itemDatas;
        public ToSave()
        {
            itemDatas = new List<SaveLootItemData>();
        }
    }
    public string Read()
    {
        ToSave toSave = new ToSave();
        for(int i = 0; i < itemContainer.slots.Count; i++)
        {
            if (itemContainer.slots[i].item == null)
            {
                toSave.itemDatas.Add(new SaveLootItemData(-1,0));
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
        if (jsonString =="" || jsonString =="{}" || jsonString == null) { return; }
        if (itemContainer == null)
        {
            Init();
        }
        ToSave toLoad = JsonUtility.FromJson<ToSave>(jsonString);
        for(int i = 0; i < toLoad.itemDatas.Count; i++)
        {
            if (toLoad.itemDatas[i].itemId == -1)
            {
                itemContainer.slots[i].Clear();
            }
            else
            {
                itemContainer.slots[i].item = GameManager.instance.itemDB.items[toLoad.itemDatas[i].itemId-1];
                itemContainer.slots[i].count = toLoad.itemDatas[i].count;
            }
        }
    }
}
    