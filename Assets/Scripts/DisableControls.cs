using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableControls : MonoBehaviour
{
    PlayerController characterController;
    ToolsCharacterController toolsCharacterController;
    InventoryController inventoryController;
    ToolbarController toolbarController;
    ItemContainerInteractController itemContainerInteractController;
    private void Awake()
    {
        characterController = GetComponent<PlayerController>();
        toolsCharacterController = GetComponent<ToolsCharacterController>();
        inventoryController = GetComponent<InventoryController>();
        toolbarController = GetComponent<ToolbarController>();
        itemContainerInteractController = GetComponent<ItemContainerInteractController>();

    }

    public void DisableControl()
    {
        characterController.enabled = false;
        toolsCharacterController.enabled = false;
        inventoryController.enabled = false;
        toolbarController.enabled = false;
        itemContainerInteractController.enabled = false;
    }
    public void EnableControl()
    {
        characterController.enabled = true;
        toolsCharacterController.enabled = true;
        inventoryController.enabled = true;
        toolbarController.enabled = true;
        itemContainerInteractController.enabled = true;
    }
}
