using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerDataSO : ScriptableObject
{


    public int CurrentHealtMax;
    public Vector3 CurrentPosition;
    public float planeSpeed;
    public float turnSpeed;
    public float smoothTime;
    [HideInInspector] public Vector2 refVelocity;
    [HideInInspector] public Vector2 currentVelocity;
    [HideInInspector] public Vector2 targetVelocity;

}
