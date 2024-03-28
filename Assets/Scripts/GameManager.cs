using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    [SerializeField] Player player;
    [SerializeField] Rigidbody playerRb;

    [SerializeField] Transform playerSpawn;

    [Header("Timer")]
    [SerializeField] Timer timer;

    [Header("Fall")]
    [SerializeField] int fallCount = 0;
    [SerializeField] TextMeshProUGUI UiFallCountTtext;

    [Header("Menu")]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject Ui;
    [SerializeField] bool isPaused;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        playerSpawn = GameObject.FindWithTag("Spawn").transform;
        playerRb = player.GetComponent<Rigidbody>();

        timer = Timer.instance;

        playerRb.position = playerSpawn.position;

        menu = GameObject.FindWithTag("Menu");
        menu.SetActive(false);
        isPaused = false;
        Ui = GameObject.FindWithTag("Ui");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRespawn();
        Finish();

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }
    }

    private void PlayerRespawn()
    {
        if (player.fall)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            player.transform.position = playerSpawn.position;
            fallCount++;
            UiFallCountTtext.text = fallCount.ToString();
            player.fall = false;
        }
    }

    private void Finish()
    {
        if (player.finish)
        {
            player.finish = false;
            timer.stopTimer = true;
            Debug.Log(timer.time);
        }
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
        isPaused = true;
        menu.SetActive(true);
        Ui.SetActive(false);
        Time.timeScale = 0;
    }
}
