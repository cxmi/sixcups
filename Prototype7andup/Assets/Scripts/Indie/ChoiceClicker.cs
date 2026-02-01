using UnityEngine;

public class ChoiceClicker : MonoBehaviour
{
    public int choiceIndex;
    public LoveStoriesTextManager inkManager;
    public string choiceText;
    public RotateUI rotateUi;

    void OnMouseDown()
    {
        if (!inkManager.canClick) return;
        Debug.Log("Clicked");
        rotateUi = GetComponentInParent<RotateUI>();
        inkManager.ChooseByText(choiceText + inkManager.sceneIndex);
        rotateUi.FastSpin();

    }
    
    
}