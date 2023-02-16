using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
     Transform destination; 
    void Start()
    {
        destination = transform.GetChild(1);
    }
    
    internal void InitiateTransition(Transform toTransition)
    {
        toTransition.position = destination.position;
    }
   
}
