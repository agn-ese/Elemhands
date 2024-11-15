using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreenUI : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    private Renderer fadeRenderer;

    // Start is called before the first frame update
    void Start()
    {
        fadeRenderer = GetComponent<Renderer>();
        if(fadeOnStart) FadeIn();
    }

    public void FadeIn()
    {
        Fade(1, 0);
        this.gameObject.SetActive(false);
    }

    public void FadeOut()
    {
        this.gameObject.SetActive(true);
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeCoroutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeCoroutine(float alphaIn, float alphaOut)
    {
        float time = 0;

        while (time <= fadeDuration)
        {
            Color color = fadeColor;
            color.a = Mathf.Lerp(alphaIn, alphaOut, time / fadeDuration);

            fadeRenderer.material.SetColor("_Color", color);

            time += Time.deltaTime;
            yield return null;
        }

        Color second_color = fadeColor;
        second_color.a = alphaOut;
        fadeRenderer.material.SetColor("_Color", second_color);
    }
}
