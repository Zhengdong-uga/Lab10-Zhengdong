/* This script is attached to the cube in our scene. It modifies the color and
transparency properties of the cube's material. We will invoke the functions 
below from the event triggers attached to the UI elements in the scene.*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// We need to import Unity's UI library to be able to use the Slider class below.
using UnityEngine.UI;

public class changeColor : MonoBehaviour
{
    /* Declaring a color variable so that we can store the state of our cube's color 
    as we interact with the UI objects in the scene.*/
    Color cubeColor = new Color(1, 1, 1, 1);
    
    /* This function will be invoked when we press the button in the scene. 
     * The function first creates a new color by randomizing the R, G, and B values
     * of our cubeColor variable. We then assign this new random color to the 
     * material of our cube.*/
    public void ChangeColorOnClick()
    {
        cubeColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), cubeColor.a);
        GetComponent<Renderer>().material.color = cubeColor;
    }

    /* This function will be invoked when we move the slider in the scene. 
     * On the slider's "Drag" event in the Unity editor, we not only call
     * this function, but we also pass it the slider game object. From that,
     * the function will request the slider's value, and use that to control
     * the transparency value of our cube's material.*/
    public void ChangeTransparencyOnSlide(Slider transparencySlider)
    {
        /* Here we keep the existing R, G, and B values of our cubeColor variable,
         * but give it a new A (transparency) value based on the slider's position.
         * We then assign the resulting color to our cube's material.*/
        cubeColor = new Color(cubeColor.r, cubeColor.g, cubeColor.b, 1 - transparencySlider.value);
        GetComponent<Renderer>().material.color = cubeColor;
    }
}
