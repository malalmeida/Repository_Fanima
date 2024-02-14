using System;
using System.Collections.Generic;

[Serializable]
public class jsonDataRestore
{
    public string msg;
    public RestoreData value;
}

[Serializable]
public class RestoreData
{
    public int gameexecutionid;
    public List<string> levels;
    public int levelid;
    //public int sequenceid;
    //public string sequence;
    public List<string> actions1;
    public List<string> actions2;
    public List<string> actions3;
    public List<string> extra1;
    public List<string> extra2;
    public List<string> extra3;
}
