//This creates a class called MainGameSaveData and it can be accessed by other classes.
//There is no monobehaviour on this class because I only need to access it and it doesn't need to attach as a component onto an object.
//This class will serve as a template when serialising and deserialising the MainGame Json file that I will create.
public class MainGameSaveData
{
    //All of the attributes displayed here are public and will either be used to store their contents into the same attributes of a Json file.
    //Or they will be used to store the Json files attribute contents into these attributes.
    //These are all of the attributes required to save and load player data for the main game.
    public int[] inventory;
    public int[] inventoryStat;
    public int money;
    public int[] locker;
    public int[] lockerStat;
    public int levelsCompleted;
    public bool[] chestOpened;
}

//This creates a class called OptionSaveData and it can be accessed by other classes.
//There is no monobehaviour on this class because I only need to access it and it doesn't need to attach as a component onto an object.
//This class will serve as a template when serialising and deserialising the Options Json file that I will create.
public class OptionSaveData
{
    //All of the attributes displayed here are public and will either be used to store their contents into the same attributes of a Json file.
    //Or they will be used to store the Json files attribute contents into these attributes.
    public float volume;
    public bool fullScr;
    public bool keyboardOnly;
    public int language;
    public int colourBlindMode;
}

//This creates a class called StatisticSaveData and it can be accessed by other classes.
//There is no monobehaviour on this class because I only need to access it and it doesn't need to attach as a component onto an object.
//This class will serve as a template when serialising and deserialising the Statistics Json file that I will create.
public class StatisticSaveData
{
    //All of the attributes displayed here are public and will either be used to store their contents into the same attributes of a Json file.
    //Or they will be used to store the Json files attribute contents into these attributes.
    public int enemiesDefeated;
    public int challengesCompleted;
}