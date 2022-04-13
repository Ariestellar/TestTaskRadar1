using System;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{    
    public event Action CoinChanged;
    public int Current {get => _current;}

    private int _current;

    public void Init(int countStartCoins)
    {
        _current = countStartCoins;
    }

    public void Collect()
    {
        _current += 1;           
        CoinChanged?.Invoke();
    }
}
