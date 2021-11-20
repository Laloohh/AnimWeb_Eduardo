using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public float VidaEnemigo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Bullet")
        {
            VidaEnemigo--;
            if(VidaEnemigo<=0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("WinScene");
            }
        }
    }
}
