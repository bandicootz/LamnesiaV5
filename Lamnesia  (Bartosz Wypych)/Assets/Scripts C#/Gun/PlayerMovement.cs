using System.Collections;
using System.Collections.Generic;
using Lamnesia.InGame.Managers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private Vector3 lastPos;
    private Vector3 currPos;
    private bool isJumping;
    public bool isDead;

    [Header("Sounds here")] 
    public AudioClip footStepsSound;
    public AudioClip jumpSound;


    private AudioSource audioSource;

    void Start()
    {
        currPos = gameObject.transform.position;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = footStepsSound;
    }
    
    void Update()
    {
        lastPos = currPos;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (!isDead)
        {
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            if (jumpSound != null) AudioManager.Instance.PlaySound(jumpSound);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        currPos = transform.position;

        if (currPos != lastPos)
        {
            if (!audioSource.isPlaying)
            audioSource.Play();

        }
    }
}
