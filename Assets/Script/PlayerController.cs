using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed = 15;

    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 2.5f;//The distance between 2 lanes

    //public bool isGrounded;
    //public LayerMask groundLayer;
    public Transform groundCheck;
   
    public float jumpForce;
    public float Gravity = -20;

    public Animator animator;
    private bool isSliding = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        if (!PlayerManager.isGameStarted)
            Time.timeScale = 0;

        if(forwardSpeed < maxSpeed)//speed
        forwardSpeed += 0.1f * Time.deltaTime;

        direction.z = forwardSpeed;

        //check grounded then jump
        if (controller.isGrounded)
        {
            //isGrounded = true;
            direction.y = -1;
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

       /* if(SwipeManager.swipeDown)
        {
            StartCoroutine(Slide());
        }*/

        playerMove();
    }

    private void FixedUpdate()
    {
       
        controller.Move(direction * Time.deltaTime);
    }

    private void playerMove()
    {
        //gather input for lane switching

        if (SwipeManager.swipeRight)//Input.GetKeyDown(KeyCode.RightArrow
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }


        //calculate next lane

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //transform.position = targetPosition;
        if (transform.position == targetPosition) //fix collide with obstacle
            return;

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
        //controller.center = controller.center;
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            Debug.Log("lose");
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
            
        }
    }

    /*private IEnumerator Slide()
    {
        animator.SetBool("isSliding", true);

        yield return new WaitForSeconds(1.3f);

        animator.SetBool("isSliding", false);
    }*/
}
