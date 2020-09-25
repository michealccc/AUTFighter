using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    [Header("References")]
    
    [SerializeField, Tooltip("Game object for the pause menu.")]
    private GameObject pauseMenu = default;

    [SerializeField, Tooltip("Game object for the pause settings menu.")]
    private GameObject pauseSettingsMenu = default;

    [SerializeField, Tooltip("Toggle component for frame rate counter.")]
    private Toggle frameRateCounterToggle = default;

    private FrameRateCounter _frameRateCounter;

    // Start is called before the first frame update
    void Start()
    {
        _frameRateCounter = FindObjectOfType<FrameRateCounter>();

        if (_frameRateCounter == null)
        {
            Debug.LogError("FrameRate Counter can not be found");
        }

        pauseMenu.SetActive(false);
        pauseSettingsMenu.SetActive(false);

        frameRateCounterToggle.SetIsOnWithoutNotify(_frameRateCounter.IsShowing);
        frameRateCounterToggle.onValueChanged.AddListener(OnFramerateCounterChanged);
    }
    
    public void ClosePause()
    {
        SetPauseMenuActivation(false);
    }

    public void TogglePause()
    {
        SetPauseMenuActivation(!(pauseMenu.activeSelf || pauseSettingsMenu.activeSelf));
    }
    
    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard.tabKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogPause(InputAction.CallbackContext context)
    {
        Debug.Log("Paused!");
    }

    void SetPauseMenuActivation(bool active)
    {
        pauseMenu.SetActive(active);
        pauseSettingsMenu.SetActive(false);
        
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;

            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    void OnFramerateCounterChanged(bool newValue)
    {
        _frameRateCounter.Show(newValue);
    }

    public void RestartMatch()
    {
        SceneManager.LoadScene("MatchScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}