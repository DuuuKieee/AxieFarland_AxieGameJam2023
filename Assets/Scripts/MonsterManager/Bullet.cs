using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class Bullet : MonsterManager
{
    // Start is called before the first frame update
    public float speed = 5;
    Vector2 moveDirection;
    void Start()
    {
        AudioManager.instance.PlaySFX("Shoot");
        moveDirection = (player.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 2.5f);
    }

}