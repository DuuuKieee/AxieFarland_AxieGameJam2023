using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour, ICollectible
{
    // Start is called before the first frame update
    public abstract void Collect();
}
