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
            text.text = timeStart.ToString("F2");
        }
    }
    public void ChangeRunning()
    {
        timerRunning = !timerRunning;
        timeStart = 0f;
        text.text = "";
    }
}
