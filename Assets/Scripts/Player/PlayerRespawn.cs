using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public GameObject diePrefab;
    public Transform pointToRespawn;

    GameObject diePrefabVariable; 
    SpriteRenderer sr; //---------------
    bool isFirstFase, isSecondFase;
    float timeFF, timeSF, timeToFirstFase, timeToSecondFase;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();  //---------------
        isFirstFase = false;
        isSecondFase = false; 
        timeToFirstFase = 0.5f;
        timeToSecondFase = 0.5f;
        timeFF = timeToFirstFase;
        timeSF = timeToSecondFase;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstFase) {   //---------------
            // timeFF -= Time.deltaTime;
            // if (timeFF < 0) {
                firstFase();
                timeFF = timeToFirstFase;
                isFirstFase = false;
                isSecondFase = true;
            // }
        }  else if (isSecondFase) {
            timeSF -= Time.deltaTime;
            if (timeSF < 0) {
                secondFase();
                timeSF = timeToSecondFase;
                isSecondFase = false;
            }
        }
    }

    public void respawn () {
        isFirstFase = true;
    }

    void firstFase() {
        sr.enabled = false;
        diePrefabVariable = Instantiate(diePrefab, transform.position, Quaternion.identity);
    }

    void secondFase() {
        Destroy(diePrefabVariable);
        transform.position = pointToRespawn.position;
        sr.enabled = true;
        GameManager.isPlayerAlive = true;
    }
}
