using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
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
        rb2d = GetComponent<Rigidbody2D>();
        previousPosition = rb2d.position;
       
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
        rb2d.position = Vector3.MoveTowards(
          rb2d.position, player.position, speed * Time.deltaTime
          );
        UpdateAnimationTame();
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
    void UpdateAnimationTame()
    {
        Vector2 direction = (rb2d.position - previousPosition).normalized;
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
        previousPosition = rb2d.position;
    }

    void UpdateAnimation()
    {
        anim.SetFloat("Horizontal", directionVector.x);
        anim.SetFloat("Vertical", directionVector.y);

    }
}
