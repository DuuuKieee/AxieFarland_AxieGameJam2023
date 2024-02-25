using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text hpText, atkText;
    public int hp, atk;
    bool Dash;
    public static StatsManager instance;
    [SerializeField] Character statCharacter;
    public Image[] classImg;
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

    }

    public void UpdateStats(int _hp, int _atk)
    {
        hp = _hp;
        atk = _atk;
        hpText.text = "HP: " + hp;
        atkText.text = "ATK: " + atk;
        statCharacter.currentHp = hp;
        statCharacter.currentAtk = atk;
        statCharacter.UpdateStats();
    }
}
