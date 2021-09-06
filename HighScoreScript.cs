using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{
    public Text highscoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        highscoreDisplay.text = Mathf.RoundToInt(PlayerPrefs.GetFloat("Highscore")).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
