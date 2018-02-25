﻿using UnityEngine;
using System.Collections;
using Base;

public class Bullet : Enemy,IObjectPool
{
    /// <summary>
    /// 所有者
    /// </summary>
    public string owner { get; set; }
    /// <summary>
    /// 是否触碰墙壁调用touchWallEvent
    /// </summary>
    bool isTouchWallDead = true;
    /// <summary>
    /// TouchEvent
    /// </summary>
    /// <param name="obj">自身</param>
    /// <param name="touchObj">触碰者</param>
    public delegate void TouchEvent(GameObject obj, GameObject touchObj);
    public TouchEvent touchEvent;


    public void SetDefault()
    {
        SetDisable();
        owner = null;
        startEvent = null;
        touchEvent = TouchWall;
        updateEvent = FlyForward;
    }
    /// <summary>
    /// 触屏到墙壁
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="touchObj"></param>
    protected void TouchWall(GameObject obj, GameObject touchObj)
    {
        if (touchObj.gameObject.transform.parent != null && touchObj.gameObject.transform.parent.gameObject.name == "Wall")
            DestroyMe();
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (isTouchWallDead)
        {
            touchEvent?.Invoke(this.gameObject, collision.gameObject);
        }
    }
}