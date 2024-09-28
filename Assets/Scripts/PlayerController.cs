using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public float moveSpeed, jumpForce;
    public float velocity;
    private bool isGrounded;

    public LayerMask whatIsGround;

    public Transform groundCheckPoint;

    public Animator anim;

    public bool isKeyboard2;

    public float timeBetweenAttack = .25f;

    private float attackCounter;
    private float speedStore, gravityStore;
    public GameObject swooshWeapon;

    [HideInInspector] public float powerupCounter;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.instance.AddPlayer(this);
        speedStore = moveSpeed;
        gravityStore = playerRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isKeyboard2) {
            velocity = 0f;
            
            if (Keyboard.current.lKey.isPressed) {
                velocity = 1f;
            }

            if (Keyboard.current.jKey.isPressed) {
                velocity = -1f;
            }

            if (isGrounded && Keyboard.current.rightShiftKey.wasPressedThisFrame && Time.timeScale != 0) {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                AudioManager.instance.PlaySFX(1);

                if (Keyboard.current.rightShiftKey.wasReleasedThisFrame && !isGrounded && playerRigidbody.velocity.y > 0) {
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y * .5f);
                }
            }

            if (!isGrounded && Keyboard.current.rightShiftKey.wasReleasedThisFrame && playerRigidbody.velocity.y > 0) {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y * .5f);
            }

            if (Keyboard.current.enterKey.wasPressedThisFrame && Time.timeScale != 0) {
                anim.SetTrigger("attack");
                AudioManager.instance.PlaySFX(0);
                Debug.Log("attack1");
            }
        }


        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
        playerRigidbody.velocity = new Vector2(velocity * moveSpeed, playerRigidbody.velocity.y);

        // if (Input.GetButtonDown("Jump")) {
        //     playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
        // }

        //Don't animate player left-right run animation when game is on the pause
        if (Time.timeScale !=0)
        {
            anim.SetBool("isGrounded2", isGrounded);;
            anim.SetFloat("xSpeed", Mathf.Abs(playerRigidbody.velocity.x));
            anim.SetFloat("ySpeed", playerRigidbody.velocity.y);

            // Update player animation
            if (playerRigidbody.velocity.x < 0) {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            } else if (playerRigidbody.velocity.x > 0) {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        


        // Freeze when attacking
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;

            playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
        }

        if (powerupCounter > 0)
        {
            powerupCounter -= Time.deltaTime;
            if (powerupCounter <= 0)
            {
                moveSpeed = speedStore;
                playerRigidbody.gravityScale = gravityStore;
            }
        }
    }

    public void Move(InputAction.CallbackContext context) {
        velocity = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context) {
        if (Time.timeScale != 0)
        {
            if (context.started && isGrounded) 
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                AudioManager.instance.PlaySFX(1);
            }

            // Make small jump
            if (context.canceled && !isGrounded && playerRigidbody.velocity.y > 0) 
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y * .5f);
            }
        }
    }

    public void Attack(InputAction.CallbackContext context) {
        if (context.started && Time.timeScale != 0) 
        {
            anim.SetTrigger("attack");
            AudioManager.instance.PlaySFX(0);
            attackCounter = timeBetweenAttack;
            Debug.Log("attack1");
        }
    }

    public void PlayerReset()
    {
        moveSpeed = speedStore;
        playerRigidbody.gravityScale = gravityStore;
        swooshWeapon.SetActive(false);
    }
}
