using UnityEngine;

public class BonusFood : MonoBehaviour
{
    [SerializeField] private float _lifetime = 5f;

    private float _timer;
    private FoodSpawner _foodSpawner;

    public void Initialize(FoodSpawner foodSpawner)
    {
        _foodSpawner = foodSpawner;
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _lifetime)
        {
            _foodSpawner.HandleBonusFoodExpired();
            Destroy(gameObject);
        }
    }
}
