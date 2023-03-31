using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneWithLoadingScreen()
    {
        // Load the "LoadingScene" scene
        // Call the "GoToDemoPage" method after 5 seconds using a coroutine
        StartCoroutine(GotoLoadingScreen());
    }

    IEnumerator GotoLoadingScreen()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }


    public void LoadBackDemoPage()
    {
        // Load the "LoadingScene" scene
        // Call the "GoToDemoPage" method after 5 seconds using a coroutine
        StartCoroutine(GoBackDemoPage());
    }

    IEnumerator GoBackDemoPage()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
