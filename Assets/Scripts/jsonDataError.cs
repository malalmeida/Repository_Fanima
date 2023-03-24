using System;
using System.Collections.Generic;

[Serializable]
public class jsonDataError
{
    public List<errorClass> content;
}

[Serializable]
public class errorClass
{
    public int sample;
    public int action;
    public string phoneme;
    public int word;
    public int label;
}

