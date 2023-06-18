using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NpcMovement
{
    [SerializeField] Rigidbody2D rigidBody2D;
    Transform player;
    [SerializeField] float speed;
    private Vector3 directionVector;
    private Vector2 previousPosition;
    [SerializeField] Vector2 attackSize = Vector2.one;
    [SerializeField] int damage;
    [SerializeField] float timeToAttack = 2f;
    private Animator anim;
    float attackTimer;
    public Transform character;

    void Start()
    {
        character = GameManager.instance.player.transform;
        anim = GetComponent<Animator>();
        player = GameManager.instance.player.transform;
        attackTimer = Random.Range(0, timeToAttack);
        rigidBody2D = GetComponent<Rigidbody2D>();
        previousPosition = rigidBody2D.position;
       
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, character.position);
        if (distance > 6f)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position,
            player.position,
            speed * Time.deltaTime);
        Attack();
        rigidBody2D.position = Vector3.MoveTowards(
          rigidBody2D.position, player.position, speed * Time.deltaTime
          );
        FollowsCharacter(ref directionVector, rigidBody2D, ref previousPosition, anim);
    }
    private void Attack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer > 0f) { return; }
        attackTimer = timeToAttack;


        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, attackSize, 0f);

        for (int i = 0; i < targets.Length; i++)
        {
            Character character = targets[i].GetComponent<Character>();
            if (character != null)
            {
                character.TakeDamage(damage);
            }
        }
        
    }
}
