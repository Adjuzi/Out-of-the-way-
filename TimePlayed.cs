using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePlayed : MonoBehaviour
{
    public static float playedTimed;
    public Text timeDisplay;
    public static int timeMultiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        playedTimed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        playedTimed += Time.deltaTime;
        timeDisplay.text = Mathf.RoundToInt(playedTimed).ToString();
        timeMultiplier = 1 + (Mathf.RoundToInt(playedTimed)/5);
    }
}
