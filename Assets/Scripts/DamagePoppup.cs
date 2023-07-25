using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePoppup : MonoBehaviour
{


    public static DamagePoppup CreatePopPup(Vector3 spawnPos, int damageValue, Action _OnPopPupComplate = null)
    {
        Transform popPupTransform = Instantiate(GameHelper.I.Pf_PopPup, spawnPos, Quaternion.identity);
        DamagePoppup damagePoppup = popPupTransform.GetComponent<DamagePoppup>();
        damagePoppup.SetDamageText(damageValue, _OnPopPupComplate);
        return damagePoppup;
    }
    private TextMeshPro damagePopPupText;
    private Vector3 textMoveDir;
    private float timer;
    private float timerMax;
    private Color color;
    public Action OnPopPupComplateAction;
    int sortingOrder;
    private void Awake()
    {
        damagePopPupText = GetComponent<TextMeshPro>();
        color = damagePopPupText.color;
    }

    public void SetDamageText(int amount, Action _onComplateAction)
    {
        sortingOrder++;
        damagePopPupText.sortingOrder += sortingOrder;
        damagePopPupText.text = amount.ToString();
        timer = 1f;
        timerMax = 1;
        textMoveDir = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, .5f)) * 4f;
        OnPopPupComplateAction = _onComplateAction;

    }

    private void Update()
    {

        float translateSpeed = 5f;
        transform.position += textMoveDir * Time.deltaTime;
        textMoveDir -= textMoveDir * translateSpeed * Time.deltaTime;
        IncreaseScaleAndDecreaseScale();



    }

    private void IncreaseScaleAndDecreaseScale()
    {

        timer -= Time.deltaTime;
        if (timer > timerMax * 0.5f)
        {
            float increaseScale = 1f;
            transform.localScale += Vector3.one * increaseScale * Time.deltaTime;
        }
        else
        {
            float decreaseScale = 1f;
            transform.localScale -= Vector3.one * decreaseScale * Time.deltaTime;

        }
        if (timer < 0)
        {
            color.a -= 4 * Time.deltaTime;
            damagePopPupText.color = color;
            if (color.a < 0)
            {
                Destroy(gameObject);
                OnPopPupComplateAction?.Invoke();
            }

        }
    }
}
