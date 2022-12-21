using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private Rigidbody2D rigidbody2d;
   public Animator animator;

    private float speed ;
    public Vector2 movement ;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        speed = 5f ;
    
    }

    // Update is called once per frame
    void Update()
    {
       movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal",movement.x);
        animator.SetFloat("Vertical",movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

     private void FixedUpdate()
    {
       rigidbody2d.MovePosition(rigidbody2d.position + movement * speed * Time.fixedDeltaTime);
    }
}