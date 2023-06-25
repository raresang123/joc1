using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Notification : MonoBehaviour
{
    [SerializeField] GameObject notification;
    public Transform character;
    NpcController dog;

    private void Start()
    {
      
    
        dog = GetComponent<NpcController>();
        character = GameManager.instance.player.transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, character.position);
        if (distance > 2f)
        {
            notification.SetActive(false);
        }
        else
        {

            if (CompareTag("npc"))
            {
                if (dog.tame == false)
                {
                    notification.SetActive(true);
                }
            }
            else
            {
                notification.SetActive(true);
            }

            if (CompareTag("crystal"))
            {
                
                {
                    notification.SetActive(true);
                }
            }
            else
            {
                notification.SetActive(true);
            }
        }
    }
      
}
