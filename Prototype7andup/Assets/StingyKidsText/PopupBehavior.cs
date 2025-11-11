using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupBehavior : MonoBehaviour
{
    public Image image;
    public Sprite inactiveWindow;
    public Sprite activeWindow;
    public Transform parentTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        Physics2D.queriesHitTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseOver()
    {
        // image.color = hoverColor;
        // if (!audioPlaying)
        // {
        //     audioSource.PlayOneShot(audioClip, 0.7f);
        //     audioPlaying = true;
        }
    //}

    private void OnMouseExit()
    {
        // image.color = startingColor;
        // audioPlaying = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        //gameObject.transform.parent = parentTransform;
        //image.sprite = activeWindow;
       
    }
    
  
}
