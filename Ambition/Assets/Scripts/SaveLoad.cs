//These are the namespaces (a collection of classes) that this script can access for more functionality.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

//This creates a class called SaveLoad and it can be accessed by other classes.
//It inherits MonoBehaviour as the class will be able to attach as a script component to the MenuManager object.
public class SaveLoad : MonoBehaviour
{
    //These lines create attributes that are private and all methods can access them. However, SerializeField enables other classes to access these through encapsulation.
    //The green terms such as "GameObject" are custom data types that are named after a class from the namespaces and tells the computer the format of 
    //what will be stored in these attributes.
    //These will displayed on the inspector tab when the MenuManager with a component containing this script is selected.
    //In the inspector, the contents of these attributes can be viewed and changed if they have SerializeField.
    
    private MainGameSaveData mainGameSaveData;
    private OptionSaveData optionSaveData; //This is an attribute that stores the OptionSaveData class that will be used as a template for Json serialisation and deserialisation.
    private StatisticSaveData statisticSaveData;    

    [SerializeField] private Slider volSlider;
    [SerializeField] private Toggle keyboardOnly;
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private TMP_Dropdown language;
    [SerializeField] private TMP_Dropdown colourBlindMode;

    [SerializeField] private AudioSource titleMusic;
    [SerializeField] private AudioClip buttonPress;

    [SerializeField] private TMP_Text progress;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TMP_Text enemiesDefeated;
    [SerializeField] private TMP_Text challengesCompleted;

    [SerializeField] private Button continueGame;

    //This creates a method called StartupLoad that can be used and accessed by other classes. The method will save or load data as soon as the user logs in.
    public void StartupLoad()
    {
        mainGameSaveData = new MainGameSaveData();
        optionSaveData = new OptionSaveData(); //This initialises and declares a local variable called "optionSaveData" with the OptionSaveData class data type.
        statisticSaveData = new StatisticSaveData();

        //Using PlayFab API calls, the program will run the method GetUserData to request for the data of that user who is logged in.
        //The data received will be stored in result.
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result => //If the request was successful and the data was received...
        {
            Debug.Log("Loaded Startup."); //Print out "Loaded Startup." in the Debug Log.
            if(!result.Data.ContainsKey("Options")) //If there is no key called "Options" in the user save data...
            {
                //This must be a new user so we need to create and save their default settings. These can then be changed, saved and loaded in the future.

                mainGameSaveData.inventory = new int[11] {0,0,0,0,0,0,0,0,0,0,0};
                mainGameSaveData.inventoryStat = new int[11] {0,0,0,0,0,0,0,0,0,0,0};
                mainGameSaveData.money = 0;
                mainGameSaveData.locker = new int[0];
                mainGameSaveData.lockerStat = new int[0];
                mainGameSaveData.levelsCompleted = 0;
                mainGameSaveData.chestOpened = new bool[0];

                //The value of the attributes belonging to OptionSaveData will store the following:
                optionSaveData.volume = 0;
                optionSaveData.fullScr = true;
                optionSaveData.keyboardOnly = false;
                optionSaveData.language = 0;
                optionSaveData.colourBlindMode = 0;

                statisticSaveData.enemiesDefeated = 0;
                statisticSaveData.challengesCompleted = 0;

                UpdateUserDataRequest request = new UpdateUserDataRequest() //This initialises and declares a local variable called "request" with the UpdateUserDataRequest class data type.
                {
                    Data = new Dictionary<string, string> //The attribute Data belonging to UpdateUserDataRequest will store a new Dictionary data structure.
                    {
                        {"MainGame", JsonConvert.SerializeObject(mainGameSaveData)}, //This dictionary will contain the string "Options" and then the serialised version of OptionSaveData.
                        {"Options", JsonConvert.SerializeObject(optionSaveData)},
                        {"Statistics", JsonConvert.SerializeObject(statisticSaveData)}
                    }
                };

                //The PlayFab API will send a call to update the user data. In this case, the request will want to store the serialised version of OptionSaveData as a Json file in the key "Options".
                PlayFabClientAPI.UpdateUserData(request, result => //If the update/save was successful...
                {
                    Debug.Log("Saved New User Data."); //Print out "Saved Options." in the Debug Log.
                }, error => //If there was an error...
                {
                    Debug.Log("Error Saving New User Data."); //Print out "Error Saving Options." in the Debug Log.
                });
            }
            else //If the save data in PlayFab has the key "Options"...
            {
                //This is an existing user so we need to load up the settings they have changed in the past.
                
                mainGameSaveData = JsonConvert.DeserializeObject<MainGameSaveData>(result.Data["MainGame"].Value);
                if(mainGameSaveData.levelsCompleted == 0)
                {
                    continueGame.interactable = false;
                }
                else
                {
                    continueGame.interactable = true;
                }

                //The Json file contents in the "Options" key will deserialise.
                //The deserialised Json file and the values of its attributes will then be stored in the OptionSaveData class and its attributes.
                optionSaveData = JsonConvert.DeserializeObject<OptionSaveData>(result.Data["Options"].Value); 
                volSlider.value = optionSaveData.volume; //The volume slider value will take the value of the volume attribute in optionSaveData.
                fullScreen.isOn = optionSaveData.fullScr; //The isOn of the fullScreen toggle will take the value of the fullScr boolean attribute in optionSaveData.
                keyboardOnly.isOn = optionSaveData.keyboardOnly; //The isOn of the keyboardOnly toggle will take the value of the keyboardOnly boolean attribute in optionSaveData.
                language.value = optionSaveData.language; //The value of the language dropdown will take the value of the language attribute in optionSaveData.
                colourBlindMode.value = optionSaveData.colourBlindMode; //The value of the colourBlindMode dropdown will take the value of the colourBlindMode attribute in optionSaveData.

                statisticSaveData = JsonConvert.DeserializeObject<StatisticSaveData>(result.Data["Statistics"].Value);
                progress.text = "Progress: " + Mathf.RoundToInt(mainGameSaveData.levelsCompleted/9f*100f).ToString() + "%";
                progressSlider.value = mainGameSaveData.levelsCompleted;
                enemiesDefeated.text = "Enemies Defeated: " + statisticSaveData.enemiesDefeated.ToString();
                challengesCompleted.text = "Challenges Completed: " + statisticSaveData.challengesCompleted.ToString();

                Debug.Log("Loaded Existing User Data."); //Print out "Loaded Options." in the Debug Log.
            }
        }, error => //If there was an error receiving the save data...
        {
            Debug.Log("Error Loading Startup."); //Print out "Error Loading Startup." in the Debug Log.
        });
    }

    //This creates a method called SaveOptions that can be used and accessed by other classes. The method will save the settings when the save button is pressed in the options menu.
    public void SaveOptions()
    {
        titleMusic.PlayOneShot(buttonPress,6.5f); //Plays the button pressing sound effect once alongside the title music.

        optionSaveData = new OptionSaveData(); //This initialises and declares a local variable called "optionSaveData" with the OptionSaveData class data type.
        
        optionSaveData.volume = volSlider.value; //The attribute volume belonging to the OptionSaveData class takes the value of the volume slider value.
        optionSaveData.fullScr = fullScreen.isOn; //The boolean attribute fullScr belonging to the OptionSaveData class takes the value of the full screen toggles isOn. 
        optionSaveData.keyboardOnly = keyboardOnly.isOn; //The boolean attribute keyboardOnly belonging to the OptionSaveData class takes the value of the keyboard only toggles isOn.
        optionSaveData.language = language.value; //The attribute language belonging to the OptionSaveData class takes the value of the language dropdown.
        optionSaveData.colourBlindMode = colourBlindMode.value; //The attribute colourBlindMode belonging to the OptionSaveData class takes the value of the colour blind mode dropdown.

        UpdateUserDataRequest request = new UpdateUserDataRequest() //This initialises and declares a local variable called "request" with the UpdateUserDataRequest class data type.
        {
            Data = new Dictionary<string, string> //The attribute Data belonging to UpdateUserDataRequest will store a new Dictionary data structure.
            {
                {"Options", JsonConvert.SerializeObject(optionSaveData)} //This dictionary will contain the string "Options" and then the serialised version of OptionSaveData.
            }
        };

        //The PlayFab API will send a call to update the user data. In this case, the request will want to store the serialised version of OptionSaveData as a Json file in the key "Options".
        PlayFabClientAPI.UpdateUserData(request, result => //If the update/save was successful...
        {
            Debug.Log("Saved Options."); //Print out "Saved Options." in the Debug Log.
        }, error => //If there was an error...
        {
            Debug.Log("Error Saving Options."); //Print out "Error Saving Options." in the Debug Log.
        });
        
        GetComponent<MainMenuButtonControls>().optionSaved = true; //The attribute optionSaved of the class MainMenuButtonControls found in the MainMenuButtonControls script component of MenuManager is now true.
    }
}