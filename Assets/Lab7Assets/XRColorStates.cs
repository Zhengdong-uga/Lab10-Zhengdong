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
    public bool useSmoothTransition = false;
    // Start is called before the first frame update
    private Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.SetColor("_Color", defaultColor);
    }

    private void ChangeColor(Color targetColor)
    {
        if (useSmoothTransition) 
        {
            StopAllCoroutines();
            StartCoroutine(SmoothColorChange(targetColor));
        }
        else
        {
            material.color = targetColor;
        }
    }

    public void ChangeToDefaultColor()
    {
        ChangeColor(defaultColor);
    }

    public void ChangeToHoverColor()
    {
        ChangeColor(hoverColor);
    }

    public void ChangetToSelectColor()
    {
        ChangeColor(selectColor);
    }
    private IEnumerator SmoothColorChange(Color targetColor)
    {
        Color startColor = material.color;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * transitionSpeed;
            material.color = Color.Lerp(startColor, targetColor, t);
            yield return null; //new WaitForSecond() ;
        }
       material.color = targetColor; // Ensure final color is exact
    }
}

//update() function can create this interation as well. 
//a coroutine is generally considered "better" than the "Update" function when you need to perform
//a long-running task that should be spread across multiple frames without blocking the main game loop,
//like loading assets, making network requests, or animating complex movements over time;