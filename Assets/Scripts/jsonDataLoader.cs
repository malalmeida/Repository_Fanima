using System;
using System.Collections.Generic;


[Serializable]
public class jsonDataLoader
{
    public string status;
    public List<actionClass> content;
}

[Serializable]
public class actionClass
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
