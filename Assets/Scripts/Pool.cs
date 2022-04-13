using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T: MonoBehaviour
{
    public bool AutoExpand { get => _autoExpand; set => _autoExpand = value; }
    public bool IsActiveByDefault { get => _isActiveByDefault; set => _isActiveByDefault = value; }
    public List<T> PoolObjects { get => poolObjects; }

    private List<T> poolObjects;
    private T _prefabObject;    
    private bool _isActiveByDefault;    
    private bool _autoExpand;    

    public Pool(T prefab, int count)
    {
        _prefabObject = prefab;        
        CreatePool(count);
        _isActiveByDefault = false;
        _autoExpand = true;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
            return element;

        if (_autoExpand)
            return CreateElement(true);

        throw new Exception("Нет свободного элемента");        
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var item in poolObjects)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    private void CreatePool(int count)
    {
        poolObjects = new List<T>();
        for (int i = 0; i < count; i++)
        {
           poolObjects.Add(CreateElement(_isActiveByDefault));
        }
    }

    private T CreateElement(bool isActiveByDefault)
    {
        var createObject = UnityEngine.Object.Instantiate(_prefabObject);
        createObject.gameObject.SetActive(isActiveByDefault);        
        return createObject;
    }
}
