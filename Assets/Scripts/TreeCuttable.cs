using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class TreeCuttable : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 3;
    [SerializeField] float spread = 0.5f;
    [SerializeField] Item item ;
    [SerializeField] int itemCountInOneDrop =1;
    [SerializeField] ResourceNodeType nodeType;
    int damageTree = 2;

    public override void Hit()
    {
        if (damageTree == 0)
        {
            while (dropCount > 0)
            {
                dropCount -= 1;
                Vector3 position = transform.position;
                position.x += spread * UnityEngine.Random.value - spread / 2;
                position.y += spread * UnityEngine.Random.value - spread / 2;
                ItemSpawnManager.instance.SpawnItem(position, item, itemCountInOneDrop);
            }
            Destroy(gameObject);

        }
        else damageTree--;

    }
    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }

}
