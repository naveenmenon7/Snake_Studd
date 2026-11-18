using UnityEngine;

public class BonusFood : MonoBehaviour
{
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private float _pulseSpeed = 4f;
    [SerializeField] private float _pulseAmount = 0.12f;

    private float _timer;
    private float _initialScale;
    private FoodSpawner _foodSpawner;
    private Transform _visualTransform;

    public void Initialize(FoodSpawner foodSpawner)
    {
        _foodSpawner = foodSpawner;
        _visualTransform = transform;
        _initialScale = transform.localScale.x;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        float pulse = 1f + Mathf.Sin(Time.time * _pulseSpeed) * _pulseAmount;
        _visualTransform.localScale = Vector3.one * (_initialScale * pulse);

        if (_timer >= _lifetime)
        {
            _foodSpawner.HandleBonusFoodExpired(gameObject);
            Destroy(gameObject);
        }
    }
}