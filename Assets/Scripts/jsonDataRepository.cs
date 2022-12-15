using System;
using System.Collections.Generic;

[Serializable]
public class jsonDataRepository
{
    public string status;
    public List<dataSource> content;
}

[Serializable]
public class dataSource
{
    public int id;
    public string name;
}
