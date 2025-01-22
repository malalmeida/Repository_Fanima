using System;
using System.Collections.Generic;

[Serializable]
public class jsonDataCharacterSelection
{
    public List<characterClass> content;
}

[Serializable]
public class characterClass
{
    public int characterID;
}