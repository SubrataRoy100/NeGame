using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class SaveData
{
    //private static SaveData _i;
    //public static SaveData I
    //{
    //    get
    //    {
    //        if (_i == null)
    //        {
    //            _i = new SaveData();
    //        }
    //        return _i;
    //
    //    }
    //    set
    //    {
    //        _i = value;
    //    }
    //}
        
}
[Serializable]
public class PlayerData
{
    public int currentHealth;
    public Vector3 currentPositon;
    public PlayerData(int _Health, Vector3 _currentPos)
    {
        currentHealth = _Health;
        currentPositon = _currentPos;
    }
}



