using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;

    [Header("Menu")]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject Ui;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        menu = GameObject.FindWithTag("Menu");
        Ui = GameObject.FindWithTag("Ui");
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            menu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main Menu":
                break;
            case "Level 01":
                if (Input.GetKeyDown(KeyCode.Escape) && !gameManager.isPaused)
                {
                    PauseGame();
                }
                break;
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 01");
    }
    public void ContinueGame()
    {
        Ui.SetActive(true);
        menu.SetActive(false);
        gameManager.isPaused = false;
        if (gameManager.gameStarted)
        {
            Time.timeScale = 1;
        }
    }

    public void PauseGame()
    {
        gameManager.isPaused = true;
        Time.timeScale = 0;
        menu.SetActive(true);
        Ui.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 0;
    }
}
