using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private ParticleSystem tireSmoke;
    [SerializeField] private AudioClip crashSound;

    [Header("Movement Settings")]
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Speed Settings")]
    [SerializeField] public float speed = 5f;
    [SerializeField] private float turnSmoothTime = 0.1f;

    private float turnSoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;
    private CharacterController controller;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        PlayerFail.OnPlayerFail += CrashBike;
    }

    private void OnDisable()
    {
        PlayerFail.OnPlayerFail -= CrashBike;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        DriveScooter();
    }

    private void DriveScooter()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //if (direction.magnitude >= 0.1f)
        //{
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

            if (!tireSmoke.isPlaying)
            {
                tireSmoke.Play();
            }
        //}
        //else
        //{
        //    if (tireSmoke.isPlaying)
        //    {
        //        tireSmoke.Stop();
        //    }
        //    audioSource.Pause();
        //}
    }

    private void CrashBike()
    {
        if(tireSmoke.isPlaying)
        {
            tireSmoke.Stop();
        }

        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(crashSound);
        this.enabled = false;
    }
}
