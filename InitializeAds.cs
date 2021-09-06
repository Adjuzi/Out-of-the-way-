using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class InitializeAds : MonoBehaviour
{
    public GameObject GDPR_Popup;

    // Start is called before the first frame update
    void Start()
    {
        CheckForGDPR();
        //Initialize the Google Mobile Ads SDK
        MobileAds.Initialize(initstatus => { });
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CheckForGDPR()
    {
        if(PlayerPrefs.GetInt("npa", -1) == -1)
        {
            //show GDPR popup
            GDPR_Popup.SetActive(true);
            //pause the game
            Time.timeScale = 0;
        }
    }

    public void OnUserClickAccept()
    {
        PlayerPrefs.SetInt("npa", 0);
        //hide gdpr popup
        GDPR_Popup.SetActive(false);
        //play
        Time.timeScale = 1;
    }

    public void OnUserClickCancel()
    {
        PlayerPrefs.SetInt("npa", 1);
        //hide gdpr popup
        GDPR_Popup.SetActive(false);
        //play
        Time.timeScale = 1;
    }

    public void OnUserClickPrivacyPolicy()
    {
        //Opens information on how Google uses data
        Application.OpenURL("https://policies.google.com/technologies/partner-sites");
    }

                
}
