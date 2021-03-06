using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    /// <summary>
    /// THIS SCRIPT WILL BE HANDLING ALL FUNCTIONS USED BY THE MENU, END GAME SCENE, 
    /// OPTION AND PAUSE MENU.
    /// </summary>
    void Update()
    {
    }

    //Menu and end game scene functions.
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1Scene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ToSettings()
    {
        SceneManager.LoadScene(1);
    }
    //Option function(s).
    public AudioMixer audioMixer;
    public void SetVolume(float vol)
    {
        Debug.Log(vol);
        audioMixer.SetFloat("Volume", vol);
    }
}
