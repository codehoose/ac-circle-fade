using System.Collections;
using UnityEngine;

public class AnywhereClick : MonoBehaviour
{
    public CircleWipeController _circleWipe;

    IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos = Input.mousePosition;
                var x = (mousePos.x - Screen.width / 2f);
                var y = (mousePos.y - Screen.height / 2f);
                var offset = new Vector2(x / Screen.width,y / Screen.height);
                _circleWipe._offset = offset;
                _circleWipe.FadeOut();

                yield return new WaitForSeconds(1.5f);
                _circleWipe.FadeIn();
                yield return new WaitForSeconds(1.5f);
            }
            else
            {
                yield return null;
            }
        }
    }
}
