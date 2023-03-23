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
    private  int currentDirection; 
    
  


    void Start()
    {
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        ChangeDirection();
              
    }
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Vector3 temp = directionVector;
         ChangeDirection();
        int loops = 0;
        while(temp == directionVector && loops <100)
        {
            loops++;
            ChangeDirection();
        }
       
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {   
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
        rigidBody2D.MovePosition(temp);
    }   


     private void ChangeDirection()
    {
       
        int direction = Random.Range(0, 4);
         while( currentDirection == direction)
        {
            direction = Random.Range(0, 4);
        }
        currentDirection = direction ;
    
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
