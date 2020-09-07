using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
    [SerializeField] private Transform pausePanel;

    [SerializeField] private Text timeText;

    private bool _isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.gameObject.SetActive(false);
        _isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "Time Since Startup: " + Time.timeSinceLevelLoad;

        if (Keyboard.current.escapeKey.isPressed && !_isPaused)
        {
            Pause();
        }
        else if (Keyboard.current.escapeKey.isPressed && _isPaused)
        {
            UnPause();
        }
    }

    public void Pause()
    {
        _isPaused = true;
        pausePanel.gameObject.SetActive(true); // Turn on the pause menu
        Time.timeScale = 0f; // Pause the game
    }

    public void UnPause()
    {
        _isPaused = false;
        pausePanel.gameObject.SetActive(false); // Turn off the pause menu
        Time.timeScale = 1f; // Resume the game
    }

    public void Restart()
    {
        SceneManager.LoadScene(5);
    }

    public void Quit()
    {
        Application.Quit();
    }
}