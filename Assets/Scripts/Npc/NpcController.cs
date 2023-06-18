using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : NpcMovement
{
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed = 5;

    public Transform character;
    public Rigidbody2D rigidBody2D;

    private Animator anim;
    private int currentDirection;
    [SerializeField] float maxDistance = 1.5f;
    private bool selectable;
    private bool tame=false;
    private Vector2 previousPosition;
   

    void Start()
    {
        character = GameManager.instance.player.transform;
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        ChangeDirection(ref currentDirection, ref directionVector,anim);
        previousPosition = rigidBody2D.position;
    }
    void Update()
    {
        
        if (tame == false)
        {
        Move(myTransform,directionVector,ref rigidBody2D, speed);
        }
        if(tame== true)
        {
            FollowCharacter();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, character.position);
            if (distance > 2f)
            {
                return;
            }
            tame = true;
            return;
        }
        


    }

    private void FollowCharacter() 
    {
        float distance = Vector3.Distance(transform.position, character.position);
        if (distance < 1.5f)
        {
            return;
        }
        rigidBody2D.position = Vector3.MoveTowards(
           rigidBody2D.position, character.position, speed * Time.deltaTime
           );

        FollowsCharacter(ref directionVector, rigidBody2D, ref previousPosition, anim);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tame == false) { 
            Vector3 temp = directionVector;
            ChangeDirection(ref currentDirection, ref directionVector,anim);
            int loops = 0;
            while (temp == directionVector && loops < 100)
            {
                loops++;
                ChangeDirection(ref currentDirection, ref directionVector, anim);
            }
        }
    }
}
