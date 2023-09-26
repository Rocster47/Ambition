//These are the namespaces (a collection of classes) that this script can access for more functionality.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

//This creates a class called OptionsController and it can be accessed by other classes.
//It inherits MonoBehaviour as the class will be able to attach as a script component to the MenuManager object.
public class OptionsController : MonoBehaviour
{
    //These lines create attributes that are private and all methods can access them. However, SerializeField enables other classes to access these through encapsulation.
    //The green terms such as "GameObject" are custom data types that are named after a class from the namespaces and tells the computer the format of 
    //what will be stored in these attributes.
    //These will displayed on the inspector tab when the MenuManager with a component containing this script is selected.
    //In the inspector, the contents of these attributes can be viewed and changed if they have SerializeField.
    [SerializeField] private Camera cam;
    [SerializeField] private AudioMixer audMix;
    [SerializeField] private Slider volSlider;
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private TMP_Dropdown colourBlindMode;

    //This creates a method called Volume that can be used and accessed by other classes.
    //The method will set the master volume value of the audio mixer according to the value of the volume slider.
    public void Volume()
    {
        audMix.SetFloat("Volume", volSlider.value);
    }
    
    //This creates a method called FullScreen that can be used and accessed by other classes.
    //The method will either enable or disable full screen of the program.
    public void FullScreen()
    {
        if(fullScreen.isOn == true) //If the full screen tick box is a tick...
        {
            Screen.fullScreen = true; //The program will be played in full screen.
        }
        else //If the full screen tick box is empty...
        {
            Screen.fullScreen = false; //The program won't be played in full screen.
        }
    }

    //This creates a method called ColourBlindMode that can be used and accessed by other classes.
    //The method will switch the camera's colour blind mode corresponding to what is selected in the dropdown menu.
    public void ColourBlindMode()
    {
        switch(colourBlindMode.value) //If the dropdown value selected is...
        {
            case 0: //0:
                cam.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Normal; //Change the camera's colour blind mode component to normal.
                break;
            case 1: //1:
                cam.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Protanopia; //Change the camera's colour blind mode component to Protanopia.
                break;
            case 2: //2:
                cam.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Protanomaly; //Change the camera's colour blind mode component to Protanomaly.
                break;
            case 3: //3:
                cam.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Deuteranopia; //Change the camera's colour blind mode component to Deuteranopia.
                break;
            case 4: //4:
                cam.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Deuteranomaly; //Change the camera's colour blind mode component to Deuteranomaly.
                break;
            case 5: //5:
                cam.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Tritanopia; //Change the camera's colour blind mode component to Tritanopia.
                break;
            case 6: //6:
                cam.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Tritanomaly; //Change the camera's colour blind mode component to Tritanomaly.
                break;
            case 7: //7:
                cam.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Achromatopsia; //Change the camera's colour blind mode component to Achromatopsia.
                break;
            case 8: //8:
                cam.GetComponent<ColorBlindFilter>().mode = ColorBlindMode.Achromatomaly; //Change the camera's colour blind mode component to Achromatomaly.
                break;
        }
    }
}