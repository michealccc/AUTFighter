using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    private void Update()
    {
        if(Keyboard.current[Key.Enter].wasPressedThisFrame)
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        //Load scene
        SceneManager.LoadScene("MainMenu");
    }
}
