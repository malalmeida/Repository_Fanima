using System;
using System.Collections.Generic;

[Serializable]
public class jsonDataLevels
{
    public  int patientid;
    public List<levels> levels;
}

[Serializable]
public class levels
{
    public int levelid;
}