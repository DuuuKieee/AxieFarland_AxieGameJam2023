using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class TalkingSign : MonoBehaviour
{
    public GameObject bubbleChat;
    public GameObject canvasDialogue, panel, dialogue;

    TriggerDialogue triggerDialogue;

    public bool isTalking, isInColiider;

    Animator animPanel, anim;

    private void Awake()
    {
        triggerDialogue = dialogue.GetComponent<TriggerDialogue>();
        animPanel = panel.GetComponent<Animator>();
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isTalking = false;
        isInColiider = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInColiider)
        {
            if (Input.GetKeyDown(KeyCode.F) && isTalking == false && canvasDialogue.activeSelf == false)
            {
                isTalking = true;
                anim.Play("Talk");
                AudioManager.instance.PlaySFX("Attack");

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
                anim.Play("Idle");
            }
        }
    }

    void SetFalseCanvasDialogue()
    {
        canvasDialogue.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isInColiider = true;
            anim.Play("Talk");
            bubbleChat.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isInColiider = false;
            bubbleChat.SetActive(false);
            anim.Play("Idle");
        }
    }
}
