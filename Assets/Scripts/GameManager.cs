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

    [Header("Start")]
    [SerializeField] bool start;
    [SerializeField] float delayOnStart;
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

        playerRb.position = playerSpawn.position;

        
        Time.timeScale = 0;

        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start && delayOnStart > 0)
        {
            delayOnStart -= Time.unscaledDeltaTime;
        }
        else if (start && delayOnStart <= 0)
        {
            start = false;
            Time.timeScale = 1;
            timer = Timer.instance;
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
