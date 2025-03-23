using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRColorStates : MonoBehaviour
{
    [Header("Color Settings")]
    public Color defaultColor = Color.white;
    public Color hoverColor = Color.yellow;
    public Color selectColor = Color.green;
    public float transitionSpeed = 5f;
    // Start is called before the first frame update
    private Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.SetColor("_Color", defaultColor);
    }

    public void ChangeToDefaultColor()
    {
        StartCoroutine(SmoothColorChange(defaultColor));
    }

    public void ChangeToHoverColor()
    {
        StartCoroutine(SmoothColorChange(hoverColor));
    }

    public void ChangetToSelectColor()
    {
        StartCoroutine(SmoothColorChange(selectColor));
    }
    private IEnumerator SmoothColorChange(Color targetColor)
    {
        Color startColor = material.color;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * transitionSpeed;
            material.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }
       material.color = targetColor; // Ensure final color is exact
    }
}
