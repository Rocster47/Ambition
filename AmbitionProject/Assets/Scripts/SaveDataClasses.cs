public class MainGameSaveData
{
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

public class StatisticSaveData
{
    public int enemiesDefeated;
    public int challengesCompleted;
}