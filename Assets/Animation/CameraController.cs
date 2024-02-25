using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject obj;
    static Animator anim;
    private Transform player;
    public float maxX = 0, minX = 0, maxY = 0, minY = 0;
    public float moveSpeed;
    static bool isFollowPlayer = true;

    //bool isScoll = false;


    static public GameObject targetObj;
    public static CameraController instance;
    public Transform tempTarget, target;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        anim = gameObject.GetComponent<Animator>();

    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);

        //if (isFollowPlayer == false) Invoke("FollowPlayer", 0.5f);
    }
    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }


    public void Shake() //rung
    {
        tempTarget = target;
        target = null;
        anim.Play("Shake", 1);
        StartCoroutine(AnimationTiming(0.3f));
    }

    public void LightShake() //rung nhe
    {
        tempTarget = target;
        target = null;
        anim.Play("LightShake", 1);
        StartCoroutine(AnimationTiming(0.3f));
    }

    public void SlideRight() //cuon phai
    {
        anim.Play("SlideRight");
        isFollowPlayer = false;
    }

    public void SlideLeft()
    {
        anim.Play("SlideLeft");
        isFollowPlayer = false;
    }

    public void ZoomIn()
    {
        anim.Play("ZoomIn");
    }

    public void ZoomOut()
    {
        anim.Play("ZoomOut");
    }

    public void SuperZoomIn()
    {
        anim.Play("SuperZoomIn");
    }

    void FollowPlayer()
    {
        isFollowPlayer = true;
    }
    IEnumerator AnimationTiming(float sec)
    {
        yield return new WaitForSeconds(sec);
        target = tempTarget;
    }

    //public void Scoll()
    //{
    //    isScoll = true;
    //}
}
