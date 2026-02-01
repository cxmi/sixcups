using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HoverColor : MonoBehaviour
{
    
    public Image image;

    public Color startingColor;
    public Color hoverColor;

    public AudioSource audioSource;
    public AudioClip audioClip;
    
    // Semitone offsets (feels pleasant for UI)
    int[] semitones = { -12, -5, -3, 0, 3, 7, 12 };
    int[] lowSemitones = { -24, -17, -15, -12, -9, -5, 0 };
    int[] sadSemitones = { -24, -21, -19, -17, -14, -12, -9, -7, -5, -2, 0 };
    int[] pentatonic = { 0, 2, 4, 7, 9 };
    


    private bool audioPlaying;
    //public Scene destinationScene;
    public string destSceneName;

    public LoveStoriesTextManager inkManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        image = GetComponent<Image>();
        startingColor = image.color;
        inkManager = FindFirstObjectByType<LoveStoriesTextManager>();
        
        //destSceneName = destinationScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {

        if (!inkManager.canClick)
        {
            //maybe theres other sounds that can be played here
            
            if (!audioPlaying)
            {
                int semi = sadSemitones[Random.Range(0, sadSemitones.Length)];
                //int semi = pentatonic[Random.Range(0, pentatonic.Length)];
            
                audioSource.pitch = Mathf.Pow(2f, semi / 12f);
                audioSource.PlayOneShot(audioClip, 0.01f);
                audioPlaying = true;
            }
            return;
        }

        if (!audioPlaying)
        {
            int semi = sadSemitones[Random.Range(0, sadSemitones.Length)];
            //int semi = pentatonic[Random.Range(0, pentatonic.Length)];
            
            audioSource.pitch = Mathf.Pow(2f, semi / 12f);
            audioSource.PlayOneShot(audioClip, 0.5f);
            audioPlaying = true;
        }
        image.color = hoverColor;

    }

    private void OnMouseExit()
    {
        image.color = startingColor;
        audioPlaying = false;
    }


}
