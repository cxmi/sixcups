using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BeginButton : MonoBehaviour
{
    
    public Image image;

    public Color startingColor;
    public Color hoverColor;
    
    public string destSceneName;

    public Image fadeImage;
    public float fadeDuration = 1f;

    private bool isTransitioning = false;
    
    public LoveStoriesTextManager inkManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        startingColor = image.color;
    }

   

    private void OnMouseOver()
    {
        
        image.color = hoverColor;

    }

    private void OnMouseExit()
    {
        image.color = startingColor;
    }
    
    
    private void OnMouseDown()
    {
        if (!isTransitioning)
            StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        isTransitioning = true;

        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));

        SceneManager.LoadScene(destSceneName);
    }

    IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(from, to, elapsed / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;
    }

}
