using System.Collections;
using UnityEngine;

public class AnywhereClick : MonoBehaviour
{
    /// <summary>
    /// The circle wipe controller instance to be controlled. Will
    /// automatically pick up one attached to the same GameObject
    /// as this script.s
    /// </summary>
    [Tooltip("The circle wipe controller instance to be controlled. Will automatically pick up one attached to the same GameObject as this script.")]
    public CircleWipeController circleWipe;

    /// <summary>
    /// Start listening for a mouse click.
    /// </summary>
    IEnumerator Start()
    {
        if (circleWipe == null)
        {
            circleWipe = GetComponent<CircleWipeController>();
        }

        if (circleWipe==null)
        {
            Debug.LogError("AnywhereClick requires a CircleWipeController to be set");
        }

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos = Input.mousePosition;
                var x = (mousePos.x - Screen.width / 2f);
                var y = (mousePos.y - Screen.height / 2f);
                var offset = new Vector2(x / Screen.width,y / Screen.height);
                circleWipe.offset = offset;
                circleWipe.FadeOut();

                yield return new WaitForSeconds(circleWipe.duration);
                circleWipe.FadeIn();
                yield return new WaitForSeconds(circleWipe.duration);
            }
            else
            {
                yield return null;
            }
        }
    }
}
