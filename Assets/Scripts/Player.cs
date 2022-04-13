using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerLife))]
[RequireComponent(typeof(CoinCounter))]
[RequireComponent(typeof(ÑollisionÑheck))]
[RequireComponent(typeof(DragInput))]
[RequireComponent(typeof(PlayerUI))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _countStartLife;
    
    private PlayerLife _playerLife;
    private CoinCounter _coinCounter;
    private ÑollisionÑheck _collisionCheck;
    private DragInput _dragInput;
    private PlayerUI _playerUI;

    public CoinCounter CoinCounter { get => _coinCounter; }
    public PlayerLife PlayerLife { get => _playerLife; }
    public DragInput DragInput { get => _dragInput; }

    private void Awake()
    {
        _playerLife = GetComponent<PlayerLife>();
        _coinCounter = GetComponent<CoinCounter>();
        _collisionCheck = GetComponent<ÑollisionÑheck>();       
        _dragInput = GetComponent<DragInput>();       
        _playerUI = GetComponent<PlayerUI>();       
    }

    public void Init(int countStartLife, TextMeshProUGUI countLifeText, TextMeshProUGUI countCoinText)
    {
        _playerLife.Init(countStartLife);
        _coinCounter.Init(0);
        _playerUI.Init(countLifeText, countCoinText);        
    }

    private void OnEnable()
    {
        _collisionCheck.TriggerEnter += Collision;
    }

    private void OnDisable()
    {
        _collisionCheck.TriggerEnter -= Collision;
    }

    private void Collision(Collider other)
    {
        if (other.GetComponent<Coin>())
        {
            _coinCounter.Collect();
        }
        else if(other.GetComponent<Let>())
        {
            _playerLife.TakeDamage();
        }
    }     
}
