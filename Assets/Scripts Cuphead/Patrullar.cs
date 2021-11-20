using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    public GameObject StartPoint;
    public GameObject EndPoint;
    public float vel;
    private bool estaDerecha;
    // Start is called before the first frame update
    void Start()
    {
        if(!estaDerecha)
        {
            transform.position = StartPoint.transform.position;
        }
        else
        {
            transform.position = EndPoint.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!estaDerecha)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                EndPoint.transform.position,
                vel * Time.deltaTime);
            if(transform.position == EndPoint.transform.position)
            {
                estaDerecha = true;
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        if(estaDerecha)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                StartPoint.transform.position,
                vel * Time.deltaTime);
            if(transform.position==StartPoint.transform.position)
            {
                estaDerecha = false;
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}
