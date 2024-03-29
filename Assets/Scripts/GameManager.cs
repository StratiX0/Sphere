using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // Create a static instance of the GameManager class
    public static GameManager Instance;

    // Create a reference to the player
    [Header("Player")]
    [SerializeField] Player player;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] Transform playerSpawn;

    // Create a reference to the timer
    [Header("Timer")]
    [SerializeField] Timer timer;
    [SerializeField] TextMeshProUGUI CountdownUi;

    // Create a reference to the fall count
    [Header("Fall")]
    [SerializeField] int fallCount = 0;
    [SerializeField] TextMeshProUGUI fallCountUi;

    // Create a reference to the start state
    [Header("Start")]
    [SerializeField] bool startingGame;
    [SerializeField] float countdownTime;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 0; // Set the time of the game to 0 (pause the game)
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;
        playerRb = player.GetComponent<Rigidbody>();
        playerRb.position = playerSpawn.position;

        timer = Timer.Instance;

        startingGame = false;

        CountdownUi.text = "Press Space to start";
    }

    // Update is called once per frame
    void Update()
    {
        StartGame();
        PlayerRespawn();
        Finish();
    }

    private void StartGame()
    {
        switch (startingGame)
        {
            case true:
                if (countdownTime > 0)
                {
                    countdownTime -= Time.unscaledDeltaTime;
                    int delay = Mathf.CeilToInt(countdownTime);
                    CountdownUi.text = delay.ToString();
                    timer.ResetTimer();
                }
                else
                {
                    CountdownUi.gameObject.SetActive(false);
                    startingGame = false;
                    Time.timeScale = 1;
                    timer.ResetTimer();
                }
                break;
            case false:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    startingGame = true;
                }
                break;
        }
    }

    private void PlayerRespawn()
    {
        if (player.hasFallen)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            player.transform.position = playerSpawn.position;
            fallCount++;
            fallCountUi.text = fallCount.ToString();
            player.hasFallen = false;
        }
    }

    private void Finish()
    {
        if (player.hasFinished)
        {
            player.hasFinished = false;
            timer.stopTimer = true;
            Debug.Log(timer.time);
        }
    }
}
