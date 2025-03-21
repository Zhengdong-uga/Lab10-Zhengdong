using Oculus.Avatar2.Experimental;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{


    [Header("Animation Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private string trigger1 = "Trigger1"; // Trigger for Animation 1
    [SerializeField] private string trigger2 = "Trigger2"; // Trigger for Animation 2

    [Header("Input Settings")]
    [SerializeField] private OVRInput.RawButton trigger1Button = OVRInput.RawButton.A; // Default: A Button
    [SerializeField] private OVRInput.RawButton trigger2Button = OVRInput.RawButton.B; // Default: B Button

    [Header("Controller Settings")]
    [SerializeField] private OVRInput.Controller controller = OVRInput.Controller.RTouch; // Default: Right Hand

    private void Start()
    {
        Debug.Log("Animation manager initialized");
    }

    void Update()
    {
        if (animator == null)
        {
            Debug.LogError("MetaXRAnimationTrigger: Animator is not assigned!");
            return;
        }

        // Check if Trigger 1 (A Button) is pressed
        if (OVRInput.GetDown(trigger1Button, controller))
        {
            animator.SetTrigger(trigger1);
            Debug.Log($"Animation Triggered: {trigger1}");
        }

        // Check if Trigger 2 (B Button) is pressed
        if (OVRInput.GetDown(trigger2Button, controller))
        {
            animator.SetTrigger(trigger2);
            Debug.Log($"Animation Triggered: {trigger2}");
        }
    }
}
