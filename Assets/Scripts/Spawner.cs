using System;
using System.Collections;
using UnityEngine;

public enum TypeInteractiveObject
{ 
    Let = 0,
    Coin = 1
}


public class Spawner : MonoBehaviour
{
    public Pool<InteractiveObject> PoolLet { get => _poolLet; }
    public Pool<InteractiveObject> PoolCoin { get => _poolCoin; }
    public float CurrentSpeed { set => _currentSpeed = value; }

    [SerializeField] private Transform _leftSpawnPoint;
    [SerializeField] private Transform _rightSpawnPoint;
    [SerializeField] private Transform _pointDeadLine; 

    [SerializeField] private InteractiveObject _prefabLet;
    [SerializeField] private InteractiveObject _prefabCoin;
    [SerializeField] private float _allowedSpawnRadius;

    private float _currentSpeed;
    private Pool<InteractiveObject> _poolLet;
    private Pool<InteractiveObject> _poolCoin;   

    public void Init(float startSpeed)
    {
        _currentSpeed = startSpeed;

        Clean();
        _poolLet = new Pool<InteractiveObject>(_prefabLet, 10);
        _poolCoin = new Pool<InteractiveObject>(_prefabCoin, 10);
        StartCoroutine(DelayRandomSpawn());
    }

    public void Clean()
    {
        if (_poolLet != null)
        { 
            foreach (var item in _poolLet.PoolObjects)
            {
                Destroy(item.gameObject);
            }
        }
        
        if (_poolCoin != null)
        { 
            foreach (var item in _poolCoin.PoolObjects)
            {
                Destroy(item.gameObject);
            }
        }        
    }

    private IEnumerator DelayRandomSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            RandomSpawnObject();
        }        
    }

    private void RandomSpawnObject()
    {
        float positionX = UnityEngine.Random.Range(_leftSpawnPoint.position.x, _rightSpawnPoint.position.x);        
        Vector3 randoPointSpawn = new Vector3(positionX, _leftSpawnPoint.position.y, _leftSpawnPoint.position.z);
        InteractiveObject randomInteractiveObject;

        TypeInteractiveObject randomIndexTypeInteractiveObject = (TypeInteractiveObject)UnityEngine.Random.Range(0, Enum.GetNames(typeof(TypeInteractiveObject)).Length);         
                
        if (!Physics.CheckSphere(randoPointSpawn, _allowedSpawnRadius))
        { 
            Vector3 pointDead = new Vector3(randoPointSpawn.x, _pointDeadLine.position.y, randoPointSpawn.z);
            switch (randomIndexTypeInteractiveObject)
            {
                case TypeInteractiveObject.Let:
                    randomInteractiveObject = _poolLet.GetFreeElement();
                    break;
                case TypeInteractiveObject.Coin:
                    randomInteractiveObject = _poolCoin.GetFreeElement();
                    break;
                default:
                    throw new Exception("Нет такого типа элемента");                    
            }
            
            randomInteractiveObject.Init(randoPointSpawn, pointDead, _currentSpeed);
        }  
    }

    void OnDrawGizmos()
    {        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftSpawnPoint.position, _rightSpawnPoint.position);
    }
}


