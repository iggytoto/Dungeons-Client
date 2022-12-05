using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserContext
{
    public long userId;
    public string userName;
    public string value;
    public DateTime validTo;
}