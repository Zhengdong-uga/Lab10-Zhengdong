using System.Collections;
using UnityEngine;
using Oculus.Interaction; // Ensure you have the Meta Interaction SDK imported

public class XRHoverEffect : MonoBehaviour
{
    [Header("Hover Effect Settings")]
    public float hoverHeight = 0.1f; // How much the object moves up when hovered
    public float transitionSpeed = 5f; // Speed of movement transition
    private Vector3 originalPosition; // Store original position

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void OnHover()
    {
        Debug.Log("Hover event triggered!"); // Add debug to check if function runs
        StopAllCoroutines();
        StartCoroutine(SmoothMove(originalPosition + new Vector3(0, hoverHeight, 0)));
    }

    public void OnUnhover()
    {
        Debug.Log("Unhover event triggered!"); // Add debug to check if function runs
        StopAllCoroutines();
        StartCoroutine(SmoothMove(originalPosition));
    }

    private IEnumerator SmoothMove(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * transitionSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            yield return null;
        }
        transform.position = targetPosition;
    }
}
