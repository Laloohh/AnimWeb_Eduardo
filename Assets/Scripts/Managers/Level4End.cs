using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4End : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            // llamar a una funcion que de retroalimentacio al usuario por terminar el nivel
            GameManager.nextlLevel = 5;
        }
    }
}
