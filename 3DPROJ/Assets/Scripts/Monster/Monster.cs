using System;
using UnityEngine;



public abstract class Monster : MonoBehaviour
{
    public bool hpUiActive;
    public Coroutine hitMotionCor;
    public Action<float, float> eventHp;
    public int compensation;
    protected int maxHp { get; set; }
    protected int currentHp { get; set; }
    public bool isDie { get; set; }
    protected float distance { get; set; }
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
            if (hitMotionCor == null)
            {
                hitMotionCor = StartCoroutine(transform.GetComponentInChildren<MonsterHitShader>().HitMotion());
            }
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

