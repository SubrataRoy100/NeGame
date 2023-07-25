using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuToggle : MonoBehaviour
{
    private bool toggle;
    [SerializeField] private Transform back_btn;
    [SerializeField] private Transform resume_btn;

    private void Start(){
            back_btn.gameObject.SetActive(false);
            resume_btn.gameObject.SetActive(false);
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            toggle = true;
        }
        if(toggle){
            back_btn.gameObject.SetActive(true);
            resume_btn.gameObject.SetActive(true);
            Time.timeScale = 0;
        }else{
            back_btn.gameObject.SetActive(false);
            resume_btn.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        

    }
public void Resume(){
    toggle = false;
}


    
}
