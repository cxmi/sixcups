using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialClick : MonoBehaviour
{
    public TutorialRevealer revealer;
    public RotateUI rotateUi;



    void Awake()
    {
        revealer = FindFirstObjectByType<TutorialRevealer>();
    }
    void OnMouseDown()
    {
        rotateUi = GetComponentInParent<RotateUI>();
        rotateUi.FastSpin();
        revealer.RevealNextImage();


    }
    
}
