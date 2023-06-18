using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{

    public virtual void Interact(PlayerController character)
    {

    }
    public void UpdateAnimation(Animator anim, Vector3 directionVector)
    {
        anim.SetFloat("Horizontal", directionVector.x);
        anim.SetFloat("Vertical", directionVector.y);

    }
    public void ChangeDirection(ref int currentDirection, ref Vector3 directionVector, Animator anim)
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
        UpdateAnimation(anim, directionVector);
    }
    public void Move( Transform myTransform,Vector3 directionVector,ref Rigidbody2D rigidBody2D,float speed)
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
        rigidBody2D.MovePosition(temp);
    }
    public void FollowsCharacter(ref Vector3 directionVector, Rigidbody2D rb2d,ref Vector2 previousPosition, Animator anim)
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

        UpdateAnimation(anim, directionVector);
        previousPosition = rb2d.position;
    }
}
