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
}
