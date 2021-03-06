using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Source File: PlayerController.cs
 * Editors: Timothy Garcia
 * Student Number: 300898955
 * Date Modified: 02-16-2021
 * Program: 3109 - Game-Programming(Optional Co-op)
 * 
 * --------------------Revision History--------------------
 *                   Pre-Alpha - 2021.02.14
 * - Basic player avatar movement and animations completed
 * - Basic enemy AI movement and animation completed
 * - Damage Player functionality implemented (Work in Progress)
 */

public class PlayerController : MonoBehaviour
{
    [Header("Player Control Properties")]
    public CharacterController controller;
    public Animator animator;
    public GameObject playerModel;
    public PauseController pause;

    //Movement Variables
    public float movementSpeed;
    public float sprintSpeed;
    public float controllerMoveSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Transform pivot;
    public float rotationSpeed;

    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;

    public Vector3 velocity;
    public bool isGrounded;

    [Header("Player Health Properties")]
    public HealthBarController healthBar;
    public int currentHealth;
    public int maxHealth;
    //Helath Bar Functions
    public event Action<float> OnHealthPercentChanged = delegate { };

    [Header("Player Audio Properties")]
    //Audio variables
    public AudioClip swordSwing;
    public AudioClip parrySound;
    AudioSource playerAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        #region Keyboard control movement
        float yJump = velocity.y;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;

        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);
        controller.Move(move * movementSpeed * Time.deltaTime);
        velocity = move.normalized * movementSpeed;

        //if (z < 0)
        //{
        //    animator.Play("Player_WalkBack");
        //}
        #endregion

        #region JoyStick control movement
        float xController = Input.GetAxis("LeftJoyStickHorizontal");
        float zController = Input.GetAxis("LeftJoyStickVertical");

        Vector3 moveController = transform.right * xController - transform.forward * zController;
        controller.Move(moveController * controllerMoveSpeed * Time.deltaTime);
        velocity = moveController.normalized * controllerMoveSpeed;
        #endregion

        //Run function
        if (Input.GetButtonDown("Sprint"))
        {
            animator.SetBool("isSprinting", Input.GetButtonDown("Sprint"));
        }
        if(isGrounded && Input.GetButtonDown("Sprint") && z > 0)
        {
            movementSpeed = sprintSpeed;
            animator.SetBool("isSprinting", Input.GetButtonDown("Sprint"));

        }
        if (isGrounded && Input.GetButtonDown("Sprint") && z < 0)
        {
            movementSpeed = 3.0f;
            animator.SetBool("isSprinting", Input.GetButtonDown("Sprint"));

        }
        if (isGrounded && Input.GetButtonUp("Sprint") && z > 0)
        {
            movementSpeed = 3.0f;
            animator.SetBool("isSprinting", false);
        }
        if (isGrounded && Input.GetButtonUp("Sprint") && z < 0)
        {
            movementSpeed = 2.0f;
            animator.SetBool("isSprinting", false);
        }

        //Jump function
        velocity.y = yJump;
        if (Input.GetButton("Jump") && isGrounded || Input.GetButton("JoystickJump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Move player direction based on camera direction
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
        }

        //Animation parameter functions
        animator.SetBool("isGrounded", controller.isGrounded);
        animator.SetFloat("Speed", Input.GetAxis("Vertical") + Input.GetAxis("Horizontal") - (Input.GetAxis("LeftJoyStickVertical") + Input.GetAxis("LeftJoyStickHorizontal")));
        animator.SetFloat("SprintSpeed", Input.GetAxis("Vertical") + Input.GetAxis("Horizontal") + Input.GetAxis("LeftJoyStickVertical") + Input.GetAxis("LeftJoyStickHorizontal") + 1);

        

        #region Temporary Health Bar Function
        if (Input.GetKeyDown(KeyCode.P))
        {
            DamageHealth(10);
            healthBar.TakeDamage(10);
        }
        #endregion

        //Attack function
        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded || Input.GetKeyDown(KeyCode.Joystick1Button2) && isGrounded)
        {
            animator.SetBool("isAttacking", true);
            playerAudioSource.clip = swordSwing;
            playerAudioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && isGrounded || Input.GetKeyUp(KeyCode.Joystick1Button2) && isGrounded)
        {
            animator.SetBool("isAttacking", false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && isGrounded)
        {
            animator.SetBool("isParrying", true);
            playerAudioSource.clip = parrySound;
            playerAudioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && isGrounded)
        {
            animator.SetBool("isParrying", false);
        }

        if (pause.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SavingSys.SavePlayer(this);
            }
        }
    }
    public void DamageHealth(int amt)
    {
        currentHealth -= amt;
        //int currenthealthPercent = (int)currentHealth / (int)maxHealth;
        //OnHealthPercentChanged(currenthealthPercent);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Invoke("RestartScene", 5.0f);
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
