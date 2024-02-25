using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinNotification : MonoBehaviour
{
    public GameObject bubbleChat;
    public GameObject canvasDialogue, panel, dialogue;

    TriggerDialogue triggerDialogue;

    bool isTalking, isInColiider;
    public bool isWining;

    Animator animPanel, anim;
    [SerializeField] GameObject blackScene;

    private void Awake()
    {
        triggerDialogue = dialogue.GetComponent<TriggerDialogue>();
        animPanel = panel.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isTalking = false;
        isWining = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWining)
        {
            if (isTalking == false && canvasDialogue.activeSelf == false)
            {
                isTalking = true;

                canvasDialogue.SetActive(true);
                animPanel.Play("Start");
                triggerDialogue.ResetStart();
                print("Talk");
                Time.timeScale = 0;
            }
            if (triggerDialogue.isEndDialogue == true && triggerDialogue.isTalking == false && canvasDialogue.activeSelf == true && isTalking == true)
            {
                animPanel.Play("End");
                //Time.timeScale = 1;
                Invoke("SetFalseCanvasDialogue", 1f);
                isTalking = false;
                isWining = false;
                blackScene.gameObject.SetActive(true);
                blackScene.GetComponent<Animator>().Play("End");
                Invoke("ChangeScene", 1f);

            }
        }
    }

    void SetFalseCanvasDialogue()
    {
        canvasDialogue.SetActive(false);
    }
    void ChangeScene()
    {
        SceneManagement.instance.GameScene();
    }
}