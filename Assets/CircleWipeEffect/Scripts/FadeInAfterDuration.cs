using System.Collections;
using UnityEngine;

public class FadeInAfterDuration : MonoBehaviour
{
    /// <summary>
    /// The circle wipe controller instance to be controlled. Will
    /// automatically pick up one attached to the same GameObject
    /// as this script.s
    /// </summary>
    [Tooltip("The circle wipe controller instance to be controlled. Will automatically pick up one attached to the same GameObject as this script.")]
    public CircleWipeController circleWipe;

    public float waitTime = 5f;
    
    IEnumerator Start()
    {
        if (circleWipe == null)
        {
            circleWipe = GetComponent<CircleWipeController>();
        }

        if (circleWipe == null)
        {
            Debug.LogError("AnywhereClick requires a CircleWipeController to be set");
        }

        yield return new WaitForSeconds(waitTime);
        circleWipe.FadeIn();
    }
}
