using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{

    const float secondsInDay = 84500f;
    const float phaseLength = 900f;
    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] float morningTime = 28800f;
    float time;

    float totalTime;

    [SerializeField] float startAtTime = 28800f;
    [SerializeField] Text text;
    [SerializeField] Text finalText;
    [SerializeField] GameObject finalTextShow;
    [SerializeField] float timeScale = 60f;
    [SerializeField] Light2D globalLight;
    private int days;
    List<TimeAgent> agents;




    private void Awake()
    {
        agents = new List<TimeAgent>();

    }

    private void Start()
    {
        time = startAtTime;
        totalTime = 0;
    }


    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }
    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }


    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    float FinalHours
    {
        get { return totalTime / 3600f; }
    }

    float FinalMinutes
    {
        get { return totalTime % 3600f / 60f; }
    }



    private void Update()
    {
        if(finalTextShow.active == false)
        {
            totalTime += Time.deltaTime * timeScale;
        }
        time += Time.deltaTime * timeScale;
        TimeValueCalculation();
        DayLight();
        TimeAgents();
        FinalTime();

        if (time > secondsInDay)
        {
            
            NextDay();
        }


    }

    int currentPhase = 0;
    private void TimeAgents()
    {
        int phase = (int)(time / phaseLength);

        if (currentPhase != phase)
        {
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].onTimeTick();
            }
        }

    }
    private void DayLight()
    {

        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
    }

    private void TimeValueCalculation()
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
    }
    private void NextDay()
    {
        time = 0;
        days += 1;
    }

    public void SkipTime(float seconds = 0, float minute= 0, float hours = 0)
    {
        float timeToSkip = seconds;
        timeToSkip += minute * 60f;
        timeToSkip += hours * 3600f;
        time += timeToSkip;
    }
    public void SkipToMorning()
    {
        float secondsToSkip = 0f;
        if(time > morningTime)
        {
            secondsToSkip += secondsInDay - time + morningTime;
        }
        else
        {
            secondsToSkip += morningTime - time;
        }
        SkipTime(secondsToSkip);
    }

    public void FinalTime()
    {
        int hh = (int)FinalHours;
        int mm = (int)FinalMinutes;
        //finalText.text = hh.ToString("00") + ":" + mm.ToString("00");
    }
    public void ShowFinalTime()
    {
        finalTextShow.SetActive(true);
        int hh = (int)FinalHours;
        int mm = (int)FinalMinutes;
        finalText.text = "You finished in:  " + hh.ToString("00")  + "h " + mm.ToString("00")+"m";
    }

}
