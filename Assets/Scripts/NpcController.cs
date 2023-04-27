using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : Interactable
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
        ChangeDirection();
        previousPosition = rigidBody2D.position;
    }
    void Update()
    {
        
        if (tame == false)
        {
        Move();
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

        UpdateAnimationTame();
    }

    void UpdateAnimationTame()
    {
        Vector2 direction = (rigidBody2D.position - previousPosition).normalized;
        float x = Mathf.Abs(direction.x);
        float y = Mathf.Abs(direction.y);

        if (x > y)
        {
            if (direction.x > 0)
            {
                directionVector = Vector2.right;
            }
            else if (direction.x < 0)
            {
                directionVector = Vector2.left;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                directionVector = Vector2.up;
            }
            else if (direction.y < 0)
            {
                directionVector = Vector2.down;
            }
        }

        UpdateAnimation();
        previousPosition = rigidBody2D.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tame == false) { 
            Vector3 temp = directionVector;
            ChangeDirection();
            int loops = 0;
            while (temp == directionVector && loops < 100)
            {
                loops++;
                ChangeDirection();
            }
        }
    }

    

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
        rigidBody2D.MovePosition(temp);
    }


    private void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
           
        while (currentDirection == direction)
        {
            direction = Random.Range(0, 4);
        }
        currentDirection = direction;
        

        switch (direction)
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
