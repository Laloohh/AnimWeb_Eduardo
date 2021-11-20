using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBehavior : MonoBehaviour
{
    public int enemyHP, enemyPower;
    public float checkRadious, movement, speed;
    public Transform groundCheck, edgeCheck, obstacleCheck;
    public LayerMask whatIsGrounded, whatIsDefault;
    public GameObject enemyDiePrefab;

    bool isGrounded, isEdged, isObstacle;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            moveForward();
        }
        // aqui va lo de checar si esta vivo o moricion ()
    }

    void moveForward()
    {
        rb2d.velocity = new Vector2(movement * speed, rb2d.velocity.y);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadious, whatIsGrounded);
        isEdged = !Physics2D.OverlapCircle(edgeCheck.position, checkRadious, whatIsGrounded);
        isObstacle = Physics2D.OverlapCircle(obstacleCheck.position, checkRadious, whatIsDefault);

        if (isGrounded && (isEdged))
        {
            flip();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            collision.GetComponent<PlayerController>().getDamage(gameObject, enemyPower);
        }
    }

    // void moricion () {
    // aqui va lo de destruir el enemigo y crear en su lugar la animacion
    // de explosion
    // }

    void flip()
    {
        rb2d.velocity = Vector2.zero;
        movement *= -1;
        transform.Rotate(0f, 180f, 0f);
        // isFacingRight = !isFacingRight;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadious);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(obstacleCheck.position, checkRadious);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(edgeCheck.position, checkRadious);

    }

    void enemyDie()
    {
        if (gameObject.name.Contains("Enemy3"))
        {
           //  Instantiate(ParticleEnemyDie, transform.position, Quaternion.identity);
            Destroy(gameObject);
            // llamar a la funcion publica de script enemyyoctupusbheavior que instancie la particula
        }
        Instantiate(enemyDiePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void getDamage(int damage)
    {
        enemyHP -= damage;
        if (enemyHP <= 0) {
            enemyDie();
        } else {
            //rb2d.AddForce(new Vector2(impactForce *
            //(collision.transform.position.x > transform.position.x ? -1 : 1)
            //, 0)
            // );
            // aqui va lo de la anim de recibir daño

        }

    }
}

