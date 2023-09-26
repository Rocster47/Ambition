//These are the namespaces (a collection of classes) that this script can access for more functionality.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

//This creates a class called MainMenuButtonControls and it can be accessed by other classes.
//It inherits MonoBehaviour as the class will be able to attach as a script component to the MenuManager object.
public class MainMenuButtonControls : MonoBehaviour
{
    //These lines create attributes that are private and all methods can access them. However, SerializeField enables other classes to access these through encapsulation.
    //The green terms such as "GameObject" are custom data types that are named after a class from the namespaces and tells the computer the format of 
    //what will be stored in these attributes.
    //These will displayed on the inspector tab when the MenuManager with a component containing this script is selected.
    //In the inspector, the contents of these attributes can be viewed and changed if they have SerializeField.
    [SerializeField] private GameObject loginSelectPanel;
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject menuSelectPanel;
    [SerializeField] private GameObject playGamePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject statisticsPanel;

    [SerializeField] private TMP_InputField regEmail;
    [SerializeField] private TMP_InputField regUsername;
    [SerializeField] private TMP_InputField regPassword;
    [SerializeField] private TMP_Text regMessage;

    [SerializeField] private TMP_InputField logUsername;
    [SerializeField] private TMP_InputField logPassword;
    [SerializeField] private TMP_Text logMessage;

    [SerializeField] private Slider volSlider;
    [SerializeField] private Toggle keyboardOnly;
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private TMP_Dropdown language;
    [SerializeField] private TMP_Dropdown colourBlindMode;

    [SerializeField] private float volSliderTEMP;
    [SerializeField] private bool fullScreenTEMP;
    [SerializeField] private bool keyboardOnlyTEMP;
    [SerializeField] private int languageTEMP;
    [SerializeField] private int colourBlindModeTEMP;
    public bool optionSaved;

    [SerializeField] private AudioSource titleMusic;
    [SerializeField] private AudioClip buttonPress;

    //This creates a method called RegisterMenu that can be used and accessed by other classes. The method will allow the user to go to the register panel.
    //This is done by deactivating the login select panel and activating the register panel.
    public void RegisterMenu()
    {
        //Plays the button pressing sound effect once alongside the title music playing in the audio source.
        //6.5f means scale the sound effect volume 6.5 because it was very quiet with the music too.
        //The f in 6.5f indicates it is a float.
        titleMusic.PlayOneShot(buttonPress,6.5f); 

        loginSelectPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void RegisterConfirm()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        //This initialises a local variable called "request" with the RegisterPlayFabUserRequest class data type.
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Email = regEmail.text; //This changes the Email attribute belonging to request with the current text in the input field of regEmail.
        request.Username = regUsername.text; //This changes the Username attribute belonging to request with the current text in the input field of regUsername.
        request.Password = regPassword.text; //This changes the Password attribute belonging to request with the current text in the input field of regPassword.

        if (regUsername.text.Length > 15) //If the username entered in the input field box is greater than 15 characters:
        {
            //The text at the top of the register menu will change to say that the registration has failed due to the username being greater than 15 characters.
            regMessage.text = "Register - Failed to create your account: Your username is more than 15 characters long!";
        }
        else //If it is 15 characters or lower:
        {
            //The method RegisterPlayFabUser() from the PlayFabClientAPI class is carried out with the Email, Username and Password parameters from request.
            PlayFabClientAPI.RegisterPlayFabUser(request, result => //If there is success with the registration:
            { 
                //The text at the top of the register menu will change to say that the account has been created.
                regMessage.text = "Resgister - Your account has been created.";
            }, error => //If there is an error and the user can not register:
            { 
                //The text at the top of the register menu will change to say that the registration has failed and why. PlayFab will automatically generate the error message.
                regMessage.text = "Register - Failed to create your account: " + error.ErrorMessage;
            });
        }
    }

    //This creates a method called LoginMenu that can be used and accessed by other classes. The method will allow the user to go to the log in panel.
    //This is done by deactivating the login select panel and activating the log in panel.
    public void LoginMenu()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        loginSelectPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    //This creates a method called LoginConfirm.
    public void LoginConfirm()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        //This initialises a local variable called "loginRequest" with the RegisterPlayFabUserRequest class data type.
        LoginWithPlayFabRequest loginRequest = new LoginWithPlayFabRequest();
        loginRequest.Username = logUsername.text; //This changes the Username attribute belonging to loginRequest with the current text in the input field of logUsername.
        loginRequest.Password = logPassword.text; //This changes the Password attribute belonging to loginRequest with the current text in the input field of logPassword.

        //The method LoginWithPlayFab() from the PlayFabClientAPI class is carried out with the Username and Password parameters from loginRequest.
        PlayFabClientAPI.LoginWithPlayFab(loginRequest, result => //If there is success with the log in:
        {
            GetComponent<SaveLoad>().StartupLoad(); //This will run the StartupLoad method in the SaveLoad script.
            //The text at the top of the log in menu will change to greet the player using their username in lower case as PlayFab stores all usernames in lowercase in the game.
            //It also tells the user that it is connecting to the game and online.
            logMessage.text = "Log In - Welcome " + logUsername.text.ToLower() + "! Connecting...";
            loginPanel.SetActive(false); //This moves on to the menu options panel by deactivating the log in panel
            menuSelectPanel.SetActive(true); //and activating the menu options panel.
        }, error => //If there is an error and the user can not log in:
        {
            //The text at the top of the log in menu will change to say that the log in has failed and why. PlayFab will automatically generate the error message.
            logMessage.text = "Log In - Failed to login: " + error.ErrorMessage;
        });
    }

    //This creates a method called BackRegToLoginSelect that can be used and accessed by other classes. The method will allow the user to go to the log in select panel
    //from the register panel. This is done by deactivating the register panel and activating the log in panel.
    public void BackRegToLoginSelect()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        registerPanel.SetActive(false);
        loginSelectPanel.SetActive(true);
    }

    //This creates a method called BackLogToLoginSelect that can be used and accessed by other classes. The method will allow the user to go to the log in select panel
    //from the log in panel. This is done by deactivating the log in panel and activating the log in select panel.
    public void BackLogToLoginSelect()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        loginPanel.SetActive(false);
        loginSelectPanel.SetActive(true);
    }

    //This creates a method called PlayMenu that can be used and accessed by other classes. The method will allow the user to go to the play game panel.
    //This is done by deactivating the main options panel and activating the play game panel.
    public void PlayMenu()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        menuSelectPanel.SetActive(false);
        playGamePanel.SetActive(true);
    }

    //This creates a method called Option that can be used and accessed by other classes. The method will allow the user to go to the options panel.
    //This is done by deactivating the main options panel and activating the options panel.
    public void OptionMenu()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        menuSelectPanel.SetActive(false);
        optionsPanel.SetActive(true);
        
        //To track whether the user has saved their options, the attribute optionSaved will be used. The boolean attribute will start as false when going to the options menu.
        optionSaved = false;
        
        //These attributes will temporarily hold the values of each settings that they are currently on.
        //These will be used to revert back to the original settings if they were never saved.
        volSliderTEMP = volSlider.value;
        fullScreenTEMP = fullScreen.isOn;
        keyboardOnlyTEMP = keyboardOnly.isOn;
        languageTEMP = language.value;
        colourBlindModeTEMP = colourBlindMode.value;
    }

    //This creates a method called StatMenu that can be used and accessed by other classes. The method will allow the user to go to the statistics panel.
    //This is done by deactivating the ain options panel and activating the statistics panel.
    public void StatMenu()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        menuSelectPanel.SetActive(false);
        statisticsPanel.SetActive(true);
    }

    //This creates a method called BackPlayToMenuSelect that can be used and accessed by other classes. The method will allow the user to go to the main options panel
    //from the play game panel. This is done by deactivating the play game panel and activating the main options panel.
    public void BackPlayToMenuSelect()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        playGamePanel.SetActive(false);
        menuSelectPanel.SetActive(true);
    }

    //This creates a method called BackOptionsToMenuSelect that can be used and accessed by other classes. The method will allow the user to go to the main options panel
    //from the options panel. This is done by deactivating the options panel and activating the main options panel.
    public void BackOptionsToMenuSelect()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        optionsPanel.SetActive(false);
        menuSelectPanel.SetActive(true);
        
        if(optionSaved == false) //If the options haven't been saved when returning back from the options menu...
        {
            //Reset all values of each setting.
            volSlider.value = volSliderTEMP;
            fullScreen.isOn = fullScreenTEMP;
            keyboardOnly.isOn = keyboardOnlyTEMP;
            language.value = languageTEMP;
            colourBlindMode.value = colourBlindModeTEMP;
        }
    }

    //This creates a method called BackStatToMenuSelect that can be used and accessed by other classes. The method will allow the user to go to the main options panel
    //from the statistics panel. This is done by deactivating the statistics panel and activating the main options panel.
    public void BackStatToMenuSelect()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        statisticsPanel.SetActive(false);
        menuSelectPanel.SetActive(true);
    }

    //This creates a method called Quit that can be used and accessed by other classes. The method will allow the user to quit the game and close the application.
    //This is done by the method Quit() from the Application class.
    public void Quit()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music. 

        Application.Quit();
    }
}