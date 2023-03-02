using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherResourceNode : ToolAction
{
    public override bool OnApply(Vector2 worldPoint)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);
        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                return true;
            }
        }
        return false;
    }
}
