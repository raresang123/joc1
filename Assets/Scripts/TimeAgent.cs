using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action invoke;
    private void Start()
    {
        GameManager.instance.timeController.Subscribe(this);
    }

    private void Invoke()
    {

    }

    private void OnDestroy()
    {
        GameManager.instance.timeController.Unsubscribe(this);
    }

}
