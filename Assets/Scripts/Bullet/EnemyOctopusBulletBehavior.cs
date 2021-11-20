using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOctopusBulletBehavior : MonoBehaviour
{

    public GameObject impactPreFab;
    public float speed = 20f;
    public int damage = 5;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;


    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Equals("Player")) {
            collision.gameObject.GetComponent<PlayerController>().getDamage(gameObject, damage);
        }
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Bullet")) {
            Instantiate(impactPreFab, transform.position, Quaternion.identity);
            Destroy(gameObject); // Crear pool de objetos
        }
    }
}
