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

    private void Start()
    {
      //  notification = GetComponent<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
         
        {

                notification.SetActive(false);
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
        }
    }
}
