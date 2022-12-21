using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightController : MonoBehaviour
{
   const float secondsInDay = 86400f;
   [SerializeField] Color nightLightColor;
   [SerializeField] AnimationCurve nightTimeCurve;
   [SerializeField] Color dayLightColor = Color.white;

   float time; 
   
}

