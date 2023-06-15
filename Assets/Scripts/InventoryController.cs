using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject panelhands;

    private void Start()
    {
        Close();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (panel.activeInHierarchy == false)
            {
                Open();
                CloseHS();
            }

            else
            {
                Close();
                OpenHS();
            }
        }
    }
    public void Open()
    {
        panel.SetActive(true);

    }
    public void Close()
    {
        panel.SetActive(false);
    }
    public void CloseHS()
    {
        panelhands.SetActive(false);

    }
    public void OpenHS()
    {
        panelhands.SetActive(true);

    }
}