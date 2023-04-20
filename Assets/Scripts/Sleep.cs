using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{

    DisableControls disableControls;
    Character character;
    DayTimeController dayTime;
    private void Awake()
    {
        disableControls = GetComponent<DisableControls>();
        character = GetComponent<Character>();
        dayTime = GameManager.instance.timeController;

    }
    internal void DoSleep()
    {
        StartCoroutine(SleepRoutine());
       
    }
    IEnumerator SleepRoutine()
    {
      ScreenTint screenTint =  GameManager.instance.screeTint;
        disableControls.DisableControl();
        character.FullHeal();
        character.FullRest();
      
        screenTint.Tint();
        yield return new WaitForSeconds(2f);
        dayTime.SkipToMorning();
        screenTint.UnTint();
        yield return new WaitForSeconds(2f);
        disableControls.EnableControl();


        yield return null;
    }
}
    