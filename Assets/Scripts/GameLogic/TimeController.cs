using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController m_timerInstance;

    [SerializeField]
    private TextMeshProUGUI m_timerTextMeshPro = null;
    [HideInInspector]
    public TimeSpan m_timePlaying;
    [HideInInspector]
    public bool m_timerIsRunning;
    [HideInInspector]
    public float m_elapsedTime;

    public event Action OneMinutePlaying = delegate { };

    private void Awake()
    {
        m_timerInstance = this;
        m_timerIsRunning = false;
        m_timerTextMeshPro.text = "";

    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    OneMinutePlaying();
        //}
    }

    public void StartTimer()
    {
        m_elapsedTime = 0.0f;
        m_timerIsRunning = true;

        StartCoroutine(UpdateTimer());
    }
    public void PauseTimer()
    {
        m_timerIsRunning = false;
    }
    public void ResumeTimer()
    {
        m_timerIsRunning = true;
        StartCoroutine(UpdateTimer());

    }


    private IEnumerator UpdateTimer()
    {
        while(m_timerIsRunning)
        {
            m_elapsedTime += Time.deltaTime;
            m_timePlaying = TimeSpan.FromSeconds(m_elapsedTime);
            string l_timeToString = m_timePlaying.ToString("hh':'mm':'ss");
            m_timerTextMeshPro.text = l_timeToString;

            yield return null;
        }
    }
}
