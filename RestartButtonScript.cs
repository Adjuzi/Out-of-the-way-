using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartScene()
    {
        BasicMovement.interstitial.Destroy();
        SceneManager.LoadScene("Game");
    }

    public void quitToMenuScene()
    {
        BasicMovement.interstitial.Destroy();
        SceneManager.LoadScene("Menu");
    }
}
