using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    
    public Image image;

    public Color startingColor;
    public Color hoverColor;

    //public Scene destinationScene;
    public string destSceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }

    private void OnMouseExit()
    {
        image.color = startingColor;
    }

    public void OnMouseDown()
    {
        SceneManager.LoadScene(destSceneName);
    }
}
