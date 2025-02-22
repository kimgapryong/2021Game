using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

public class ResourcesManager
{
    private Dictionary<string, UnityEngine.Object> dataDic = new Dictionary<string, UnityEngine.Object>();

    public T Load<T>(string name) where T : UnityEngine.Object
    {
        if (dataDic.TryGetValue(name, out UnityEngine.Object objData) == false)
            return null;

        //���� T ������ ��ȯ �ȵǸ� �극��ũ ����Ʈ ����
        if (objData is T == false)
        {
            int a = 13;
        }

        return objData as T;
    }

    public GameObject Instantiate(string name, Vector3 vec = default, Transform trans = null, bool pooling = false)
    {
        if(vec == default)
            vec = Vector3.zero;

        GameObject obj = Load<GameObject>(name);
        if (obj == null) return null;

        if(pooling)
            return Manager.Pool.Pop(obj);

        GameObject clone = UnityEngine.Object.Instantiate(obj, trans);
        clone.name = name;
        clone.transform.position = vec;
        return clone;
    }

    public void Destroy(GameObject obj)
    {
        if (Manager.Pool.Push(obj))
            return;

        Destroy(obj);
    }
    public void LoadAsync<T>(string name, Action<T> callBack) where T : UnityEngine.Object
    {
        string keyName = name;
        if (keyName.Contains(".sprite"))
            keyName = $"{name}[{name.Replace(".sprite", "")}]";

        Addressables.LoadAssetAsync<T>(keyName).Completed += (op) =>
        {
            dataDic.Add(name, op.Result);
            callBack?.Invoke(op.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callBack) where T : UnityEngine.Object
    {
        Addressables.LoadResourceLocationsAsync(label, typeof(T)).Completed += (op) =>
        {
            int count = 0;
            int result = op.Result.Count;

            foreach (var item in op.Result)
            {
                LoadAsync<T>(item.PrimaryKey, (obj) =>
                {
                    count++;
                    callBack?.Invoke(item.PrimaryKey, count, result);
                });
            }
        };
    }

    //public void LoadSpriteAtlas(string atlasName, string spriteName)
    //{
    //    Addressables.LoadAssetAsync<UnityEngine.U2D.SpriteAtlas>(atlasName).Completed += (op) =>
    //    {
    //        if (op.Status == AsyncOperationStatus.Succeeded)
    //        {
    //            SpriteAtlas atlas = op.Result;

    //            // Ư�� �̸��� Sprite ��������
    //            Sprite targetSprite = atlas.GetSprite(spriteName);
    //            if (targetSprite != null)
    //                spriteDic.Add(atlasName, targetSprite);
    //        }
    //        else
    //        {
    //            Addressables.LoadAssetAsync<Sprite>(atlasName).Completed += (spriteOp) =>
    //            {
    //                if (spriteOp.Status == AsyncOperationStatus.Succeeded)
    //                {
    //                    // ���� Sprite�� �ε�Ǿ��� ��
    //                    Sprite targetSprite = spriteOp.Result;
    //                    if (targetSprite != null)
    //                    {
    //                        // ���� ��������Ʈ�� spriteDic�� �߰�
    //                        spriteDic.Add(atlasName, targetSprite);
    //                    }
    //                }
    //            };
    //        }
    //    };
    //}
}
