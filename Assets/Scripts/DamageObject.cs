using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : ToolHit
{
    [SerializeField] int dropCount = 3;
    [SerializeField] float spread = 0.5f;
    [SerializeField] Item item ;
    [SerializeField] int itemCountInOneDrop =1;
    [SerializeField] ResourceNodeType nodeType;
    [SerializeField] int nrHits = 2;
    public Rigidbody2D enemy;



    private void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    public override void Hit()
    {
        if (nrHits == 0)
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
        else nrHits--;

    }
    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }

}
