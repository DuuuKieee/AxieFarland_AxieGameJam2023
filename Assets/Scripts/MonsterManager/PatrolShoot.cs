using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
public class PatrolShoot : MonsterManager
{
    public float speed;
    public float lineOfSight;
    public float shootingRange;
    public float fireRate;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform playerPos;

    void Start()
    {
        playerPos = Player.instance.transform; // FIND THE PLAYER
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(playerPos.position, transform.position); // When player goes near enemy

        // Will move when player in line of sight, but not move when in shooting range
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, playerPos.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}