using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLogic : MonoBehaviour
{
    private Rigidbody rb;
    public float walkspeed = 0.5f, runspeed = 1f, jumppower = 10f, fallspeed = 5f;
    private Transform PlayerOrientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    bool grounded = true;

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(moveDirection.normalized * runspeed * 10f, ForceMode.Force);
        } else
        {
            rb.AddForce(moveDirection.normalized * walkspeed * 10f, ForceMode.Force);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            //rb.AddForce(transform.up * jumppower, ForceMode.Impulse);
            rb.velocity = new Vector3(0f, jumppower, 0f);
            grounded = false;
        } else if (!grounded)
        {
            //rb.AddForce(Vector3.down * fallspeed * rb.mass, ForceMode.Force);
            rb.velocity = new Vector3(0f, -fallspeed, 0f);
        }
    }

    public void groundedchanger()
    {
        grounded = true;
    }
}
