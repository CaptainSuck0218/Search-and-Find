using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countdownDisplay;
    public float timeGiven = 60;
    public bool countdownActive = false;

    void Start()
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        
        countdownDisplay.text = Mathf.Round(timeGiven).ToString();

        if (countdownActive == false && timeGiven > 0)
        {
            StartCoroutine(CountDown());
        }

        if(timeGiven == 0)
        {
            LoadGame();
        }

    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator CountDown()
    {
        countdownActive = true;
        yield return new WaitForSeconds(1);
        timeGiven -= 1;
        countdownDisplay.text = Mathf.Round(timeGiven).ToString();
        countdownActive = false;
    }
}
