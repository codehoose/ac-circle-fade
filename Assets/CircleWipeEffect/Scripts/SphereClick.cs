using UnityEngine;

public class SphereClick : MonoBehaviour
{
    public CircleWipeController _circleWipe;

    private void OnMouseUpAsButton()
    {
        _circleWipe.FadeOut();
    }
}
