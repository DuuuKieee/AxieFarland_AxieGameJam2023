using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class RandomNPC : MonoBehaviour
{
    public SkeletonDataAsset[] asset;
    [SerializeField] SkeletonAnimation avatar;
    // Start is called before the first frame update
    void Start()
    {
        avatar.skeletonDataAsset = asset[Random.Range(0, asset.Length)];
        avatar.Initialize(true);
    }
}
