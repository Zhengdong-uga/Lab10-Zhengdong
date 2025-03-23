using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using Meta.WitAi.Dictation;
using Meta.WitAi.Dictation.Events;
using Meta.WitAi.Requests;
using Oculus.Voice.Dictation;
using TMPro;

public class VoiceCommand : MonoBehaviour
{
    [Header("Dictation Experience")]
    public AppDictationExperience dictationExperience; // Reference to the Meta Dictation Experience

    [Header("Recognized Keywords")]
    public List<string> keywords = new List<string> { "left", "right", "up", "down" };

    [Header("Keyword UI")]
    public TextMeshPro keywordDisplay; // UI text to display detected keyword

    [Header("Interactive Object")]
    public GameObject targetObject; // Assign target object

    public float moveDistance = 0.3f; // How far the object moves per command
    public float moveDuration = 0.3f; // Time taken to move

    private bool dictationRunning = false; // Prevents duplicate dictation activation

    private void Start()
    {
        if (dictationExperience == null)
        {
            Debug.LogError("❌ DictationExperience is not assigned! Please attach it in the Inspector.");
            return;
        }

        StartDictation(); // Ensure dictation starts automatically

        // Subscribe to transcription events
        dictationExperience.DictationEvents.OnPartialTranscription.AddListener(OnPartialTranscriptionReceived);
        dictationExperience.DictationEvents.OnFullTranscription.AddListener(OnFullTranscriptionReceived);
        dictationExperience.DictationEvents.OnStoppedListening.AddListener(OnDictationStopped);
    }

    private void OnDestroy()
    {
        if (dictationExperience != null)
        {
            dictationExperience.DictationEvents.OnPartialTranscription.RemoveListener(OnPartialTranscriptionReceived);
            dictationExperience.DictationEvents.OnFullTranscription.RemoveListener(OnFullTranscriptionReceived);
            dictationExperience.DictationEvents.OnStoppedListening.RemoveListener(OnDictationStopped);
        }
    }

    // Listen for partial transcriptions (spoken but not finalized)
    private void OnPartialTranscriptionReceived(string transcription)
    {
        Debug.Log($"Partial Transcription: {transcription}");
        DetectKeywords(transcription);
    }

    // Listen for full transcriptions (finalized spoken text)
    private void OnFullTranscriptionReceived(string transcription)
    {
        Debug.Log($"🎙 Full Transcription: {transcription}");
        DetectKeywords(transcription);
    }

    // Restart dictation if it stops
    private void OnDictationStopped()
    {
        Debug.Log("🎤 Dictation Stopped, Restarting...");
        RestartDictation(); // Ensures continuous dictation
    }

    // Check if a recognized keyword is spoken
    private void DetectKeywords(string transcription)
    {
        string processedText = transcription.Trim().ToLower();

        foreach (string keyword in keywords)
        {
            if (processedText.Contains(keyword.ToLower()))
            {
                Debug.Log($"✅ Recognized Keyword: '{keyword}'");

                // Ensure UI updates on the main thread
                UpdateKeywordDisplay(keyword);

                // Execute movement
                HandleKeywordAction(keyword);

                return; // Stop checking after first match
            }
        }
    }

    // Update the UI text with the detected keyword
    private void UpdateKeywordDisplay(string keyword)
    {
        if (keywordDisplay != null)
        {
            keywordDisplay.text = $"Detected: {keyword}";
        }
        else
        {
            Debug.LogWarning("⚠ TextMeshProUGUI keywordDisplay is not assigned!");
        }
    }

    // Perform an action based on the recognized keyword
    private void HandleKeywordAction(string keyword)
    {
        if (targetObject == null)
        {
            Debug.LogWarning("⚠ Target object is not assigned!");
            return;
        }

        Vector3 newPosition = targetObject.transform.position;

        switch (keyword.ToLower())
        {
            case "up":
                Debug.Log("⬆ Moving Up");
                newPosition += Vector3.up * moveDistance;
                break;
            case "down":
                Debug.Log("⬇ Moving Down");
                newPosition -= Vector3.up * moveDistance;
                break;
            case "left":
                Debug.Log("⬅ Moving Left");
                newPosition += Vector3.left * moveDistance;
                break;
            case "right":
                Debug.Log("➡ Moving Right");
                newPosition += Vector3.right * moveDistance;
                break;
            default:
                Debug.Log("⚠ Unhandled Keyword.");
                return;
        }

        // Move object smoothly
        StartCoroutine(MoveObject(targetObject, newPosition, moveDuration));

        // Restart dictation after processing a keyword
        RestartDictation();
    }

    // Coroutine to smoothly move the object
    private IEnumerator MoveObject(GameObject obj, Vector3 targetPos, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startPos = obj.transform.position;

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPos; // Ensure it reaches the final position
    }

    // Manually start dictation
    public void StartDictation()
    {
        if (dictationExperience != null && !dictationExperience.Active)
        {
            dictationExperience.Activate();
            dictationRunning = true;
            Debug.Log("Dictation Started...");
        }
    }

    // Restart dictation after a short delay
    private void RestartDictation()
    {
        if (!dictationRunning) return; // Prevent duplicate activations
        Invoke(nameof(StartDictation), 0.2f); // Small delay to prevent overlapping requests
    }

    // Manually stop dictation
    public void StopDictation()
    {
        if (dictationExperience != null)
        {
            dictationExperience.Deactivate();
            dictationRunning = false;
            Debug.Log("Dictation Stopped...");
        }
    }
}
