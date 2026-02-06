using System;
using UnityEngine;
using System.Collections;

public class MoveBlackBg : MonoBehaviour
{
    public RectTransform target;
    public float moveDuration = 1.5f; // seconds
    public float startX = 600f;
    public float endX = 240f;

    private void Start()
    {
        target.anchoredPosition = new Vector2(startX, target.anchoredPosition.y);

        MoveImage();
    }

    public void MoveImage()
    {
        StopAllCoroutines();
        StartCoroutine(MoveXCoroutine());
    }

    IEnumerator MoveXCoroutine()
    {

        Vector2 startPos = new Vector2(startX, target.anchoredPosition.y);
        Vector2 endPos = new Vector2(endX, target.anchoredPosition.y);

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            target.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        // Snap exactly to final position
        target.anchoredPosition = endPos;
    }
}