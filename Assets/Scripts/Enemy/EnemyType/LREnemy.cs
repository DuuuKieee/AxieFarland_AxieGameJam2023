using Game;
using UnityEngine;

public class LREnemy : MonsterManager
{
    private float dirX;
    public float moveSpeed;

    //private bool facingRight = false;
    private Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        // rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
        figure.DoRunAnim();
        //moveSpeed = 3f;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.GetComponent<Wall>())
    //     {
    //         dirX *= -1f;
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dirX = -dirX;
        figure.flipX = !figure.flipX;

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    //void LateUpdate()
    //{
    //    CheckWhereToFace();
    //}

    //void CheckWhereToFace()
    //{
    //    if (dirX > 0)
    //        facingRight = true;
    //    else if (dirX < 0)
    //        facingRight = false;

    //    if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
    //        localScale.x *= -1;

    //    transform.localScale = localScale;
    //}
}