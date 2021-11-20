using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOctopusBehavior : MonoBehaviour
{
    public Transform scopeAreaA, scopeAreaB, enemyShootPoint;
    public GameObject enemyBulletPreFrab;
    public LayerMask whatIsPlayer; 

    bool isPlayerScoped;
    float timeToShoot, startTimeToShoot =0.2f; 

    // Start is called before the first frame update
    void Start()
    {
        timeToShoot = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerScoped && (timeToShoot <=0)) {
           shoot();
           timeToShoot = startTimeToShoot; 
        } else
        {
            timeToShoot -= Time.deltaTime;
        }
    }

    void FixedUpdate() {
        isPlayerScoped = Physics2D.OverlapArea(scopeAreaA.position, scopeAreaB.position, whatIsPlayer);  
    }

    void shoot () {
        Instantiate(enemyBulletPreFrab, enemyShootPoint.position, enemyShootPoint.rotation);

    }

    //void dash() { // Particulas enemy die
    //    dashTrial.SetActive(true);
    //    dashTrial.transform.localScale = new Vector3((isFacingRight ? 1 : -1), 1, 1);
    //    anim.SetBool("Dashing", true);
    //    canPlayerControl = false;
    //    timeToResetDash = startTimeToResetDash;
    //}

    //void enemyDie() {
    //    Instantiate(ParticleEnemyDie, transform.position, Quaternion.identity);
    //    Destroy(gameObject);
    //}
}
