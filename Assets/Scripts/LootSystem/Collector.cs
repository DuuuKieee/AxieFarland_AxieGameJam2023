using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collison)
    {
        ICollectible collectible = collison.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.Collect();
            AudioManager.instance.PlaySFX("Collect");
        }
    }
}
