using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Item>(out Item item))
        {
            item.SetTarget(transform.parent.position);
        }
    }
}
