using System.Collections;
using UnityEngine;

public class RotateUI : MonoBehaviour
{
    public float rotationSpeed = 30f; // degrees per second
    public float fastSpeed = -10f;
    public float rampUpTime = 0.1f;
    public float holdTime = 0.2f;
    public float rampDownTime = 0.3f;

    private Coroutine spinRoutine;

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    public void FastSpin()
    {
        // Prevent stacking multiple spins
        if (spinRoutine != null)
            StopCoroutine(spinRoutine);

        spinRoutine = StartCoroutine(FastSpinRoutine());
    }
    
    private IEnumerator FastSpinRoutine()
    {
        float originalSpeed = rotationSpeed;

        // Ramp up
        float t = 0f;
        while (t < rampUpTime)
        {
            t += Time.deltaTime;
            rotationSpeed = Mathf.Lerp(originalSpeed, fastSpeed, t / rampUpTime);
            yield return null;
        }

        rotationSpeed = fastSpeed;

        // Hold fast spin
        yield return new WaitForSeconds(holdTime);

        // Ramp down
        t = 0f;
        while (t < rampDownTime)
        {
            t += Time.deltaTime;
            rotationSpeed = Mathf.Lerp(fastSpeed, originalSpeed, t / rampDownTime);
            yield return null;
        }

        rotationSpeed = originalSpeed;
        spinRoutine = null;
    }

}
