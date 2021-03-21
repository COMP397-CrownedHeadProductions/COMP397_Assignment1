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
    public List<MonoBehaviour> scripts;

    [Header("Player")]
    public PlayerController player;
    public CameraController playerCamera;

    [Header("Enemy")]
    public RangeEnemyController rngEnem;
    public RangeEnemyController rHealth;

    [Header("Scene")]
    public SceneDataSO sceneData;

    void Start()
    {
        ResumeGame();
        playerCamera = FindObjectOfType<CameraController>();
        player = FindObjectOfType<PlayerController>();
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResumeGame();
                playerCamera.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                OnSaveButtonPressed();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                OnLoadButtonPressed();
            }
        }
    }
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.None;
        PauseMenuUI.SetActive(false);        
        Time.timeScale = 1f;
        isPaused = false;
        foreach (var script in scripts)
        {
            script.enabled = isPaused;
        }
        Debug.Log("Resume");
    }
    void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        foreach (var script in scripts)
        {
            script.enabled = !isPaused;
        }
        Debug.Log("Pause");
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void OnLoadButtonPressed()
    {
        player.controller.enabled = false;
        player.transform.position = sceneData.playerPosition;
        player.transform.rotation = sceneData.playerRotation;
        playerCamera.transform.position = sceneData.cameraPosition;
        playerCamera.transform.rotation = sceneData.cameraRotation;
        player.currentHealth = sceneData.playerHealth;
        player.healthBar.SetHealth(sceneData.playerHealth);
        //Enemy
        rngEnem.transform.position = sceneData.rangeEnemPos;
        rngEnem.transform.rotation = sceneData.rangeEnemRot;
        rngEnem.rHealth = sceneData.rangeEnemHealth;
    }
    public void OnSaveButtonPressed()
    {
        sceneData.playerPosition = player.transform.position;
        sceneData.playerRotation = player.transform.rotation;
        sceneData.cameraRotation = playerCamera.transform.rotation;
        sceneData.cameraPosition = playerCamera.transform.position;
        sceneData.playerHealth = player.currentHealth;

        sceneData.rangeEnemPos = rngEnem.transform.position;
        sceneData.rangeEnemRot = rngEnem.transform.rotation;
        sceneData.rangeEnemHealth = rngEnem.rHealth;
    }
}
