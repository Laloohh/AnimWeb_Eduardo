using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public float speed, jumpForce, checkRadious, dashSpeed, impactForce;
    public float startTimeToShoot, startTimeToDash, startTimeToResetDash;
    public int extraJumps, playerHP;
    public Transform groundCheck, shootPosition;
    public LayerMask whatIsGrounded;
    public GameObject bullet, dashTrial;
    public AudioClip audioJump, audioRun;

    bool isPlayerAlive, canPlayerControl, isFacingRight, isGrounded;
    bool isJumping;
    int extraJumpSet, playerLives, playerHPset;
    float moveInput, timeToShoot, timeToDash, timeToResetDash;
    Rigidbody2D rb2d;
    Animator anim;
    PlayerRespawn pr;
    AudioSource audioSource;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update    
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isPlayerAlive = true;
        canPlayerControl = true;
        isFacingRight = true;
        extraJumpSet = extraJumps;
        timeToShoot = 0;
        playerLives = 5; // Esto va a cambiar cuando guardemos el avance del jugador
        playerHPset = 10; // Esto va a cambiar cuando guardemos el avance del jugador
        playerHP = playerHPset;
        timeToDash = startTimeToDash;
        timeToResetDash = startTimeToResetDash;      
    }

    // Update is called once per frame
    void Update() {
        isPlayerAlive = GameManager.isPlayerAlive; 
        if (isPlayerAlive && !canPlayerControl) {
            rb2d.WakeUp();
            if (isGrounded) {
                canPlayerControl = true;
            }
            canPlayerControl = true;
        }
        if (isPlayerAlive && canPlayerControl) {
            if (Input.GetButtonDown("Jump") && (extraJumps > 0)) {           // JUMP
                jump();
            }

            if (Input.GetButtonDown("Fire3") && isGrounded && (timeToResetDash <= 0)) { // DASH
                dash();

            }

            if (isGrounded && (moveInput != 0) && !audioSource.isPlaying) {
                audioSource.PlayOneShot(audioRun);
            }

            // shoot
            if (timeToShoot <= 0) {
                if (Input.GetButtonDown("Fire1"))
                    shoot();
            }
            else {
                timeToShoot -= Time.deltaTime;
                anim.SetBool("Shooting", false);
            }
        } 

        if (timeToResetDash > 0) {
            timeToResetDash -= Time.deltaTime;
        }
    }

   



    // PARA CONTROLAR EL SALTO
    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadious, whatIsGrounded);
        if (isPlayerAlive && canPlayerControl) {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);

            // PARA CONTROLAR MOV HORIZONTAL
            if ((isFacingRight && (moveInput < 0)) || (!isFacingRight && (moveInput > 0)))  {
                flip();
            }


            if (isGrounded) {
                extraJumps = extraJumpSet;
                isJumping = false;
            }

            anim.SetBool("Grounded", isGrounded);
            anim.SetBool("Falling", (rb2d.velocity.y < -2));
            anim.SetFloat("SpeedX", Mathf.Abs(rb2d.velocity.x));
            anim.SetFloat("SpeedY", rb2d.velocity.y);   
        }

        if (anim.GetBool("Dashing")) {
            timeToDash -= Time.deltaTime;
            rb2d.velocity = new Vector2(dashSpeed * (isFacingRight ? 1 : -1), rb2d.velocity.y);
            canPlayerControl = false;
            if (timeToDash < 0) {
                dashTrial.SetActive(false);
                anim.SetBool("Dashing", false);
                timeToDash = startTimeToDash;
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("DeadZone")) {
            playerDie();
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D collisionRB2D)) {
                collisionRB2D.constraints =
                RigidbodyConstraints2D.FreezeRotation;//---------
            }
            if (collision.TryGetComponent<BoxCollider2D>(out BoxCollider2D colliderBoxC2D)) {
                colliderBoxC2D.isTrigger = false; // ----------
            }
        }
    }


    void jump () {
        rb2d.velocity = Vector2.up * jumpForce;
        extraJumps--;
        isJumping = true;
        // audioSource.clip = audioJump;
        audioSource.PlayOneShot(audioJump);
    }

    void dash () {
        dashTrial.SetActive(true);
        dashTrial.transform.localScale = new Vector3((isFacingRight ? 1 : -1), 1, 1);
        anim.SetBool("Dashing", true);
        canPlayerControl = false;
        timeToResetDash = startTimeToResetDash;
    }

    void playerDie() {
        rb2d.Sleep();
        playerLives--;
        if (playerLives < 0) {
            // dar retroalimen al usuario de que hizo la muricion
            GameManager.isPlayerAlive = false;
        }
        GetComponent<PlayerRespawn>().respawn();
        playerHP = playerHPset;
        isPlayerAlive = false;
        canPlayerControl = false;
    }

    void shoot() {
        // anim.SetTrigger("Stand");
        anim.SetTrigger("Shooting");
        Instantiate(bullet, shootPosition.position, shootPosition.rotation);
        timeToShoot = startTimeToShoot;
    }
    void flip()
    {
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
    }

    public void getDamage(GameObject collision, int damage) {
        if (anim.GetBool("Dashing")) {
            if (collision.CompareTag("Enemy")) {
                if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D collisionRB2D)) {
                    collisionRB2D.constraints =
                    RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
                if (collision.TryGetComponent<BoxCollider2D>(out BoxCollider2D colliderBoxC2D)) {
                    colliderBoxC2D.isTrigger = true;
                }
            }
        } else {
            playerHP-= damage;
            if (playerHP <= 0) {
                playerDie();
            } else {
                rb2d.AddForce(new Vector2(impactForce *
                   (collision.transform.position.x > transform.position.x ? -1 : 1)
                   , 0)
                    );
                // aqui va lo de la anim de recibir daño
            }
        }
    }


    void OnDrawGizmosSelected() {
       Gizmos.color = Color.red; 
       Gizmos.DrawWireSphere(groundCheck.position, checkRadious);
       Gizmos.color = Color.green;
       Gizmos.DrawWireSphere(shootPosition.position, checkRadious);

    }
}
