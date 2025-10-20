using System;
using System.Collections;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewInkTest : MonoBehaviour
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJSONAsset;
    private Story story;

    [Header("UI References")]
    [SerializeField] private ScrollRect scrollRect;                // Scroll View component
    [SerializeField] private Transform contentParent;              // ScrollView/Viewport/Content
    [SerializeField] private TextMeshProUGUI messagePrefab;        // Prefab for story messages
    [SerializeField] private Button choiceButtonPrefab;            // Prefab for choice buttons

    private void Start()
    {
       // scrollRect.verticalNormalizedPosition = 0f;

        if (inkJSONAsset == null)
        {
            Debug.LogError("No Ink JSON file assigned.");
            return;
        }

        story = new Story(inkJSONAsset.text);
        StartCoroutine(PlayStory());
    }

    private IEnumerator PlayStory()
    {
        while (story.canContinue)
        {
            string line = story.Continue().Trim();
            AddMessage(line);
            //yield return new WaitForSeconds(0.25f); // Add delay for chat feel
            
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
            AddMessage("<i>End of story.</i>");
        }
    }

    private void AddMessage(string text)
    {
        var message = Instantiate(messagePrefab, contentParent);
        message.text = text;

        
        
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
        while (!Input.GetKeyDown(KeyCode.Space))
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
}
