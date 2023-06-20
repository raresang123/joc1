using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterInteractController : MonoBehaviour
{
    PlayerController character;
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeReference] Notification notification; 
    private void Awake()
    {
        character = GetComponent<PlayerController>();
        rgbd2d=GetComponent<Rigidbody2D>();
       
    }

    private void Update()
    {
       // Check();
        if (Input.GetMouseButtonDown(1))
        {
            Interact();
        }
    }

   

    public void Interact()
    {
        Vector2 position = rgbd2d.position + character.movement * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach (Collider2D c in colliders)
        {       
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (c.OverlapPoint(mousePosition))
                {
                    Interactable hit = c.GetComponent<Interactable>();
                    if (hit != null)
                    {
                        hit.Interact(character);
                        break;
                    }
                }
        }
    }
    //private void Check()
    //{
    //    Vector2 position = rgbd2d.position + character.movement * offsetDistance;
    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
      
    //    foreach (Collider2D c in colliders)
    //    {
    
    //            Interactable hit = c.GetComponent<Interactable>();
    //            if (hit != null)
    //            {
    //                notification.NotificationFunction(hit.gameObject);
    //                return;
    //            }
            
    //    }
    //   notification.NotificationFunctionof();
    //}

 }
