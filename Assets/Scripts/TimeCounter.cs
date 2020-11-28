using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public bool pause;
    [SerializeField] private Text timeText = null;

    private float _timeInSeconds = 0;
    

    private void Update()
    {
        if (pause)
            return;

        CountTime();
        SetTimeInText(ref timeText);
    }

    private void Start()
    {
        StartCounting();
    }

    public void Reset()
    {
        _timeInSeconds = 0;
        pause = false;
    }

    public void StartCounting()
    {
        pause = false;
    }

    public void SetTimeInText(ref Text text)
    {
        var time = SecondsToMinutesAndSeconds(GetSeconds());
        if (time.Value < 10)
            text.text = $"{time.Key}:0{time.Value}";
        else
            text.text = $"{time.Key}:{time.Value}";
    }
        
    private void CountTime()
    {
        if (!pause)
            _timeInSeconds += Time.deltaTime;
    }

    private KeyValuePair<int, int> SecondsToMinutesAndSeconds(int seconds)
    {
        return new KeyValuePair<int, int>(Mathf.FloorToInt(seconds / 60f), seconds % 60);
    }

    private int GetSeconds()
    {
        return (int)_timeInSeconds;
    }
}
