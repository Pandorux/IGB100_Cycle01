using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public GameObject instructionsMenu;
    public GameObject mainMenu;

    [HideInInspector]
    public bool instructionsActive { get; set; }

    // Use this for initialization
    void Start() {
        instructionsActive = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public void Instructions()
    {
        //instructionsMenu.SetActive(instructionsActive);
        //mainMenu.SetActive(!instructionsActive);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
