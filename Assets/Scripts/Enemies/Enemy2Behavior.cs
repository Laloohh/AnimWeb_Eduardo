using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Behavior : MonoBehaviour
{
    public Transform scopeAreaA, scopeAreaB, scopeAreaC, scopeAreaD, enemyShootPoint, enemyShootPoint2;
    public GameObject enemyBulletPreFrab;
    public LayerMask whatIsPlayer;

    bool isPlayerScoped;
    bool isPlayerScoped2;
    float timeToShoot, startTimeToShoot = 0.2f;
    float timeToShoot2, startTimeToShoot2 = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        timeToShoot = 0;
        timeToShoot2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerScoped && (timeToShoot <= 0))
        {
            shoot();
            timeToShoot = startTimeToShoot;
        }

        if (isPlayerScoped2 && (timeToShoot2 <= 0))
        {
            shoot2();
            timeToShoot2 = startTimeToShoot2;
        }
        else
        {
            timeToShoot -= Time.deltaTime;
            timeToShoot2 -= Time.deltaTime;
        }

    }

    void FixedUpdate()
    {
        isPlayerScoped = Physics2D.OverlapArea(scopeAreaA.position, scopeAreaB.position, whatIsPlayer);
        isPlayerScoped2 = Physics2D.OverlapArea(scopeAreaC.position, scopeAreaD.position, whatIsPlayer);
    }

    void shoot()
    {
        Instantiate(enemyBulletPreFrab, enemyShootPoint.position, enemyShootPoint.rotation);
    }

    void shoot2()
    {
        Instantiate(enemyBulletPreFrab, enemyShootPoint2.position, enemyShootPoint2.rotation);
    }
}
