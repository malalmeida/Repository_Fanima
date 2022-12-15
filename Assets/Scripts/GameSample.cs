using System;
using System.Collections.Generic;


[Serializable]
public class GameSample
{
    public SampleData data;
    public int gameactionid;

    public int gameexecutionid;

    public GameSample(SampleData sampleData, int gaID, int geID)
    {
        data = sampleData;
        gameactionid = gaID;
        gameexecutionid = geID;
    }
}

[Serializable]
public class SampleData
{
    public int id;

    public SampleData(int actionID)
    {
        id = actionID;
    }
}