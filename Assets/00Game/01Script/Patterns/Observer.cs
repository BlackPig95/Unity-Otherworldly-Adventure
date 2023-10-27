using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : Singleton<Observer>
{
    Dictionary<string, List<Action<object>>> listActions = new Dictionary<string, List<Action<object>>>();
    #region KEYS
    public static string FinishLevel = "FinishLevel";
    public static string SavePoint = "Saved";
    public static string InitLevel = "InitLevel";
    public static string ReloadLevel = "ReloadLevel";
    #endregion

    public void AddListener(string key, Action<object> callBack)
    {
        List<Action<object>> temp = new List<Action<object>>();
        if(listActions.ContainsKey(key))
        {
            temp = listActions[key];
        }
        else
        {
            listActions.Add(key, temp);
        }
        temp.Add(callBack);
    }
    public void Notify(string key, object data = null)
    {
        if (!listActions.ContainsKey(key))
            return;
        foreach(Action<object> action in listActions[key])
        {
            action?.Invoke(data);
        }
    }
    public void RemoveListener(string key, Action<object> callBack)
    {
        if (!listActions.ContainsKey(key))
            return;
        listActions[key].Remove(callBack);
    }
}
