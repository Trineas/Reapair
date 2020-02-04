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

    public static int bones, maxBones;

    private Vector3 moveDirection;

    public CharacterController charController;
    private Camera mainCam;
    public GameObject[] playerModels;
    public Animator anim1, anim2, anim3;

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
        maxBones = 11;
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

        if (bones >= 17)
        {
            bones = maxBones;
        }

        if (skullEnabled)
        {
            moveSpeed = 4.5f;
            jumpForce = 0f;
            charController.center = new Vector3(0f, 0.35f, 0f);
            charController.radius = 0.34f;
            charController.height = 0.5f;
        }

        if (armsEnabled)
        {
            moveSpeed = 5.5f;
            jumpForce = 10f;
            charController.center = new Vector3(0f, 0.80f, 0f);
            charController.radius = 0.34f;
            charController.height = 1.75f;
        }

        if (everythingEnabled)
        {
            moveSpeed = 7.5f;
            jumpForce = 15f;
            charController.center = new Vector3(0f, 1.45f, 0f);
            charController.radius = 0.34f;
            charController.height = 2.75f;
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
                playerModels[0].transform.rotation = Quaternion.Slerp(playerModels[0].transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
                playerModels[1].transform.rotation = Quaternion.Slerp(playerModels[1].transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
                playerModels[2].transform.rotation = Quaternion.Slerp(playerModels[2].transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }   
        }

        if (isKnocking)
        {
            knockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModels[0].transform.forward * -knockbackPower.x;
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

        anim1.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim2.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim3.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));

        anim1.SetBool("Grounded", charController.isGrounded);
        anim2.SetBool("Grounded", charController.isGrounded);
        anim3.SetBool("Grounded", charController.isGrounded);
    }

    public void Knockback()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        moveDirection.y = knockbackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }
}
