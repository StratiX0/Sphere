using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    [SerializeField] Player player;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] Transform playerSpawn;

    [Header("Timer")]
    [SerializeField] Timer timer;
    [SerializeField] TextMeshProUGUI startCountdown;

    [Header("Fall")]
    [SerializeField] int fallCount = 0;
    [SerializeField] TextMeshProUGUI UiFallCountTtext;

    [Header("Start")]
    [SerializeField] bool start;
    [SerializeField] float delayOnStart;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        playerSpawn = GameObject.FindWithTag("Spawn").transform;
        playerRb = player.GetComponent<Rigidbody>();

        playerRb.position = playerSpawn.position;

        timer = Timer.instance;

        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start && delayOnStart > 0)
        {
            delayOnStart -= Time.unscaledDeltaTime;
            int delay = Mathf.CeilToInt(delayOnStart);
            startCountdown.text = delay.ToString();
            timer.ResetTimer();
        }
        else if (start && delayOnStart <= 0)
        {
            startCountdown.gameObject.SetActive(false);
            start = false;
            Time.timeScale = 1;
            timer.ResetTimer();
        }

        PlayerRespawn();
        Finish();
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
}
