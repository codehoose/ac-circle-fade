using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Place this script on the Main Camera in the scene. This script controls the circle wipe
/// shader that is placed over the entire screen.
/// 
/// See the example scripts in this package for example on how to use it.
/// </summary>
public class CircleWipeController : MonoBehaviour
{
    private const float RADIUS = 2f;

    public Shader shader;

    private Material material;

    [Range(0, RADIUS)]
    public float radius = 0f;

    public float horizontal = 16;

    public float verical = 9;

    public float duration = 1f;

    public Color fadeColour = Color.black;

    public Texture fadeTexture;

    public Vector2 offset;

    void Awake()
    {
        material = new Material(shader);
        UpdateShader();
    }

    void OnValidate()
    {
        material = material == null ? new Material(shader) : material;
        UpdateShader();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

    public void FadeOut(Action callback = null)
    {
        StartCoroutine(DoFade(RADIUS, 0f, callback));
    }

    public void FadeIn(Action callback = null)
    {
        StartCoroutine(DoFade(0, RADIUS, callback));
    }

    IEnumerator DoFade(float start, float end, Action callback = null)
    {
        radius = start;
        UpdateShader();

        var time = 0f;
        while (time < 1f)
        {
            radius = Mathf.Lerp(start, end, time);
            time += Time.deltaTime / duration;
            UpdateShader();
            yield return null;
        }

        radius = end;
        UpdateShader();
        callback?.Invoke();
    }

    public void UpdateShader()
    {
        var radiusSpeed = Mathf.Max(horizontal, verical);
        material.SetFloat("_Horizontal", horizontal);
        material.SetFloat("_Vertical", verical);
        material.SetFloat("_RadiusSpeed", radiusSpeed);
        material.SetFloat("_Radius", radius);
        material.SetColor("_FadeColour", fadeColour);
        material.SetTexture("_FadeTex", fadeTexture);
        material.SetVector("_Offset", offset);
    }
}
