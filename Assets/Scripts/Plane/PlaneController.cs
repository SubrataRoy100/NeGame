using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private Vector2 _input;
    [SerializeField] private PlayerDataSO playerDataSO;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (PlayerPrefs.HasKey("X"))
        {

            Vector3 pos = new Vector3();
            pos.x = PlayerPrefs.GetFloat("X");
            pos.y = PlayerPrefs.GetFloat("Y");
            transform.position = pos;
            Debug.Log(transform.position);
        }

        
    }

    private void Update()
    {
        float xInput = 0;
        float yInput = 0;
        if (Input.GetKey(KeyCode.W))
        {
            yInput = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yInput = -1;

        }
        if (Input.GetKey(KeyCode.A))
        {
            xInput = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xInput = +1;
        }

        _input = new Vector2(xInput, yInput).normalized;
        playerDataSO.targetVelocity = _input * playerDataSO.planeSpeed * Time.deltaTime;
        if (_input != Vector2.zero)
        {
            float angle = Mathf.Atan2(_input.y, _input.x) * Mathf.Rad2Deg;
            Quaternion targetZRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetZRotation, playerDataSO.turnSpeed * Time.deltaTime);
        }
        playerDataSO.CurrentPosition = transform.position;

    }

    private void FixedUpdate()
    {
        playerDataSO.currentVelocity = Vector2.SmoothDamp(playerDataSO.currentVelocity, playerDataSO.targetVelocity, ref playerDataSO.refVelocity, playerDataSO.smoothTime);
        rb2d.velocity = playerDataSO.currentVelocity;
    }


    // TESTING 
    public PlayerDataSO GetPlayerDataSO()
    {
        return playerDataSO;
    }

    public Vector3 GetTransform()
    {
        return transform.position;
    }
    public void SetTranform(Vector3 _pos)
    {
        transform.position = _pos;
        Debug.Log("+++++++Position Change++++++");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("X", transform.position.x);
        PlayerPrefs.SetFloat("Y", transform.position.y);
        Debug.Log(transform.position);


    }



}
