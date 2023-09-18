using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLogic : MonoBehaviour
{
    private Rigidbody rb;
    public float walkspeed = 0.25f, runspeed = 0.5f, jumppower = 10f, fallspeed = 5f, airMultiplier;
    public Transform PlayerOrientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    bool grounded = true, aerialboost = true;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        PlayerOrientation = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    public void Movement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = PlayerOrientation.forward * verticalInput + PlayerOrientation.right * horizontalInput;

        if (grounded && moveDirection != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                rb.AddForce(moveDirection.normalized * runspeed * 10f, ForceMode.Force);
            } else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                rb.AddForce(moveDirection.normalized * walkspeed * 10f, ForceMode.Force);
            }
        } else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.y, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumppower, ForceMode.Impulse);
            grounded = false;
            anim.SetBool("Jump", true);
        } else if (!grounded)
        {
            rb.AddForce(Vector3.down * fallspeed * rb.mass, ForceMode.Force);
            if (aerialboost)
            {
                rb.AddForce(moveDirection.normalized * walkspeed * 10f * airMultiplier, ForceMode.Impulse);
                aerialboost = false;
            }
        }
    }

    public void groundedchanger()
    {
        grounded = true;
        aerialboost = true;
        anim.SetBool("Jump", false);
    }
}
