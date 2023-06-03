using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public int maxVal;
    public int currVal;

    public Stat(int curr, int max)
    {
        maxVal = max;
        currVal = curr;

    }
    internal void Subtract(int amount )
    {
        currVal -= amount;
    }
    internal void Add(int amount)
    {
        currVal -= amount;
        if(currVal > maxVal) { currVal = maxVal; }
    }
    internal void SetToMax()
    {
        currVal = maxVal;
    }

}

public class Character : MonoBehaviour
{
    public Stat hp;
    public Stat stamina;
    public bool isDead;
    public bool isExhausted;
    [SerializeField] StatusBar hpBar;
    [SerializeField] StatusBar staminaBar;
    DisableControls disableControls;
    PlayerRespawn playerRespawn;


    private void Awake()
    {
        disableControls = GetComponent<DisableControls>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }


    private void Start()
    {
        UpdateHpBar();
        UpdateStaminaBar();

    }

    private void UpdateHpBar()
    {
        hpBar.Set(hp.currVal, hp.maxVal);
    }
    private void UpdateStaminaBar()
    {
        staminaBar.Set(stamina.currVal, stamina.maxVal);
    }


    public void TakeDamage(int amount)
    {
        if (isDead == true) { return; }
        GameManager.instance.messageSystem.PostMessage(transform.position,amount.ToString());
        hp.Subtract(amount);
        

        if (hp.currVal <= 0 && isDead!=true)
        {
            Dead();
        }
        UpdateHpBar();
    }

    private void Dead()
    {
        isDead = true;
        disableControls.DisableControl();
        playerRespawn.StartRespawn();
    }

    public void Heal(int amount)
    {
        hp.Add(amount);
        UpdateHpBar();
    }
    public void FullHeal()
    {
        isDead = false;
        hp.SetToMax();
        UpdateHpBar();
    }
    public void GetTired(int amount)
    {
        stamina.Subtract(amount);
        if(stamina.currVal <0)
        {
            isExhausted = true;
        }
        UpdateStaminaBar();
    }
    public void Rest(int amount)
    {
        stamina.Add(amount);
        UpdateStaminaBar();
    }
    public void FullRest()
    {
        stamina.SetToMax();
        UpdateStaminaBar();
    }

    private void Update()
    {
        
    }

}
