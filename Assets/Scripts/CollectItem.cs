using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private Transform myTransform;

    PlayerController character;
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        character = GetComponent<PlayerController>();
        rgbd2d = GetComponent<Rigidbody2D>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collect();
        }
    }
    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         if (Input.GetKeyDown(KeyCode.E))
         {
             float distance = Vector3.Distance(transform.position, character.position);
             if (distance > 2f)
             {

             }

         }*/
    private void Collect()
    {
        Vector2 position = rgbd2d.position + character.movement * offsetDistance;
    Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach (Collider2D c in colliders)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (c.OverlapPoint(mousePosition))
            {
                Interactable collect = c.GetComponent<Interactable>();
                if (collect != null)
                {
                  //  collect.Destroy();
                    break;
                }

            }
        }
    }
   
}
