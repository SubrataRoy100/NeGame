using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Vector3 startingPos;
    private Vector3 roamingPos;

    private void Start(){
        startingPos = transform.position;
        roamingPos = GetRandomPos();
    }
    private void Update(){
        transform.Rotate(Vector3.forward,Space.World);
        Vector3 dir =(roamingPos - transform.position).normalized;
        float r_speed = 0.2f;
        transform.position += dir*r_speed*Time.deltaTime;
        if(Vector3.Distance(transform.position,roamingPos) < 1f){
            r_speed = Random.Range(0.1f,2f);
            roamingPos = GetRandomPos();
        }
        //MonoSave.I.currentSaveData.asteroidPosition = transform.position;
    }

    private Vector3  GetRandomPos(){
        Vector3 r = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
        return startingPos + r* Random.Range(2f,2f);
    }
}
