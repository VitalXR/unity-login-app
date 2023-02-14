using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedSceneLoader : MonoBehaviour
{
    [SerializeField]
    private float sceneLoadDelay = 2f;
    [SerializeField]
    private string sceneName;

    private float timeElapsed;


    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > sceneLoadDelay)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
