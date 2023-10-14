using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeStart;
    [SerializeField] private TextMeshProUGUI text;
    private bool timerRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        text.text = timeStart.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            if(Time.timeScale == 0.1f)
            {
                timeStart = 0;
                text.text = "";
                Time.timeScale = 1f;
            }
            timeStart += Time.deltaTime;
            var minute = (int)(timeStart / 60 % 60);
            var hour = (int)(timeStart / 60 / 60 % 24);
            var seconds = (int)(timeStart % 60);
            text.text = hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + seconds.ToString("D2");
        }
    }
    public void ChangeRunning()
    {
        timerRunning = !timerRunning;
        timeStart = 0f;
        text.text = "";
    }
}
