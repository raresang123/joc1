using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource playbutton;
   public void PlayGame()
    {
        playbutton.Play();
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        SceneManager.LoadScene("Home");
        SceneManager.LoadScene("Essentials",parameters);
    }
    public void QuitGame()
    {
        Debug.Log("pa");
        Application.Quit();
    }

}