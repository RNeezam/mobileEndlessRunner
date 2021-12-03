using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour
{
    public void ReplayGame()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        Application.Quit();
    }
}
