using System;
using System.Collections.Generic;

[Serializable]
public class jsonDataRequestAutoHelp
{
    public string msg;
    public RequestAutoHelp value;

}

[Serializable]
public class RequestAutoHelp
{
    public string therapist;
    public string patient;
    public string context;
    public int data;
}