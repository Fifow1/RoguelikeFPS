using System;
using UnityEngine;
using UnityEngine.AI;



public abstract class Monster : MonoBehaviour
{
    public bool hpUiActive;
    public Action<float, float> eventHp;
    public int compensation;
    protected string name { get; set; }
    protected int maxHp { get; set; }
    protected int currentHp { get; set; }
    protected bool isDie { get; set; }
    protected float distance { get; set; }
   // public abstract State currentState { get; set; }
    protected void SetValue(int maxHp, int currentHp, bool isDie, float distance)
    {
        this.maxHp = maxHp;
        this.currentHp = currentHp;
        this.isDie = isDie;
        this.distance = distance;
    }
    public abstract void OnAttack();
    public virtual void DecreaseHp(int damage)
    {
        if (isDie == false)
        {
            StartCoroutine(transform.GetComponentInChildren<MonsterHitShader>().HitMotion());
        }
        if (currentHp > 0)
        {
            currentHp -= damage;
            UiManager.instance.MonsterHpOnActive(gameObject.GetComponent<Monster>());
            eventHp?.Invoke(currentHp, maxHp);
        }
        else
        {
            isDie = true;
            return;
        }
    }
    public abstract void OnDie();
}

