using UnityEngine;

public class ChoiceClicker : MonoBehaviour
{
    public int choiceIndex;
    public LoveStoriesTextManager inkManager;
    public string choiceText;

    void OnMouseDown()
    {
        Debug.Log("Clicked");
        inkManager.ChooseByText(choiceText + inkManager.sceneIndex);

    }
    
    
}