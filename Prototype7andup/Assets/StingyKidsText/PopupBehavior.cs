using Ink.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image image;
    public Sprite inactiveWindow;
    public Sprite activeWindow;
    public Transform parentTransform;
    //public GameObject textManager;
    public StingyKidsText stingyKidsText;
    public GameObject mainWindow;
    public Canvas canvas;
    private static PopupBehavior lastClicked;

    
    [SerializeField] private TextAsset relevantInkJSON;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        //Physics2D.queriesHitTriggers = true;
        stingyKidsText = FindFirstObjectByType<StingyKidsText>();
        canvas = GetComponentInParent<Canvas>();
        
    }

    public void ReplaceJson()
    { 
        stingyKidsText.inkJSONAsset = relevantInkJSON;

       // GameObject instance = Instantiate(mainWindow, canvas.transform);
       // RectTransform rectTransform = instance.GetComponent<RectTransform>();
       // rectTransform.anchoredPosition3D = new Vector3(594f, 230f, 0f);
       //
       // rectTransform.localRotation = Quaternion.identity;
       
       stingyKidsText.StartStory();
       gameObject.SetActive(false);
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

    // private void OnMouseDown()
    // {
    //     Debug.Log("OnMouseDown");
    //     //gameObject.transform.parent = parentTransform;
    //     //image.sprite = activeWindow;
    //    
    // }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (stingyKidsText.storyIsPlaying) return;
         
        //gameObject.transform.parent = parentTransform;
        
        // If another object was previously clicked, reset its image

        if (lastClicked != null && lastClicked != this)
        {
            lastClicked.image.sprite = lastClicked.inactiveWindow;
        }
        //bring window to front
        gameObject.transform.SetParent(parentTransform, true);
        gameObject.transform.SetAsLastSibling();
        image.sprite = activeWindow;
        lastClicked = this;
        
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("pointer up"); 
    }
}
