﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base;

public abstract class SpellCard
{
    /// <summary>
    /// 初始化设置 以及加载资源
    /// </summary>
    public abstract void InitAndLoadResources();
    /// <summary>
    /// 符卡的提前准备工作
    /// </summary>
    public abstract void Prepare();
    /// <summary>
    /// 开始施放符卡
    /// </summary>
    public abstract void Spell();
    /// <summary>
    /// 停止（结束）符卡
    /// </summary>
    public abstract void StopSpell();
    /// <summary>
    /// 符卡名
    /// </summary>
    public string spellCardName { get; set; }
    /// <summary>
    /// 符卡持续时间
    /// </summary>
    public float spellKeepTime { get; set; }
    /// <summary>
    /// 施放符卡前等待时间
    /// </summary>
    public float beforeSpellTime { get; set; }
    /// <summary>
    /// 当前施放的符卡的Obj
    /// </summary>
    public GameObject spellGameObject { get; set; }

    public Vector3 startPosition = new Vector3(0, 0, 0);
    /*
        为了获得StartCoroutine方法先创建个GameObject挂上脚本 设置属性 不影响到其他物体
    */
    private GameObject _privateObj = new GameObject();
    private GameObjectBase bulletShooter;
    private bool isBulletShooterNull = false;

    private void InitBulletShooter()
    {
        _privateObj.name = "privateObj";
        _privateObj.tag = "EnemyBullet";
        bulletShooter = _privateObj.AddComponent<GameObjectBase>();
        bulletShooter.SetEnable();
    }

    /// <summary>
    /// 启动协程
    /// </summary>
    /// <param name="routine"></param>
    protected Coroutine StartCoroutine(IEnumerator routine)
    {
        if (!isBulletShooterNull)
            InitBulletShooter();
        return bulletShooter.StartCoroutine(routine);
    }
    protected void StopCoroutine(string methodName)
    {
        if (!isBulletShooterNull)
            InitBulletShooter();
        bulletShooter.StopCoroutine(methodName);
    }
    protected void StopAllCoroutine()
    {
        if (!isBulletShooterNull)
            InitBulletShooter();
        bulletShooter.StopAllCoroutines();
    }
   
    /// <summary>
    /// 创建一个空的BulletShooter Gameobject
    /// </summary>
    /// <returns>false Active的GameObject</returns>
    public BulletShooter CreateEmptyBulletShooter()
    {
        GameObject empty = new GameObject();
        empty.name = "GameObjShooter";
        empty.tag = "EnemyBullet";
        BulletShooter bulletShooter =  empty.AddComponent<BulletShooter>();
        empty.transform.rotation = Quaternion.identity;
        bulletShooter.SetDefault();
        return bulletShooter;
    }
    public BulletShooter CreateEmptyBulletShooter(Transform parent)
    {
        GameObject empty = new GameObject();
        empty.name = "GameObjShooter";
        empty.tag = "EnemyBullet";
        BulletShooter bulletShooter = empty.AddComponent<BulletShooter>();
        bulletShooter.transform.parent = parent;
        bulletShooter.transform.position = parent.transform.position;
        empty.transform.rotation = Quaternion.identity;
        bulletShooter.SetDefault();
        return bulletShooter;
    }
    /// <summary>
    ///  按位置 创建一个空的BulletShooter Gameobject
    /// </summary>
    /// <param name="pos">坐标</param>
    /// <returns>false Active的GameObject</returns>
    public BulletShooter CreateEmptyBulletShooter(Vector3 pos)
    {
        BulletShooter t = CreateEmptyBulletShooter();
        GameObject empty = t.gameObject;
        empty.transform.position = pos;
        return t;
    }
    /// <summary>
    /// 启动BulletShooter
    /// </summary>
    /// <param name="list"></param>
    protected void SetActiveList(List<GameObject> list)
    {
        list.ForEach(obj =>
        {
            obj.SetActive(true);
        });
    }



}
