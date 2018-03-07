using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    //Singleton Setup
    public static GameManager instance = null;

    public GameObject gameHUD;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public Text scoreText;
    public Text multiplierText;

    [HideInInspector]
    public float xBoundary;

    [HideInInspector]
    public float zBoundary;

    [HideInInspector]
    public bool gameRunning;

    [Range(0, 10)]
    public float gameOverDelay;
    public GameObject player;

    // Awake Checks - Singleton setup
    void Awake() {

        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    void Start()
    {
        gameHUD.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        gameRunning = true;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        StartCoroutine(GameOver(gameOverDelay));
    }

    private IEnumerator GameOver(float delay)
    {
        gameRunning = false;
        ScoreManager.instance.StopAllCoroutines();
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);

    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateText(Text text, string value)
    {
        text.text = value;
    }

    /// <summary>
    /// Fill in space of text with a particular character if the text length is too small
    /// </summary>
    /// <param name="strLen">The desired length of the string</param>
    /// <param name="fillerChar"></param>
    /// <param name="curString"></param>
    /// <param name="fillOnRight"></param>
    /// <returns></returns>
    public string FillTextSpace(int strLen, char fillerChar, string curString, bool fillOnRight = true)
    {
        int lenDiff = 0;
        string newStr = "";
        string fill = "";

        if (strLen > curString.Length)
        {
            lenDiff = strLen - curString.Length;
            fill = new string(fillerChar, lenDiff);


            if (fillOnRight)
            {
                newStr = curString + fill;
            }
            else
            {
                newStr = fill + curString;
            }
        }

        return newStr;
    }
}
