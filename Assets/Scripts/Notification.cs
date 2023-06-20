using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TMPro;

public class Notification : MonoBehaviour
{
    [SerializeField] GameObject notification;
    GameObject currentTarget;
   public bool playerInRange;

   

    private void Update()
    {
        if (playerInRange)
        {
            if (notification.activeInHierarchy)
            {
                notification.SetActive(false);
            }
            else
            {
                notification.SetActive(true);
            }
        }
    }

    //public void NotificationFunction(GameObject target)
    //{
    //    if(currentTarget = target)
    //    {
    //        currentTarget = target;
    //        Vector3 position = target.transform.position;
    //        NotificationFunction(position);
    //    }

    //}

    //public void NotificationFunction(Vector3 position)
    //{
    //    notification.SetActive(true);


    //    notification.transform.position = position;
    //}
    //public void NotificationFunctionof()
    //{
    //    currentTarget = null;
    //    notification.SetActive(false);



    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange =false;
            notification.SetActive(false);

        }
    }
}
