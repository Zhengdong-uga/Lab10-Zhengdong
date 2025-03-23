using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityExample : MonoBehaviour
{
    private XRColorStates _xrColorStates;

    private void Start()
    {
        _xrColorStates = this.GetComponent<XRColorStates>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "entered.");
        _xrColorStates.ChangeToHoverColor();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name + "extied.");
        _xrColorStates.ChangeToDefaultColor();
    }
}
