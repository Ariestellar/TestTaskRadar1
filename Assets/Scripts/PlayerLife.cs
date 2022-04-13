using System;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{    
    public event Action LifeChanged;
    public event Action LifeEnd;

    public int Current {get => _current;}

    private int _current;

    public void Init(int countStartLife)
    {
        _current = countStartLife;
    }

    public void TakeDamage()
    {
        _current -= 1;           
        LifeChanged?.Invoke();
        if (_current == 0)
        {
            LifeEnd?.Invoke();
        }
    }
}
