using System;
using System.Collections.Generic;

[Serializable]
public class jsonDataLevels
{
    public string msg;
    public LevelsStructure value;
}

[Serializable]
public class LevelsStructure
{
    public int patient;
    public List<string> levels;
    public List<string> actions1;
    public List<string> actions2;
    public List<string> actions3;
}

//[Serializable]
//public class StructureData
//{
  //  public List<string> level1;
   // public List<string> level2;
   // public List<string> level3;
//}

/**[Serializable]
public class ActionData
{
   public int id;V
   public string action;
    public int ordering;
    public int levelid;
    public string level;
    public int sequenceid;
    public string sequence;
    public int repository;
    public int time; 
    public int word;
}
**/

