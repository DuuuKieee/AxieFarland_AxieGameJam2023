using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFirst;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameController.instance.mainDoor.transform.position = transform.position;
            if (!isFirst)
            {
                GameController.instance.DoorAnimation();
                SpawnEnemy();
            }
            CameraController.instance.ChangeTarget(transform);
        }

    }
    public void SpawnEnemy()
    {
        Player.instance.isCanBeHurted = false;
        StartCoroutine(Player.instance.EndHurt(2f));
        gameObject.GetComponentInParent<ObjectRoomSpawner>().InitalliseObjectSpawning();
        isFirst = true;
    }
}
