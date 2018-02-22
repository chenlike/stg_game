﻿using UnityEngine;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace Share
{
    static class ObjectPool
    {
        static Dictionary<string, ConcurrentQueue<GameObject>> pool = new Dictionary<string, ConcurrentQueue<GameObject>>();
        /// <summary>
        /// 从池中获得obj
        /// </summary>
        /// <param name="name">名字</param>
        /// <returns>obj||null</returns>
        public static GameObject FindGameObject(string name)
        {
            //如果不存在该name或者 存在但是数量为0时 null
            if (!pool.ContainsKey(name))
                return null;
            if (pool[name].Count == 0)
            {
                return null;
            }
            GameObject tryOut;
            pool[name].TryDequeue(out tryOut);
            return tryOut;
        }
        /// <summary>
        /// 停用对象增加到池中
        /// </summary>
        /// <param name="obj"></param>
        public static void AddToPool(GameObject obj)
        {
            string temName = obj.name;
            //检查Key
            if (pool.ContainsKey(temName) == false)
            {
                pool[obj.name] = new ConcurrentQueue<GameObject>();
            }

            //停用 以及 初始化
            obj.SetActive(false);
            obj.GetComponent<IObjectPool>().SetDefault();
            pool[temName].Enqueue(obj);
        }
    }

}
