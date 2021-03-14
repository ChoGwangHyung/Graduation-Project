﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    protected Rigidbody rb;
    protected CapsuleCollider col;
    protected Animator ani;

    [SerializeField] protected ParticleSystem bloodSpit;
    [SerializeField] protected Slider hpBar;

    [SerializeField] protected float curLife;
    protected float maxLife;
    protected float walkSpeed;

    protected bool haveDamagedByBullet = false;
    public bool _haveDamagedByBullet { get { return haveDamagedByBullet; } }
    protected bool haveDamagedByBarrel = false;
    public bool _haveDamagedByBarrel { get { return haveDamagedByBarrel; } }
    protected bool isDead = false;
    public bool _isDead { get { return isDead; } }

    protected void SetCharacterStat(float maxLife, float walkSpeed)
    {
        this.maxLife = maxLife;
        this.curLife = maxLife;
        hpBar.value = curLife / maxLife;
        this.walkSpeed = walkSpeed;        
    }

    protected void HpBarLookAtCamera()
    {
        hpBar.transform.LookAt(MainCamera.instance.transform);
    }

    protected void HpReset()
    {
        curLife = maxLife;
        hpBar.value = curLife / maxLife;
    }

    private void HaveDamagedByBulletInit() { haveDamagedByBullet = false; }
    private void HaveDamagedByBarrelInit() { haveDamagedByBarrel = false; }

    virtual protected void Dead()
    {
        isDead = true;
    }

    //총알 데미지
    public void ReceiveDamage(float damage, Vector3 damagePos)
    {
        bloodSpit.transform.position = damagePos;
        bloodSpit.transform.rotation = Quaternion.Euler(0, Random.Range(-90.0f, 90.0f), 90.0f);
        bloodSpit.Play();

        DownLife(damage);

        haveDamagedByBullet = true;
        if (IsInvoking("HaveDamagedByBulletInit") == true) CancelInvoke("HaveDamagedByBulletInit");
        else Invoke("HaveDamagedByBulletInit", 0.2f);
    }

    //베럴 데미지
    public void ReceiveDamage(float damage)
    {
        DownLife(damage);

        haveDamagedByBarrel = true;
        if (IsInvoking("HaveDamagedByBarrelInit") == true) CancelInvoke("HaveDamagedByBarrelInit");
        else Invoke("HaveDamagedByBarrelInit", 0.2f);
    }

    private void DownLife(float damage)
    {
        curLife -= damage;
        hpBar.value = curLife / maxLife;

        if (curLife <= 0)
        {            
            Dead();
            return;
        }
    }    
}