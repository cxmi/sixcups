using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StingyKidsText : MonoBehaviour
{
    [Header("Ink Story")]
    public TextAsset inkJSONAsset;
    private Story story;

    [Header("UI References")]
    [SerializeField] private ScrollRect scrollRect;                // Scroll View component
    [SerializeField] private Transform contentParent;              // ScrollView/Viewport/Content
    [SerializeField] private TextMeshProUGUI romanMessagePrefab;        // Prefab for story messages
    [SerializeField] private TextMeshProUGUI quoteMessagePrefab;
    [SerializeField] private Button choiceButtonPrefab;   
    
    [SerializeField] private TextMeshProUGUI messagePrefab;// Prefab for choice buttons

    [SerializeField] private String currentTag;
    public GameObject mainWindow;
    
    public AudioSource audioSource;
    public AudioClip clip;
    
    public bool storyIsPlaying = false;
    private bool noProgressing = false;
    private int rememberedStories = 0;
    private int forgottenStories = 0;
    
    // SLIDER

    public Image progressBar;
    private float duration = 1f;
    public TextMeshProUGUI progressText;
    
    //FOLDERS

    public TextMeshProUGUI keepText;
    public TextMeshProUGUI trashText;
    
    //BUTTONS

    public TextMeshProUGUI rememberText;
    public TextMeshProUGUI forgetText;
    public Color enableColor;
    public Color disableColor;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

       // scrollRect.verticalNormalizedPosition = 0f;

        if (inkJSONAsset == null)
        {
            Debug.LogError("No Ink JSON file assigned.");
            return;
        }

        
        story = new Story(inkJSONAsset.text);
        StartCoroutine(PlayStory());
    }

    public IEnumerator PlayStory()
    {
        storyIsPlaying = true;
        noProgressing = true;
        while (story.canContinue)
        {
            string line = story.Continue().Trim();
            //yield return new WaitForSeconds(0.25f); // Add delay for chat feel
            
            // Check for tags
            List<string> currentTags = story.currentTags;
            if (currentTags.Count > 0)
            {
                Debug.Log("Tags found:");
                foreach (string tag in currentTags)
                {
                    //Debug.Log("- " + tag);
                    
                    currentTag = tag;
                    // You can parse and react to tags here
                    // For example, if a tag is "#character:John", you can extract "John"
                    // and update a character portrait.
                }
            }
            
            AddMessage(line);

            
            
            yield return WaitForSpaceKey();
            
            
           
        }

        if (story.currentChoices.Count > 0)
        {
            foreach (Choice choice in story.currentChoices)
            {
                CreateChoiceButton(choice);
            }
        }
        else
        {
            //AddMessage("<i>To be continued.</i>"); //THIS IS THE ENDING THINGY
        }
        noProgressing = false;
        
    }

    private void AddMessage(string text)
    {
        
        //check tags here
        //GameObject canvasInstance = Instantiate(canvasPrefab);
       // Instantiate(messagePrefab, canvasInstance.transform);

        //Instantiate(messagePrefab, canvasInstance.transform);

        if (currentTag == "quote")
        {
            var message = Instantiate(quoteMessagePrefab, contentParent);
            message.text = text;
            audioSource.PlayOneShot(clip);

        }
        else if (currentTag == "roman")
        {
            var message = Instantiate(romanMessagePrefab, contentParent);
            message.text = text;
            audioSource.PlayOneShot(clip);
        
        }
        else
        {
            var message = Instantiate(romanMessagePrefab, contentParent);
            message.text = text;
            audioSource.PlayOneShot(clip);

        }
        

        
        
        // Force UI update before scrolling - TRYING A COROUTINE
        // Canvas.ForceUpdateCanvases();
        // scrollRect.verticalNormalizedPosition = 0f;
        
        StartCoroutine(ScrollToBottomNextFrame());

    }

    private void CreateChoiceButton(Choice choice)
    {
        var button = Instantiate(choiceButtonPrefab, contentParent);
        var tmpText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null)
        {
            tmpText.text = choice.text.Trim();
        }

        button.onClick.AddListener(() =>
        {
            story.ChooseChoiceIndex(choice.index);
            ClearChoices();
            StartCoroutine(PlayStory());
        });

        // Scroll after adding button
        // Canvas.ForceUpdateCanvases();
        // scrollRect.verticalNormalizedPosition = 0f;
        StartCoroutine(ScrollToBottomNextFrame());

    }

    private void ClearChoices()
    {
        foreach (Transform child in contentParent)
        {
            if (child.GetComponent<Button>())
            {
                Destroy(child.gameObject);
            }
        }
    }
    
    private IEnumerator WaitForSpaceKey()
    {
        // Wait until the player presses the Space key
        while (!Input.GetKeyDown(KeyCode.Space) && !Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        // Optional: Wait one frame so the key doesn't "leak" into the next input
        yield return null;
    }
    
    private IEnumerator ScrollToBottomNextFrame()
    {
        yield return null; // Wait one frame
        yield return null;    
        yield return null; 
        Canvas.ForceUpdateCanvases(); // Force layout to update now
        scrollRect.verticalNormalizedPosition = 0f; // Scroll to bottom
    }

    public void StartStory()
    { 
        
        // scrollRect = mainWindow.transform.Find("Scroll View").GetComponent<ScrollRect>();
        // contentParent = mainWindow.transform.Find("Scroll View/Viewport/Content").GetComponent<RectTransform>();

        //Debug.Log("Story can continue? " + story.canContinue);
        ClearStory();
        story = new Story(inkJSONAsset.text);

        StartCoroutine(PlayStory());

    }
    
    public void ClearStory()
    {
        if (contentParent == null)
        {
            Debug.LogWarning("ClearStory() called but contentParent is null!");
            return;
        }

        // Loop through all children of contentParent and destroy them
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        

        // Optionally scroll back to top
        if (scrollRect != null)
            scrollRect.verticalNormalizedPosition = 1f;

        Debug.Log("Story cleared!");
    }


    public void RememberStory()
    {
        if (noProgressing) return;
        // ClearStory();
        // rememberedStories++;
        // storyIsPlaying = false;

        StartCoroutine(LoadProgress());
        rememberedStories++;


    }

    public void ForgetStory()
    {
        if (noProgressing) return;
        //ClearStory();
        StartCoroutine(LoadProgress());
        forgottenStories++;
        //storyIsPlaying = false;

    }

    private IEnumerator LoadProgress()
    {
        // Reset fill
        progressBar.fillAmount = 0f;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // Ease-out curve: fast at start, slow at end
            float eased = Mathf.Sin(t * Mathf.PI * 0.5f); // Sin easing
            progressBar.fillAmount = eased;

            yield return null;
        }

        // Ensure it's fully filled
        progressBar.fillAmount = 1f;

        // Now clear story and update variables
        ClearStory();
        storyIsPlaying = false;
        progressBar.fillAmount = 0f;
        noProgressing = true;

    }

    private void Update()
    {
        int percent = (int)(progressBar.fillAmount * 100);
        progressText.text = percent.ToString()+"%";

        keepText.text = "Kept (" + rememberedStories + ")";
        trashText.text = "Forgotten (" + forgottenStories + ")";

        if (noProgressing)
        {
            rememberText.color = disableColor;
            forgetText.color = disableColor;
        }
        else
        {
            rememberText.color = enableColor;
            forgetText.color = enableColor;
        }
    }
}
