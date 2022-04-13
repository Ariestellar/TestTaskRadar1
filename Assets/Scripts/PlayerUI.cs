using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerLife))]
[RequireComponent(typeof(CoinCounter))]
public class PlayerUI : MonoBehaviour
{
    private TextMeshProUGUI _countLifeText;
    private TextMeshProUGUI _countCoinText;
    private PlayerLife _playerLife;
    private CoinCounter _coinCounter;

    private void Awake()
    {
        _playerLife = GetComponent<PlayerLife>();   
        _coinCounter = GetComponent<CoinCounter>();   
    }

    private void OnEnable()
    {
        _playerLife.LifeChanged += UpdateLifeBar;            
        _coinCounter.CoinChanged += UpdateCountCoin;            
    }

    private void OnDisable()
    {      
        _playerLife.LifeChanged -= UpdateLifeBar;
        _coinCounter.CoinChanged -= UpdateCountCoin;
    }

    public void Init(TextMeshProUGUI countLifeText, TextMeshProUGUI countCoinText)
    {
        _countLifeText = countLifeText;
        _countCoinText = countCoinText;
        UpdateLifeBar();
        UpdateCountCoin();
    }

    private void UpdateLifeBar()
    {
        _countLifeText.text = _playerLife.Current.ToString();
    }

    private void UpdateCountCoin()
    {
        _countCoinText.text = _coinCounter.Current.ToString();
    }
}
