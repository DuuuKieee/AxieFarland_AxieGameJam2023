using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bossPrefabs;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(bossPrefabs, transform.position, Quaternion.identity);
        }
    }
}
