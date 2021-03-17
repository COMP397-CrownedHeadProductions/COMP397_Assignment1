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
    public CameraController playerCamera;


    void Start()
    {
        ResumeGame();
        playerCamera = FindObjectOfType<CameraController>();
    }
    // Update is called once per frame
    void Update()
    {
        //Pause Menu Function
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isPaused)
            {
                ResumeGame();
                //Cursor.lockState = CursorLockMode.Locked;
                playerCamera.enabled = true;
            }
            else
            {
                PauseGame();
                playerCamera.enabled = false;
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
                playerCamera.enabled = true;
            }            
        }
    }
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
