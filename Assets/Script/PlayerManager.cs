using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;
 

    public static int numCoins;
    public Text coinsText;

    void Start()
    {
        Time.timeScale = 1;
        gameOver = false;
        isGameStarted = false;
        numCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }

        coinsText.text = "Coins: " + numCoins;

       if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            Time.timeScale = 1;
            Destroy(startingText);
        }

    }
}

