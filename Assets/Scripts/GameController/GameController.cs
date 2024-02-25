using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int numEnemy, numRoom;
    public int room = 0;
    public static GameController instance;
    public GameObject mainDoor, doorLeft, doorRight, doorUp, doorDown, charracterCard;
    public LayerMask roomLayer;
    public Animator[] doorAnim;
    public bool doorCheck, isChoosen;

    // [SerializeField] GameObject arrow1, arrow2, goal;
    GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        doorAnim = mainDoor.GetComponentsInChildren<Animator>();
        isChoosen = false;
        // arrow1.transform.localScale = Vector3.zero;
        // arrow2.transform.localScale = Vector3.zero;
        // goal.transform.localScale = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        room = 0;
        DoorOpenAnimation();
        CameraController.instance.ZoomIn();

    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.K)) print("room: " + room);
        // if (Input.GetKeyDown(KeyCode.L)) NextLevel();
        // if (Input.GetKeyDown(KeyCode.J)) print(SceneManager.GetActiveScene().buildIndex);

        if (numEnemy <= 0)
        {
            DoorOpenAnimation();
        }
    }

    public static void NextLevel()
    {
        // if (Level.level == 2)
        // {
        //     Level.level = 1;
        //     SceneManager.LoadScene(0);
        // }
        // else
        // {
        //     Level.level++;
        //     SceneManager.LoadScene(0);
        // }
    }

    public static void Restart(bool isGoal)
    {
        if (isGoal)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        if (isChoosen)
        {
            CameraController.instance.ZoomOut();
            Player.instance.GetComponent<Player>().enabled = true;
            charracterCard.SetActive(false);
        }
    }
    public void DoorAnimation()
    {
        doorAnim[0].SetBool("isClose", true);
        doorAnim[1].SetBool("isClose", true);
        doorAnim[2].SetBool("isClose", true);
        doorAnim[3].SetBool("isClose", true);


    }
    public void DoorOpenAnimation()
    {
        doorAnim[0].SetBool("isClose", false);
        doorAnim[1].SetBool("isClose", false);
        doorAnim[2].SetBool("isClose", false);
        doorAnim[3].SetBool("isClose", false);
    }
}