using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public int currentHp, maxHp, currentAtk, maxAtk, currentSpeed, maxSpeed, currentRange, maxRange, currentExperience,
    maxExperience, currentLevel, currentatkExperience, maxatkExperience, currentatkLevel
    , currentspeedExperience, maxspeedExperience, currentspeedLevel,
    currentatkrangeExperience, maxatkrangeExperience, currentatkrangeLevel;

    [SerializeField] Text atkText, speedText, rangeText, hpText;
    [SerializeField] Slider hpSlider;
    [SerializeField] AimRotation atkZone;
    [SerializeField] Player player;
    [SerializeField] GameObject lvUp;

    public static Character instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

    }
    private void OnEnable()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }
    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }
    private void HandleExperienceChange(int newExperience, int idEperience)
    {
        if (idEperience == 1)
        {
            currentatkExperience += newExperience;
            // atkSlider.value = currentatkExperience;
            if (currentatkExperience >= maxatkExperience)
            {
                LevelUpAtk();
            }
            atkText.text = $"Level {currentatkLevel}: {currentatkExperience} / {maxatkExperience}";
        }
        if (idEperience == 2)
        {
            currentspeedExperience += newExperience;
            if (currentspeedExperience >= maxspeedExperience)
            {
                LevelUpSpeed();
            }
            speedText.text = $"Level {currentspeedLevel}: {currentspeedExperience} / {maxspeedExperience}";
        }
        if (idEperience == 3)
        {
            currentatkrangeExperience += newExperience;
            if (currentatkrangeExperience >= maxatkrangeExperience)
            {
                LevelUpRange();
            }
            rangeText.text = $"Level {currentatkrangeLevel}: {currentatkrangeExperience} / {maxatkrangeExperience}";
        }
        if (idEperience == 4)
        {
            if (currentHp < maxHp)
                Heal();
        }
    }
    private void LevelUpAtk()
    {
        maxAtk += 3;
        currentAtk = maxAtk;
        currentatkLevel++;
        currentatkExperience = 0;
        maxatkExperience += 100;
        GameObject damagePopup = Instantiate(lvUp, transform.position, Quaternion.identity) as GameObject;
        damagePopup.transform.GetChild(0).GetComponent<TextMesh>().text = "LEVEL UP (+Atk)";
        AudioManager.instance.PlaySFX("LevelUp");
        UpdateStats();
    }
    private void LevelUpSpeed()
    {
        maxSpeed += 1;
        currentSpeed = maxSpeed;
        currentspeedLevel++;
        currentspeedExperience = 0;
        maxspeedExperience += 100;
        Player.instance.timeAtack = Player.instance.timeAtack / 2;
        GameObject damagePopup = Instantiate(lvUp, transform.position, Quaternion.identity) as GameObject;
        damagePopup.transform.GetChild(0).GetComponent<TextMesh>().text = "LEVEL UP (+AtkSpeed, AimSpeed)";
        AudioManager.instance.PlaySFX("LevelUp");

        UpdateStats();
    }
    private void LevelUpRange()
    {
        maxRange += 1;
        currentRange = maxRange;
        currentatkrangeLevel++;
        currentatkrangeExperience = 0;
        maxatkrangeExperience += 100;
        Player.instance.moveSpeed = Player.instance.moveSpeed + 0.5f;
        GameObject damagePopup = Instantiate(lvUp, transform.position, Quaternion.identity) as GameObject;
        damagePopup.transform.GetChild(0).GetComponent<TextMesh>().text = "LEVEL UP (+Range, Speed)";
        AudioManager.instance.PlaySFX("LevelUp");
        UpdateStats();
    }
    public void UpdateStats()
    {
        atkZone.speed = currentSpeed;
        atkZone.radius = currentRange;
        player.HP = currentHp;
        player.maxHP = maxHp;
        hpSlider.maxValue = maxHp;
        hpText.text = currentHp.ToString();
        hpSlider.value = currentHp;
    }
    public void RandomStats()
    {
        currentAtk = Random.Range(3, 11);
        currentHp = Random.Range(3, 11);
        maxAtk = currentAtk;
        maxHp = currentHp;
        StatsManager.instance.UpdateStats(currentHp, currentAtk);
    }
    public void Heal()
    {
        currentHp++;
        UpdateStats();
    }


}
