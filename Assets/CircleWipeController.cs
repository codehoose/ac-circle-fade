using System.Collections;
using UnityEngine;

public class CircleWipeController : MonoBehaviour
{
    public Shader shader;

    private Material material;

    [Range(0, 1.2f)]
    public float _radius = 0f;

    public float _horizontal = 16;

    public float _verical = 9;

    public float _duration = 1f;

    void Start()
    {
        material = new Material(shader);
        _radius = 1.2f;
        UpdateShader();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

    public void FadeOut()
    {
        StartCoroutine(DoFade(1.2f, 0f));
    }

    public void FadeIn()
    {
        StartCoroutine(DoFade(0, 1.2f));
    }

    IEnumerator DoFade(float start, float end)
    {
        _radius = start;
        UpdateShader();

        var time = 0f;
        while (time < 1f)
        {
            _radius = Mathf.Lerp(start, end, time);
            time += Time.deltaTime / _duration;
            UpdateShader();
            yield return null;
        }

        _radius = end;
        UpdateShader();
    }

    private void UpdateShader()
    {
        var radiusSpeed = Mathf.Max(_horizontal, _verical);
        material.SetFloat("_Horizontal", _horizontal);
        material.SetFloat("_Vertical", _verical);
        material.SetFloat("_RadiusSpeed", radiusSpeed);
        material.SetFloat("_Radius", _radius);
    }
}
