using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private Text timeText = null;

    private bool _started;
    private float _timeInSeconds = 0;
    

    private void Update()
    {
        CountTime();
        SetTimeInText(ref timeText);
    }

    private void Start()
    {
        StartCounting();
    }

    public void StartCounting()
    {
        _started = true;
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
        if (_started)
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
