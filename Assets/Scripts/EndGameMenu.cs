using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    public static bool gameIsEnded = false;
    public static float timeUsed;
    public GameObject endMenuUI;
    public TextMeshProUGUI textmeshPro;

    void Update()
    {
        timeUsed = GameScript.timeStart;
        textmeshPro.text=timeUsed.ToString() + " Seconds";
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}


