using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    /// <summary>
    /// Pause can only be done by pressing esc in
    /// the game but resume can be done by both 
    /// pressing esc or the resume button.
    /// </summary>
    /// 
    public bool isPaused=false;
    public GameObject PauseMenuUI;

    void Start()
    {
        ResumeGame();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        if (isPaused == true)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToMenu();
            }
            else 
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResumeGame();
            }            
        }
    }
    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);        
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Resume");
    }
    void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Debug.Log("Pause");
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }    
}
