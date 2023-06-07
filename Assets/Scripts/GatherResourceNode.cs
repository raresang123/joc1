using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
Undifined,
Tree,
Rock,
Enemy


}

[CreateAssetMenu(menuName = "Data/....")]

public class GatherResourceNode : ToolAction
     
{
    [SerializeField] float sizeOfInteractableArea = 1f;
    [SerializeField] List<ResourceNodeType> canHitNodeTypes;
    Animator anim;


    public override bool OnApply(Vector2 worldPoint)
        
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);
        foreach (Collider2D c in colliders)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (c.OverlapPoint(mousePosition))
                {
                    ToolHit hit = c.GetComponent<ToolHit>();
                    if (hit != null)
                    {
                        if(hit.CanBeHit(canHitNodeTypes)==true)
                        {
                            hit.Hit();
                            
                            return true;
                        }
                        
                    }
                }
            }


        }
        return false;
    }
}
