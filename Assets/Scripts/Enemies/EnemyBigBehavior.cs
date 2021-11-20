using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigBehavior : MonoBehaviour
{
    public Transform scopeAreaA, scopeAreaB, scopeAreaC, scopeAreaD, scopeAreaE, scopeAreaF, enemyShootPoint, enemyShootPoint2, enemyShootPoint3;
    public GameObject enemyBulletPreFrab;
    public LayerMask whatIsPlayer;

    bool isPlayerScoped, isPlayerScoped2, isPlayerScoped3;
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

        if (isPlayerScoped2 && (timeToShoot <= 0))
        {
            shoot2();
            timeToShoot = startTimeToShoot;
        }

        if (isPlayerScoped3 && (timeToShoot <= 0))
        {
            shoot3();
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
        isPlayerScoped2 = Physics2D.OverlapArea(scopeAreaC.position, scopeAreaD.position, whatIsPlayer);
        isPlayerScoped3 = Physics2D.OverlapArea(scopeAreaE.position, scopeAreaF.position, whatIsPlayer);
    }

    void shoot()
    {
        Instantiate(enemyBulletPreFrab, enemyShootPoint.position, enemyShootPoint.rotation);

    }

    void shoot2()
    {
        Instantiate(enemyBulletPreFrab, enemyShootPoint2.position, enemyShootPoint2.rotation);

    }

    void shoot3()
    {
        Instantiate(enemyBulletPreFrab, enemyShootPoint3.position, enemyShootPoint3.rotation);

    }
}
