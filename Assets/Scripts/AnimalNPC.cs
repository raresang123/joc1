using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalNPC : Interactable
{
    private Vector3 directionVector;
    private Transform myTransform; 
    public float speed = 5;
    public Rigidbody2D rigidBody2D;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        ChangeDirection();
              
    }
    void Collide()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(myTransform.position, 0.4f);
        if (colliders.Length  >1)
        {
            ChangeDirection();
        }
       
    }

    void Update()
    {
        Move();
        Collide();
    }

    private void Move()
    {
        rigidBody2D.MovePosition(myTransform.position + directionVector * speed * Time.deltaTime);
    }
     private void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch(direction)
        {

            case 0:
                directionVector = Vector3.right;
                break;
            case 1:
                directionVector = Vector3.left;
                break;
            case 2:
                directionVector = Vector3.up;
                break;
            case 3:
                directionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }
   void UpdateAnimation()
   {
       
      anim.SetFloat("Horizontal", directionVector.x);
      anim.SetFloat("Vertical", directionVector.y);
   }
}
