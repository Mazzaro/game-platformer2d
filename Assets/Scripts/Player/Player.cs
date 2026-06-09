using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using System.ComponentModel;

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

    [Header("Player Animation")]
    public string boolRun = "Run";
    public Animator animator;
    public float playerSwipeDuration = .1f;
    private float _facingDirection = 1f;

    private float _currentSpeed;
    private bool _isLanding;


    void Update()
    {
        HandleJump();
        HandleMovement();
    }

    void LateUpdate()
    {
        ApplyFacingDirection();
    }

    public void HandleMovement()
    {
        
        if(Input.GetKey(KeyCode.LeftControl)){
            _currentSpeed = speedRun;
            animator.speed = 1.5f;
        }
        else{
            _currentSpeed = speed;
            animator.speed = 1.5f;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SetFacingDirection(-1f);

            myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
            animator.SetBool(boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            SetFacingDirection(1f);

            myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
            animator.SetBool(boolRun, true);
        }
        else
        {
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            animator.SetBool(boolRun, false);
        }

    }

    private void SetFacingDirection(float direction)
    {
        _facingDirection = direction;
        ApplyFacingDirection();
    }

    private void ApplyFacingDirection()
    {
        Transform playerTransform = myRigidbody.transform;

        Vector3 currentScale = playerTransform.localScale;

        float scaleX = Mathf.Abs(currentScale.x);

        if (scaleX < 0.01f)
        {
            scaleX = 1f;
        }

        currentScale.x = scaleX * _facingDirection;

        playerTransform.localScale = currentScale;
    }

    public void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);

            Transform playerTransform = myRigidbody.transform;

            DOTween.Kill(playerTransform);

            playerTransform.localScale = new Vector3(_facingDirection, 1f, 1f);

            HandleScaleJump(_facingDirection);
        }
    }

    public void HandleScaleJump(float direction)
    {
        Transform playerTransform = myRigidbody.transform;
        playerTransform.DOKill();

        playerTransform.DOScaleX(direction * jumpScaleX, animDuration).SetLoops(2, LoopType.Yoyo);
        playerTransform.DOScaleY(jumpScaleY, animDuration).SetLoops(2, LoopType.Yoyo);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && !_isLanding)
        {
            _isLanding = true;

            Transform playerTransform = myRigidbody.transform;
            float direction = _facingDirection;

            playerTransform.DOKill();

            Sequence squash = DOTween.Sequence();

            squash.Append(playerTransform.DOScale(new Vector3(direction * 1.15f, 0.75f, 1), .17f).SetEase(ease));
            squash.Append(playerTransform.DOScale(new Vector3(direction * 1f, 1.3f, 1), .17f).SetEase(ease));
            squash.Append(playerTransform.DOScale(new Vector3(direction * 1f, 1f, 1), animDuration));

            squash.OnComplete(() =>
            {
                _isLanding = false;
            });
        }
    }
}
