using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class User : MonoBehaviour
{
    public static string userName;
    public static string userCountry;
    public static DateTime date;

    private void Start()
    {
        Simulator.OnNewPlayer += GetUserInfo;
    }

    void GetUserInfo(string name, string country, DateTime time)
    {
        userName = name;
        userCountry = country;
        date = time;
    }

}
