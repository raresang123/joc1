using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Tool Action/Heal")]
public class ItemsCharacterStats : ToolAction
{
    public override void OnItemUsed(Item useItem, ItemContainer inventory, Character characterStats)
    {
        inventory.Remove(useItem);
        characterStats.Heal(50);
    }
}
