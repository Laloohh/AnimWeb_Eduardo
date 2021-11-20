using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public float vel;
    public float tiempoVida;
    public GameObject player;
    Transform transformPlayer;
    SpriteRenderer renderPlayer;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        transformPlayer = player.transform;
    }
    void Start()
    {
        if(transformPlayer.localScale.x>0)//viendo a la derecha
        {
            rb.velocity = new Vector2(vel, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        } 
        else if (transformPlayer.localScale.x < 0)//viendo a la izquierda
        {
            rb.velocity = new Vector2(-vel, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Update()
    {
        Destroy(gameObject, tiempoVida);
    }
}
