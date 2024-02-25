using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text text;
    void Start()
    {
        text.text = PlayerPrefs.GetInt("myWin").ToString();
    }

}
