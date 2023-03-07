using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{

    const float secondsInDay = 84500f;
    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;
    
    float time ; 

     [SerializeField] Text text;
     [SerializeField] float timeScale = 60f;
     [SerializeField] Light2D globalLight;
     private int days;

     List<TimeAgent> agents ;
     
    private void Awake()
    {
        agents = new List<TimeAgent>();

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
        get { return time /3600f;}
     }

     float Minutes 
     {
        get { return time % 3600f/60f;}
     }
    
    
    
    private void Update() 
    {
        time += Time.deltaTime * timeScale;
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
        if (time > secondsInDay)
        {
            NextDay();
        }
    }
    private void NextDay()
    {
        time = 0    ;
        days += 1; 

    }

}
