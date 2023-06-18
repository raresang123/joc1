using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalNPC : NpcMovement
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
        ChangeDirection(ref currentDirection, ref directionVector, anim);
              
    }
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Vector3 temp = directionVector;
         ChangeDirection(ref currentDirection, ref directionVector, anim);
        int loops = 0;
        while(temp == directionVector && loops <100)
        {
            loops++;
            ChangeDirection(ref currentDirection, ref directionVector, anim);
        }
       
    }

    void Update()
    {
        Move(myTransform, directionVector,ref rigidBody2D, speed);
    }

  
}
