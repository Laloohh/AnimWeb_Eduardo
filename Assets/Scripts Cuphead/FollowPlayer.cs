using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float suvizado = 5f;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nuevaPosicion = player.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            nuevaPosicion,
            suvizado * Time.deltaTime);
    }
}
