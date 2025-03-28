// (c) Meta Platforms, Inc. and affiliates. Confidential and proprietary.

using UnityEngine;
using Oculus.Haptics;
using System;
using Oculus.Interaction;

public class XRHapticInteraction : MonoBehaviour
{
    [Header("Haptic Clips")]
    [SerializeField] private HapticClip clip1;  // Clip for hover feedback
    [SerializeField] private HapticClip clip2;
    private HapticClipPlayer leftClipPlayer1;
    private HapticClipPlayer leftClipPlayer2;
    private HapticClipPlayer rightClipPlayer1;
    private HapticClipPlayer rightClipPlayer2;// Clip for select feedback

    [Header("Target Interactor")]
    [SerializeField] private DistanceGrabInteractor leftInteractor; // Adjuste the interactor type as needed
    [SerializeField] private DistanceGrabInteractor rightInteractor;

    [Header("Target Interactable")]
    [SerializeField] private DistanceGrabInteractable targetInteractable; // Adjust the interactable type as needed
 

    protected virtual void Start()
    {

        leftClipPlayer1 = new HapticClipPlayer(clip1);
        leftClipPlayer2 = new HapticClipPlayer(clip2);
        rightClipPlayer1 = new HapticClipPlayer(clip1);
        rightClipPlayer2 = new HapticClipPlayer(clip2);

        leftClipPlayer2.priority = 1;
        rightClipPlayer2.priority = 1;
    }

    public void Update()
    {
        // Right Controller Select target
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch) && rightInteractor.Interactable == targetInteractable)
        {
            rightClipPlayer2.isLooping = true;
            rightClipPlayer2.Play(Controller.Right);
        }

        // Right Controller Unselect target
        else if(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch) || rightInteractor.Interactable != targetInteractable)
        {
       
            rightClipPlayer1.Stop();
            rightClipPlayer2.Stop();
        }

        // Right Controller Hover 
        else if (rightInteractor.HasInteractable && !rightInteractor.HasSelectedInteractable && rightInteractor.Interactable == targetInteractable)
        {
            rightClipPlayer1.isLooping = true;
            rightClipPlayer1.Play(Controller.Right);
        }

        // Right Controller UnHover
        else if(rightInteractor.ShouldUnhover)
        {
            rightClipPlayer1.Stop();
            rightClipPlayer2.Stop();
        }


    }

    public void PlayHoverHaptics()
    {
 
        rightClipPlayer1.isLooping = true;
        rightClipPlayer1.Play(Controller.Right);

        leftClipPlayer1.isLooping = true;
        leftClipPlayer1.Play(Controller.Left);
    }

    public void StopHoverHaptics()
    {
        rightClipPlayer1.Stop();
        leftClipPlayer1.Stop();
    }

    public void PlaySelectHaptics()
    {
        OVRInput.Controller activeController = OVRInput.GetActiveController();
        Debug.Log("Select Active Controller: " + activeController);
        //TODO 
        Debug.Log("Select Should feel vibration right");
        rightClipPlayer2.isLooping = true;
        rightClipPlayer2.Play(Controller.Right);
        rightClipPlayer2.isLooping = true;
        leftClipPlayer2.Play(Controller.Left);
    }

    public void StopSelectHaptics()
    {
        rightClipPlayer2.Stop();
        leftClipPlayer2.Stop();
    }

    protected virtual void OnDestroy()
    {
        leftClipPlayer1?.Dispose();
        leftClipPlayer2?.Dispose();
        rightClipPlayer1?.Dispose();
        rightClipPlayer2?.Dispose();
    }


    /// <summary>
    /// Dispose of haptics on application quit.
    /// </summary>
    protected virtual void OnApplicationQuit()
    {
        Haptics.Instance.Dispose();
    }
}
