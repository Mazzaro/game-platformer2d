using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Speed Setup")]
    public Rigidbody2D myRigidbody;
    public Vector2 velocity;
    public float speed;
    public float speedRun;
    public float jumpForce = 2;
    public Vector2 friction = new Vector2(.1f, 0);

    [Header("Animation Setup")]
    public float jumpScaleY = 1.1f;
     public float jumpScaleX = 1.1f;
    public float animDuration = .3f;
    public Ease ease = Ease.OutQuad;

    private float _currentSpeed;
    private bool _isLanding;


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
            myRigidbody.velocity =  Vector2.up * jumpForce;
            myRigidbody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidbody.transform);
            HandleScaleJump();
        }
    }

    public void HandleScaleJump()
    {
        myRigidbody.transform.DOScaleX(jumpScaleX, animDuration).SetLoops(2, LoopType.Yoyo);
        myRigidbody.transform.DOScaleY(0.8f, animDuration).SetLoops(2, LoopType.Yoyo);
        myRigidbody.transform.DOScaleY(jumpScaleY, animDuration).SetLoops(2, LoopType.Yoyo);
        myRigidbody.transform.DOScaleX(0.8f, animDuration).SetLoops(2, LoopType.Yoyo);
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")&& !_isLanding)
        {
            _isLanding = true;

            myRigidbody.transform.DOKill();
            Sequence squash = DOTween.Sequence();

            squash.Append(myRigidbody.transform.DOScale(new Vector3(1.15f, 0.75f, 1), .17f).SetEase(ease));
            squash.Append(myRigidbody.transform.DOScale(new Vector3(1f, 1.3f, 1), .17f).SetEase(ease));

            squash.Append(myRigidbody.transform.DOScale(Vector3.one, animDuration));

            squash.OnComplete(() =>
            {
                _isLanding = false;
            });
        }
    }
}
