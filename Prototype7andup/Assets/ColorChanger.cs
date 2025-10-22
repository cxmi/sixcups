using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    
    public Image image;

    public Color startingColor;
    public Color hoverColor;

    public AudioSource audioSource;
    public AudioClip audioClip;

    private bool audioPlaying;
    //public Scene destinationScene;
    public string destSceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        image = GetComponent<Image>();
        startingColor = image.color;
        //destSceneName = destinationScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        image.color = hoverColor;
        if (!audioPlaying)
        {
            audioSource.PlayOneShot(audioClip);
            audioPlaying = true;
        }
    }

    private void OnMouseExit()
    {
        image.color = startingColor;
        audioPlaying = false;
    }

    public void OnMouseDown()
    {
        SceneManager.LoadScene(destSceneName);
    }
}
