using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public Vector2 velocity;
    public float speed;
    public float speedRun;
    public float jumpForce = 2;
    public Vector2 friction = new Vector2(.1f, 0);

    private float _currentSpeed;

    void Update()
    {
        HandleJump();
        HandleMovement();
    }

    public void HandleMovement()
    {
        
        if(Input.GetKey(KeyCode.LeftControl))
            _currentSpeed = speedRun;
        else
            _currentSpeed = speed;


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidbody.MovePosition(myRigidbody.position - velocity * Time.deltaTime);
            myRigidbody.velocity =  new Vector2(-_currentSpeed, myRigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
            myRigidbody.velocity =  new Vector2(_currentSpeed, myRigidbody.velocity.y);
        }

        if (myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity += friction;
        }
        else if (myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity -= friction;
        }

    }

    public void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //myRigidbody.MovePosition(myRigidbody.position - velocity * Time.deltaTime);
            myRigidbody.velocity =  Vector2.up * jumpForce;
        }
    }
}
