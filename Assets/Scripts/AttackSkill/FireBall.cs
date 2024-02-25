using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform aimCrosshair;
    int critChance;
    void Start()
    {
        StartCoroutine(DestroyObj(0.33f));
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        var hp = col.gameObject.GetComponent<IHealth>();
        if (hp != null && col.tag != "Player" && col.tag != "UndefeatEnemy")
        {
            if (Player.instance.isBeastClass)
            {
                critChance = Random.Range(1, 100);
                if (critChance <= 20)
                {
                    hp.TakeDamage(Character.instance.currentAtk * 2);
                    Debug.Log("Chi mang");
                }
                else hp.TakeDamage(Character.instance.currentAtk);
            }
            else hp.TakeDamage(Character.instance.currentAtk);
        }
        Destroy(gameObject, 0.33f);
    }
    IEnumerator DestroyObj(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);

    }

}
