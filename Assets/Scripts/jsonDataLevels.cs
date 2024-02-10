using System;
using System.Collections.Generic;

[Serializable]
public class jsonDataLevels
{
    public string msg;
    //public List<string> value;
    public LevelsStructure value;
}

[Serializable]
public class LevelsStructure
{
    public List<string> levels;
    public StructureData structure;
}

[Serializable]
public class StructureData
{
    public string level;
    public List<ActionData> structure;
}

[Serializable]
public class ActionData
{
    public int id;
    public int levelid;
    public string level;
    public int sequenceid;
    public string sequence;
    public int time; 
    public int repository;
    public int word;
}

