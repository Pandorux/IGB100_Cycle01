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
    public GameObject[] spawners;

    [Range(1, 5)]
    public int startDifficulty = 1;
    public int maxDifficulty = 5;
    public float difficultyIncreaseRate;

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

        for (int i = 0; i < spawners.Length; i++)
            spawners[i].SetActive(false);

        AssignDifficulty(startDifficulty);
        StartCoroutine(IncreaseDifficulty(difficultyIncreaseRate, 1));
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

    private void AssignDifficulty(int dif = 1)
    {
        dif = dif <= 0 ? 1 :
            dif >= 6 ? 5 : dif;

        if(dif >= 1)
        {
            spawners[0].gameObject.SetActive(true);
        }

        if(dif >= 2)
        {
            spawners[1].gameObject.SetActive(true);
            spawners[2].gameObject.SetActive(true);
        }

        if(dif >= 3)
        {
            spawners[3].gameObject.SetActive(true);
            spawners[4].gameObject.SetActive(true);
        }

        if(dif >= 4)
        {
            spawners[5].gameObject.SetActive(true);
        }

        if(dif == 5)
        {
            spawners[6].gameObject.SetActive(true);
            spawners[7].gameObject.SetActive(true);
        }

    }

    private IEnumerator IncreaseDifficulty(float delay, int curDif)
    {
        yield return new WaitForSeconds(delay);
        AssignDifficulty(curDif + 1);

        if (curDif + 1 < maxDifficulty)
            StartCoroutine(IncreaseDifficulty(delay, curDif + 1));
    }
}
