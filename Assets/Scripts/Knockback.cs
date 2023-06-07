using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knocktime ;
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;
        if (other.gameObject.CompareTag("enemy"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if(enemy != null )
            {
                enemy.isKinematic = false;
                Vector2 direction = (enemy.transform.position -transform.position);
                direction = direction.normalized * thrust; 
                 enemy.AddForce(direction * thrust, ForceMode2D.Impulse);
                StartCoroutine(KnockTime(enemy));

            }

        }
    }
    private IEnumerator KnockTime(Rigidbody2D enemy)
    {
        if(enemy != null)
        {
            yield return new WaitForSeconds(knocktime);
            enemy.velocity = Vector2.zero;  
            enemy.isKinematic = true;

        }
    }

}
