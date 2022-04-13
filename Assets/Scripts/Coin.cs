using UnityEngine;

public class Coin : InteractiveObject
{
    protected override void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
    }
}
