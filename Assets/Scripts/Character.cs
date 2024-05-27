using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHP = 3;
    public GameObject hpGauge;
    float hp;
    float hpMaxWidth;
    void Start()
    {
        hp = maxHP;

        if (hpGauge != null)
        {
            hpMaxWidth = hpGauge.GetComponent<RectTransform>().sizeDelta.x;
        }

    }

    public void Initialize()
    {
        hp = maxHP;

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Hit(float damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
        }

        if (hpGauge != null ) {
            Debug.Log("hp" + hp);
            Debug.Log("maxHp" + maxHP);
            Debug.Log("width" + hpMaxWidth);
            hpGauge.GetComponent<RectTransform>().sizeDelta = new Vector2(hp / maxHP * hpMaxWidth, hpGauge.GetComponent<RectTransform>().sizeDelta.y);
        }

        return hp > 0;
    }
}
