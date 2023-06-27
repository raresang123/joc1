using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerInteractController : MonoBehaviour
{
    ItemContainer targetItemContainer;
    InventoryController inventoryController;
    [SerializeField] ItemContainerPanel itemContainerPanel;
    Transform openedChest;
    [SerializeField] float maxDistance = 0.8f;
    [SerializeField] GameObject panelhands;


    private void Awake()
    {
        inventoryController = GetComponent<InventoryController>();
    }
    private void Update()
    {
            if(openedChest != null)
        {
            float distance = Vector2.Distance(openedChest.position, transform.position);

            if(distance > maxDistance || Input.GetKeyDown(KeyCode.Tab))

            {
               openedChest.GetComponent<LootContainerInteract>().Close(GetComponent<PlayerController>());

                
            }

        }
    }

    public void Open(ItemContainer itemContainer,Transform _openedChest)
    {
        targetItemContainer = itemContainer;
        itemContainerPanel.inventory = targetItemContainer;
        inventoryController.Open();
        CloseHS();
        itemContainerPanel.gameObject.SetActive(true);
        openedChest = _openedChest;
    }
    public void Close()
    {
        inventoryController.Close();
        OpenHS();
        itemContainerPanel.gameObject.SetActive(false);
        openedChest = null;
    }
    public void CloseHS()
    {
        panelhands.SetActive(false);

    }
    public void OpenHS()
    {
        panelhands.SetActive(true);

    }
}
