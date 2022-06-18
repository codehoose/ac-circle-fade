using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CircleWipeController))]
public class FadeToNextScene : MonoBehaviour
{
    [Tooltip("The number of seconds to wait before fading to black and then loading the next scene")]
    public float waitTime = 5f; 

    [Tooltip("The scene to load. Make sure this scene is in the scene list")]
    public string nextScene = "NextScene";

    IEnumerator Start()
    {
        CircleWipeController circleWipeController = GetComponent<CircleWipeController>();

        yield return new WaitForSeconds(waitTime);

        circleWipeController.FadeOut(() => LoadScene());
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
