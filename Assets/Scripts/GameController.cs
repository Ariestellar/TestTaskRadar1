using System;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private DataGame _dataGame;
    [SerializeField] private Player _playerPrefab;    
    [SerializeField] private Transform _pointSpawnPlayer;    
    [SerializeField] private Spawner _spawner;    
    [SerializeField] private GameObject _panelEnd;
    [SerializeField] private TextMeshProUGUI _countLifeText;
    [SerializeField] private TextMeshProUGUI _countCoinText;

    private int _currentIndexlevelsSpeedGames;
    private Player _player;

    public void StartGame()
    {
        _currentIndexlevelsSpeedGames = 0;

        if (_player != null)
        {
            Destroy(_player.gameObject);
        }

        _player = Instantiate(_playerPrefab, _pointSpawnPlayer.position, Quaternion.identity);
        _player.Init(_dataGame.countStartLife, _countLifeText, _countCoinText);
        _player.DragInput.enabled = true;
        _player.CoinCounter.CoinChanged += IncrementSpeedGame;
        _player.PlayerLife.LifeEnd += EndGame;
        _spawner.Init(_dataGame.startSpeed);
        _panelEnd.SetActive(false);
    }

    public void IncrementSpeedGame()
    {
        if (_player.CoinCounter.Current >= _dataGame.levelsSpeedGames[_currentIndexlevelsSpeedGames].countScoreForActivation)
        {
            float newSpeed = _dataGame.startSpeed * _dataGame.levelsSpeedGames[_currentIndexlevelsSpeedGames].increaseSpeed;

            _spawner.CurrentSpeed = newSpeed;

            foreach (var item in _spawner.PoolCoin.PoolObjects)
            {
                item.Speed = newSpeed;
            }

            foreach (var item in _spawner.PoolLet.PoolObjects)
            {
                item.Speed = newSpeed;
            }

            _currentIndexlevelsSpeedGames = Mathf.Clamp(_currentIndexlevelsSpeedGames, _currentIndexlevelsSpeedGames + 1, _dataGame.levelsSpeedGames.Length - 1);
        }        
    }

    public void EndGame()
    {
        StopGame();
        _panelEnd.SetActive(true);
        _player.CoinCounter.CoinChanged -= IncrementSpeedGame;
    }

    private void StopGame()
    {
        _player.DragInput.enabled = false;
        foreach (var item in _spawner.PoolCoin.PoolObjects)
        {
            item.Speed = 0;
        }

        foreach (var item in _spawner.PoolLet.PoolObjects)
        {
            item.Speed = 0;
        }
    }
}
