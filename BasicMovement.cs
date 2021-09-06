using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class BasicMovement : MonoBehaviour
{
    public static InterstitialAd interstitial;
    private int npaValue = -1;

    public Animator animator;
    bool facingRight = true;

    public GameObject gameOverText, restartButton, quitToMenuButton, playedTime;

    //Touch try
    float moveSpeed = 5f;
    Rigidbody2D rb;
    Touch touch;
    Vector3 touchPosition, whereToMove;
    bool isMoving = false;
    float previousDistanceToTouchPos, currentDistanceToTouchPos;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.SetActive(false);
        restartButton.SetActive(false);
        quitToMenuButton.SetActive(false);
        playedTime.SetActive(true);

        //Touch try
        rb = GetComponent<Rigidbody2D>();

        npaValue = PlayerPrefs.GetInt("npa");
        RequestInterstitial();
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Touch try
        if (isMoving)
        {
            currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
            animator.SetFloat("Horizontal", whereToMove.x);
            animator.SetFloat("Vertical", whereToMove.y);
            animator.SetFloat("Magnitude", currentDistanceToTouchPos);
        }

        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                previousDistanceToTouchPos = 0;
                currentDistanceToTouchPos = 0;
                isMoving = true;
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                whereToMove = (touchPosition - transform.position).normalized;
                rb.velocity = new Vector2(whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
            }
        }

        if(currentDistanceToTouchPos > previousDistanceToTouchPos)
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
            animator.SetFloat("Magnitude", 0.0f);
        }

        if (isMoving)
        {
            previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
            animator.SetFloat("Horizontal", whereToMove.x);
            animator.SetFloat("Vertical", whereToMove.y);
            animator.SetFloat("Magnitude", currentDistanceToTouchPos);
        }

        
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        
        //transform.position = transform.position + 5*movement * Time.deltaTime;
        
        
        if (touchPosition.x > transform.position.x && !facingRight)
            {
                Flip();
                facingRight = true;
            }
        else if (touchPosition.x < transform.position.x && facingRight)
            {
                Flip();
                facingRight = false;
            }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            quitToMenuButton.SetActive(true);
            playedTime.SetActive(false);
            gameObject.SetActive(false);

            float highscoreTime = PlayerPrefs.GetFloat("Highscore");

            if(highscoreTime < TimePlayed.playedTimed)
            {
                PlayerPrefs.SetFloat("Highscore", TimePlayed.playedTimed);
            }

            int tries = PlayerPrefs.GetInt("NumberOfTries");
            tries = tries+1;
            PlayerPrefs.SetInt("NumberOfTries", tries);

            if (tries >= 3)
            {
                if (interstitial.IsLoaded())
                {
                    interstitial.Show();
                }

                PlayerPrefs.SetInt("NumberOfTries", 0);
            }


        }
    }

    private void RequestInterstitial()
    {

        string adUnitId = "ca-app-pub-7447972291607411/7803090017";

        //Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        //Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddExtra("npa", npaValue.ToString()).Build();
        //Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

}