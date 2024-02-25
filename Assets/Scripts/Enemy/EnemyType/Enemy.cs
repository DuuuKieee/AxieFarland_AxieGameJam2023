using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;
    protected Rigidbody2D rb;
    protected SpriteRenderer sprRen;
    protected Animator anim;
    protected Collider2D coli;

    protected GameObject player;
    public GameObject dieObj;
    protected bool isDie = false;

    Color color;
    void Awake()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();
        sprRen = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        coli = gameObject.GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        color = sprRen.material.color;

        enabled = false;

    }

    private void LateUpdate()
    {
        if (HP <= 0 && isDie == false) Die();
    }
    public void Hurt()
    {
        HP--;

        StartCoroutine(RedBlinking(0.01f));

        print(gameObject.name + "'s HP remaining: " + HP);
    }

    IEnumerator RedBlinking(float sec)
    {
        while (color.g > 0)
        {
            yield return new WaitForSeconds(sec);
            color.g -= 0.1f;
            color.b -= 0.1f;
            if (color.g < 0) color.g = 0;
            if (color.b < 0) color.b = 0;
            sprRen.material.color = color;
        }
        while (color.g < 1)
        {
            yield return new WaitForSeconds(sec);
            color.g += 0.1f;
            color.b += 0.1f;
            if (color.g > 1) color.g = 1;
            if (color.b > 1) color.b = 1;
            sprRen.material.color = color;
        }
    }
    public void Die()
    {
        Debug.Log(gameObject.name + " die");

        isDie = true;


        if (anim != null) anim.speed = 0;
        Destroy(gameObject, 0.5f);
    }

    //Cac ham duoi nay dung de ngung hoat dong cua Enemy khi ra khoi man hinh camera
    void OnBecameVisible()
    {
        enabled = true;
    }
    void OnBecameInvisible()
    {
        enabled = false;
    }
    private void OnDestroy()
    {
        GameController.instance.numEnemy--;
        GameObject dieObject;
        dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
        Destroy(dieObject, 1);
        Debug.Log(GameController.instance.numEnemy--);
    }
}