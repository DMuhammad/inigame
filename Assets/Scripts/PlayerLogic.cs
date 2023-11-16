using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic: MonoBehaviour
{
    [Header("Player Setting")]
    public Transform PlayerOrientation;
    public CameraLogic camlogic;
    public UIGameplayLogic gameplaylogic;
    private Rigidbody rb;
    public Animator anim;
    public float walkspeed = 0.05f, runspeed = 0.1f, jumppower = 5f, fallspeed = 2.5f, airMultiplier, HitPoints = 100f;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    bool grounded = true, aerialboost = true, AimMode = false, TPSMode = true;

    float maxhealth;

    [Header("SFX")]
    public AudioClip ShootAudio;
    public AudioClip StepAudio;
    public AudioClip DeathAudio;
    AudioSource PlayerAudio;



    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        PlayerOrientation = this.GetComponent<Transform>();
        PlayerAudio = this.GetComponent<AudioSource>();
        maxhealth = HitPoints;
        gameplaylogic.UpdateHealthBar(HitPoints, maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        AimModeAdjuster();
        ShootLogic();

        if (Input.GetKey(KeyCode.F))
        {
            PlayerGetHit(100f);
        }
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
                anim.SetBool("AimMode", false);
                rb.AddForce(moveDirection.normalized * runspeed * 10f, ForceMode.Force);
            } else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                anim.SetBool("AimMode", false);
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
            anim.SetBool("AimMode", false);
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

    public void AimModeAdjuster()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (AimMode)
            {
                TPSMode = true;
                AimMode = false;
                anim.SetBool("AimMode", false);
            } else if (TPSMode)
            {
                TPSMode = false; 
                AimMode = true;
                anim.SetBool("AimMode", true);
            }
            camlogic.CameraModeChanger(TPSMode, AimMode);
        }
    }

    public void ShootLogic()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            PlayerAudio.clip = ShootAudio;
            PlayerAudio.Play();
            if (moveDirection.normalized != Vector3.zero)
            {
                anim.SetBool("WalkShoot", true);
                //anim.SetBool("Walk", false);
                anim.SetBool("IdleShoot", false);
            } else
            {
                anim.SetBool("IdleShoot", true);
                anim.SetBool("WalkShoot", false);
            }
        } else
        {
            anim.SetBool("WalkShoot", false);
            anim.SetBool("IdleShoot", false);
        }
    }

    public void PlayerGetHit(float damage)
    {
        Debug.Log("Player Receive Damage - " + damage);
        HitPoints = HitPoints - damage;
        gameplaylogic.UpdateHealthBar(HitPoints, maxhealth);
        anim.SetTrigger("GetHit");

        if (HitPoints == 0f)
        {
            PlayerAudio.clip = DeathAudio;
            PlayerAudio.Play();
            anim.SetBool("Death", true);
        }
    }

    public void step()
    {
        Debug.Log("Step");
        PlayerAudio.clip = StepAudio;
        PlayerAudio.Play();
    }
}
