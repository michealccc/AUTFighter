using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StPaulStreet : MonoBehaviour
{

    public void SceneChanger()
    {
        SceneManager.LoadScene("StPaulStreetStage");
    }


    /* public float transitionTime = 0.01f;

    public void SceneChanger()
    {
        StartCoroutine(LoadLevel(1));
        
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);

    }*/

}
