using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    public float rotateSpeed;

    public static int bones, minBones;

    private Vector3 moveDirection;

    public CharacterController charController;
    private Camera mainCam;
    public GameObject playerModel;
    public Animator anim;

    public static bool skullEnabled, armsEnabled, everythingEnabled;

    public bool isKnocking;
    public float knockBackLength = 0.5f;
    private float knockBackCounter;
    public Vector2 knockbackPower;

    public GameObject[] playerPieces;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mainCam = Camera.main;
        armsEnabled = false;
        everythingEnabled = false;
        bones = 1;
    }

    void Update()
    {
        if (bones <= 5)
        {
            skullEnabled = true;
            armsEnabled = false;
            everythingEnabled = false;
        }

        if (bones >= 6 && bones <= 10)
        {
            skullEnabled = false;
            armsEnabled = true;
            everythingEnabled = false;
        }

        if (bones >= 11)
        {
            skullEnabled = false;
            armsEnabled = false;
            everythingEnabled = true;
        }

        if (skullEnabled)
        {
            moveSpeed = 3f;
            jumpForce = 0f;
            HealthManager.currentHealth = 1;
        }

        if (armsEnabled)
        {
            moveSpeed = 5f;
            jumpForce = 10f;
            HealthManager.currentHealth = 2;
        }

        if (everythingEnabled)
        {
            moveSpeed = 10f;
            jumpForce = 15f;
            HealthManager.currentHealth = 3;
        }

        if (!isKnocking)
        {
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = 0f;

                if (everythingEnabled)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                    moveDirection.y = jumpForce;
                    }
                }

                if (armsEnabled)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                    moveDirection.y = jumpForce;
                    }
                }
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, mainCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }   
        }

        if (isKnocking)
        {
            knockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockbackPower.x;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = 0f;
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            if (knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim.SetBool("Grounded", charController.isGrounded);
    }

    public void Knockback()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        moveDirection.y = knockbackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }
}
