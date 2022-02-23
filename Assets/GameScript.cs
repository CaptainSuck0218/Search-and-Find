using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameScript : MonoBehaviour
{
    public GameObject[] gameobjectarray;
    public GameObject[] invisiblearray;
    private GameObject tempGO;
    public GameObject endgamemenu;
    public GameObject inGameUI;
    public GameObject pauseMenuUI;
    public TextMeshProUGUI timerDisplay;
    public TextMeshProUGUI foundDisplay;
    public GameObject timeUsed;
    public static float timeStart = 0;
    public bool timeActive = false;
    public static int itemfound = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeStart = 0;
        gameobjectarray = GameObject.FindGameObjectsWithTag("Randomable");
        Shuffle();
        for (int i = 0; i < 20; i++)
        {
            gameobjectarray[i].tag = ("Invisible");
            gameobjectarray[i].GetComponent<Renderer>().enabled = false;
        }
        timerDisplay.text = Mathf.Round(timeStart).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        invisiblearray = GameObject.FindGameObjectsWithTag("Invisible");

        if (timeActive == false && invisiblearray.Length > 0)
        {
            StartCoroutine(TimerStart());
        }
        else
        {
            StopCoroutine(TimerStart());
        }

        if (itemfound == 20)
        {
            EndGame();
            itemfound = 0;
        }

        timerDisplay.text = Mathf.Round(timeStart).ToString();
        invisiblearray = GameObject.FindGameObjectsWithTag("Invisible");
        foundDisplay.text = "Item Left : " + invisiblearray.Length;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (Input.GetButtonDown("LeftMouse"))
            {
                if (hit.collider.tag == "Invisible")
                {
                    hit.collider.GetComponent<Renderer>().enabled = true;
                    hit.collider.tag = "Randomable";
                    itemfound++;
                }
            }
        }

    }

    public void Shuffle()
    {
        for (int i = 0; i < gameobjectarray.Length; i++)
        {
            int rnd = Random.Range(0, gameobjectarray.Length);
            tempGO = gameobjectarray[rnd];
            gameobjectarray[rnd] = gameobjectarray[i];
            gameobjectarray[i] = tempGO;
        }

    }

    void EndGame()
    {
        endgamemenu.SetActive(true);
        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator TimerStart()
    {
        timeActive = true;
        yield return new WaitForSeconds(1);
        timeStart += 1;
        if (timeStart < 10)
        {
            timerDisplay.text = Mathf.Round(timeStart).ToString();
        }
        else
        {
            timerDisplay.text = Mathf.Round(timeStart).ToString();
        }
        timeActive = false;
    }

}
