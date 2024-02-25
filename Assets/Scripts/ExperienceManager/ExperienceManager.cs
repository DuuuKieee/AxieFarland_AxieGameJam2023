using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ExperienceManager Instance;
    public delegate void ExperienceChangeHandler(int amount, int idEperience);
    public event ExperienceChangeHandler OnExperienceChange;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void AddExperience(int amount, int id)
    {
        OnExperienceChange?.Invoke(amount, id);
    }

}
