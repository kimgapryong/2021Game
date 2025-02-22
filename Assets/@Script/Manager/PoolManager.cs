using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
public class Pool
{
    public GameObject prefab;

    private Transform _root;
    public Transform Root
    {
        get
        {
            if(_root == null)
            {
                GameObject rootname = new GameObject($"{prefab.name}_Root");
                _root = rootname.transform;
            }
            return _root;
        }
    }
    public IObjectPool<GameObject> pools;

    public Pool(GameObject obj)
    {
        prefab = obj;

        pools = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    public GameObject Pop()
    {
        return pools.Get();
    }

    public void Push(GameObject @object)
    {
        pools.Release(@object);
    }

    private void ActionOnDestroy(GameObject @object)
    {
        UnityEngine.Object.Destroy(@object);
    }

    private void ActionOnRelease(GameObject @object)
    {
        @object.SetActive(false);
    }

    private void ActionOnGet(GameObject @object)
    {
        @object.SetActive(true);
    }

    private GameObject CreateFunc()
    {
        GameObject go = UnityEngine.Object.Instantiate(prefab);
        go.name = prefab.name;
        go.transform.parent = Root;

        return go;
    }
}
public class PoolManager
{
    Dictionary<string, Pool> poolsKey = new Dictionary<string, Pool>();

    private Transform _root;
    public Transform Root
    {
        get
        {
            if(_root == null)
            {
                GameObject obj = new GameObject("Root");
                _root = obj.transform;
            }
            return _root;
        }
    }
    public GameObject Pop(GameObject go)
    {
        if (!poolsKey.ContainsKey(go.name))
            CreatePool(go);

        return poolsKey[go.name].Pop();
    }
    public bool Push(GameObject go)
    {
        if(!poolsKey.ContainsKey(go.name))
            return false;
        poolsKey[go.name].Push(go);

        return true;
    }

    public void CreatePool(GameObject go)
    {
        Pool pool = new Pool(go);
        pool.Root.parent = Root;

        poolsKey.Add(go.name, pool);

    }
}
