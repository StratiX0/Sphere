using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject Ui;
    [SerializeField] bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.FindWithTag("Menu");
        Ui = GameObject.FindWithTag("Ui");
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            menu.SetActive(false);
            isPaused = false;
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
                if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
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
        Time.timeScale = 1;
        Ui.SetActive(true);
        menu.SetActive(false);
        isPaused = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
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
