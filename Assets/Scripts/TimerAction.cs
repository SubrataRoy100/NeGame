using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerAction
{
    #region Timer Action

    private float timer;
    private Action OnAction;
    private bool isTimerOver= false;
    private  GameObject timer_Go;

    
    public static TimerAction Create(float timer,Action OnActin){
        GameObject timer_Go = new GameObject("TimerAction",typeof(DummyTimerActionMono));
        TimerAction timerAction = new TimerAction(OnActin,timer,timer_Go);
        timer_Go.GetComponent<DummyTimerActionMono>().OnAction =timerAction.Update;
        return timerAction;
    }

    public class DummyTimerActionMono : MonoBehaviour
    {

        public Action OnAction;
        private void Update(){
            OnAction?.Invoke();
        }
    }
    public TimerAction(Action action,float timer,GameObject timerGo)
    {
        this.timer = timer;
        this.timer_Go = timerGo;
        OnAction = action;
    }

    public void Update()
    {
        if (!isTimerOver)
        {

            timer -= Time.deltaTime;
            if (timer < 0)
            {
                OnAction?.Invoke();
                isTimerOver = true;
                GameObject.Destroy(timer_Go);

            }
        }

    }


    #endregion

}
