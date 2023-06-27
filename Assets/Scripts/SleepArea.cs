using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepArea : MonoBehaviour
{
    public void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Sleep sleep = other.GetComponent<Sleep>();
                if (sleep != null)
                {
                    sleep.DoSleep();
                }  
    }
}
