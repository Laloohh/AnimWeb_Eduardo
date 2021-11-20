using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Behavior : MonoBehaviour
{
    public Transform scopeAreaA, scopeAreaB, enemyShootPoint;
    public GameObject enemyBulletPreFrab;
    public LayerMask whatIsPlayer;

    bool isPlayerScoped;
    float timeToShoot, startTimeToShoot = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        timeToShoot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerScoped && (timeToShoot <= 0))
        {
            shoot();
            timeToShoot = startTimeToShoot;
        }
        else
        {
            timeToShoot -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        isPlayerScoped = Physics2D.OverlapArea(scopeAreaA.position, scopeAreaB.position, whatIsPlayer);
    }

    void shoot()
    {
        Instantiate(enemyBulletPreFrab, enemyShootPoint.position, enemyShootPoint.rotation);

    }
}
