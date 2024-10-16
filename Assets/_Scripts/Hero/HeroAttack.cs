using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : HeroAbstract
{
    [Header("Hero Attack")]
    [SerializeField] protected bool isAutoAttack = false;
    [SerializeField] protected bool isAttacking = false;
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float autoAttackDelay = 1f;
    [SerializeField] protected float attackDelay = 0.5f;
    public bool IsAutoAttack => isAutoAttack;   
    public bool IsAttacking => isAttacking;

    protected override void Update()
    {
        base.FixedUpdate();
        this.AutoAttacking();
        this.MouseAttacking();
    }

    protected virtual void AutoAttacking()
    {
        if(!this.CheckCanAutoShoot()) return;

        this.isAttacking = false;

        this.timer += Time.deltaTime;
        if (this.timer < this.autoAttackDelay) return;
        this.timer = 0f;

        this.SpawnBullet();
    }

    protected virtual void MouseAttacking()
    {
        if(this.isAutoAttack) return;
        if(!this.CheckCanMosueShoot()) return;

        this.isAttacking = false;

        this.timer += Time.deltaTime;
        if (this.timer < this.attackDelay) return;
        this.timer = 0f;

        this.SpawnBullet();
    }

    protected virtual void SpawnBullet()
    {
        this.isAttacking = true;

        Vector3 spawnPos = transform.position;
        Quaternion spawnRot = transform.rotation;
        Transform bullet = BulletSpawner.Instance.SpawnPrefab(BulletSpawner.blueBullet, spawnPos, spawnRot);
        if (bullet == null) return;
        bullet.gameObject.SetActive(true);
    }

    protected virtual bool CheckCanAutoShoot()
    {
        if (!this.CheckIsCurrentHero()) this.isAutoAttack = true;
        else this.isAutoAttack = false;
        return this.heroCtrl.HeroFindEnemy.IsFindEnemy && this.isAutoAttack;
    }
    
    protected virtual bool CheckCanMosueShoot()
    {
        return InputManager.Instance.MouseInput == 1;
    }
}
