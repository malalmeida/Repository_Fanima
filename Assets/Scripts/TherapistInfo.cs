using System;
using System.Collections.Generic;

[Serializable]
public class TherapistInfo
{
    public string status;
    public List<therapistdata> content;
}

[Serializable]
public class therapistdata
{
    public int id;
    public string name;
}