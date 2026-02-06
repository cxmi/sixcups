using System.Collections;
using UnityEngine;

public class PleaseBeginScript : MonoBehaviour
{
    public CanvasGroup targetCanvasGroup;

    [Header("Fade Timings")]
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 0.5f;

    private bool hasClicked = false;

    void Start()
    {
        // Start hidden
        targetCanvasGroup.alpha = 0f;
        targetCanvasGroup.interactable = false;
        targetCanvasGroup.blocksRaycasts = false;

        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (!hasClicked && Input.GetMouseButtonDown(0))
        {
            hasClicked = true;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;

        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            targetCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeInDuration);
            yield return null;
        }

        targetCanvasGroup.alpha = 1f;
        targetCanvasGroup.interactable = true;
        targetCanvasGroup.blocksRaycasts = true;
    }

    IEnumerator FadeOut()
    {
        float elapsed = 0f;

        targetCanvasGroup.interactable = false;
        targetCanvasGroup.blocksRaycasts = false;

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            targetCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeOutDuration);
            yield return null;
        }

        targetCanvasGroup.alpha = 0f;
    }
}