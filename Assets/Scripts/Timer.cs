using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    public float time = 0.0f;

    public bool stopTimer = false;

    [SerializeField] TextMeshProUGUI UiTimerText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimerUpdate();
        FormatTime();
    }

    private void TimerUpdate()
    {
        if (!stopTimer)
        {
            time += Time.deltaTime;
        }
    }

    public void FormatTime()
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int milliseconds = Mathf.FloorToInt((time - minutes * 60 - seconds) * 1000);

        string format = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);

        UiTimerText.text = format;
    }
}