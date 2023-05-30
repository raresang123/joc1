using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private bool t = true;
    public float transitionSpeed = 3f;
    LensSettings lens;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        lens = virtualCamera.m_Lens;
    }

    private void Update()
    {
        if (t)
        {
            float currentZoom = lens.OrthographicSize;
            float newZoom = Mathf.Lerp(30f, lens.OrthographicSize, transitionSpeed * Time.deltaTime);
            LensSettings lensaux=new LensSettings();
            lensaux.OrthographicSize = newZoom;
            virtualCamera.m_Lens = lensaux;


            // Check if the transition is nearly complete and stop it
            if (Mathf.Abs(newZoom - lens.OrthographicSize) < 0.01f)
            {
                virtualCamera.m_Lens = lens;
                t = false;
            }
        }
    }
}
