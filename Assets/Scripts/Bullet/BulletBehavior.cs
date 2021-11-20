using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {
    public GameObject impactPreFab;
    public float speed = 20f;
    public int damage = 1;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<EnemyBasicBehavior>().getDamage(damage);
        }
        Instantiate(impactPreFab, transform.position, Quaternion.identity);
        Destroy(gameObject); // Crear pool de objetos
    }
}
