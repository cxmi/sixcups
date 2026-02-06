using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRevealer : MonoBehaviour
{
    public Image[] images;   // Assign in Inspector
    private int currentIndex = 0;
  
    void Start()
    {
        // Make sure all images start hidden
        foreach (Image img in images)
        {
            img.gameObject.SetActive(false);
        }
    }
    public void RevealNextImage()
    {
        if (currentIndex >= images.Length)
            return;

        Image img = images[currentIndex];
        img.gameObject.SetActive(true);
        StartCoroutine(FadeIn(img));

        currentIndex++;
    }

    IEnumerator FadeIn(Image img)
    {
        float duration = 1.5f; // fade time
        float elapsed = 0f;

        Color color = img.color;
        color.a = 0f;
        img.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / duration);
            img.color = color;
            yield return null;
        }

        color.a = 1f;
        img.color = color;
    }
}
