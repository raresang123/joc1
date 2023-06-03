using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb2d;
    Transform player;
    [SerializeField] float speed;

    [SerializeField] Vector2 attackSize = Vector2.one;
    [SerializeField] int damage;
    [SerializeField] float timeToAttack = 2f;
    float attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player.transform;
        attackTimer = Random.Range(0, timeToAttack);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position,
            player.position,
            speed * Time.deltaTime);
        Attack();
        


    }
    private void Knockback()
    {
        Vector2 direction = (transform.position - player.position).normalized;
        rb2d.AddForce(direction * 16, ForceMode2D.Impulse);
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
                Knockback();
            }
        }
        
    }
}
