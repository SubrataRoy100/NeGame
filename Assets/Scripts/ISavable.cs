using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavable
{
   
  public void Save(SaveData save );
  public void Load(ref SaveData saveData);
}
