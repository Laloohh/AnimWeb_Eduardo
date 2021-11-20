using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Joystick joystick;
    public Transform BulletSpawner;
    public GameObject BulletPrefab;
    public float salto;
    public float vel; //Declarar una variable pública vel de tipo punto flotante 
    Rigidbody2D rb; //Declarar una variable rb de tipo Cuerpo Rígido 2D
    bool voltearPlayer = true;//mi player ve a la derecha
    SpriteRenderer miPlayer;
    Animator animatorPlayer;
    public int numeroSaltos = 0;
    public int limiteSaltos = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        miPlayer = GetComponent<SpriteRenderer>();
        animatorPlayer = GetComponent<Animator>();
    }
    void Update()
    {
        //if (Input.GetAxis("Jump") > 0)//teclado
        if (joystick.Vertical> 0.2f)
        {
            if (numeroSaltos < limiteSaltos)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0, salto), ForceMode2D.Impulse);
                numeroSaltos++;
            }
        }
        //float mh = Input.GetAxis("Horizontal");//leemos las flechas izq der o teclas A D
        float mh = joystick.Horizontal;
        if (mh > 0.2f && !voltearPlayer)//si fue tecla D o flecha derecha y personaje esta viendo a la izq, voltealo
        {
            voltear();
        }
        else if (mh < -0.2f && voltearPlayer)//si fue tecla A o flecha izq y personaje esta viendo a la der, voltealo
        {
            voltear();
        }
        rb.velocity = new Vector2(mh * vel, rb.velocity.y);
        animatorPlayer.SetFloat("velMov", Mathf.Abs(mh));
        //playerShooting();
    }
    void voltear()
    {
        voltearPlayer = !voltearPlayer;
        transform.localScale = new Vector3(
            -transform.localScale.x,
            transform.localScale.y,
            transform.localScale.z);
        //miPlayer.flipX = !miPlayer.flipX;
    }
    public void playerShooting()
    {
        //if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(BulletPrefab, BulletSpawner.position, BulletSpawner.rotation);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Suelo")
        {
            numeroSaltos = 0;
        }
    }
}
