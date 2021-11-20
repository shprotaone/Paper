using UnityEngine;

public class HealthUpdateUI : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private GameObject[] _heartsUI;

    private void Start()
    {
        OnEnable();
        OnDisable();
    }

    private void OnEnable()
    {
        _healthSystem.OnHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        _healthSystem.OnHealthChanged -= UpdateHealth;
    }

    private void DrawLifes(int value, bool activate)
    {
        if (value >= -1)
        {
            if (activate)
            {
                _heartsUI[value - 1].SetActive(activate);
            }
            else if(value >=0)
            {
                _heartsUI[value].SetActive(activate);
            }
        }
    }

    private void UpdateHealth(int value, bool enable)
    {
        if (_healthSystem == null) return;

        DrawLifes(value, enable);
    }
}
