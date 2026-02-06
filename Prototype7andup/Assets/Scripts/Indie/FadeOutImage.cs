using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutImage : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 1.5f;
    

    private void Start()
    {
        FadeOut();
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        Color color = image.color;
        color.a = 1f;
        image.color = color;

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            image.color = color;
            yield return null;
        }

        // Ensure final alpha is exactly 1
        color.a = 0f;
        image.color = color;
    }
}