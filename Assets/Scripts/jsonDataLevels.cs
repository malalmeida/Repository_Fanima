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

