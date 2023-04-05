using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
 {
[SerializeField] GameObject closedChest;
[SerializeField] GameObject openedChest;
[SerializeField] bool opened;
[SerializeField] ItemContainer itemContainer;

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
}
    