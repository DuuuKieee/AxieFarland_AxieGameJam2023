using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class ChasingEnemy : MonsterManager
{
    // Start is called before the first frame update
    public float speed;
    public float lineOfSight;
    private Transform playerPos;

    void Start()
    {
        playerPos = Player.instance.transform; // FIND THE PLAYER
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(playerPos.position, transform.position); // When player goes near enemy

        // Will move when player in line of sight, but not move when in shooting range
        if (distanceFromPlayer < lineOfSight)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, playerPos.position, speed * Time.deltaTime);
            if (transform.position.x <= playerPos.position.x) figure.flipX = true;
            else figure.flipX = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}
