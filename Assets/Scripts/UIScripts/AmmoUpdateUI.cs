using UnityEngine;
using TMPro;

public class AmmoUpdateUI : MonoBehaviour
{
    [SerializeField] private AmmoSystem _ammoSystem;
    [SerializeField] private TMP_Text _capacityAmmoText;
    [SerializeField] private RectTransform _ammoView;

    [SerializeField] private Vector3 _offset;

    private void Start()
    {
        OnEnable();
        OnDisable();
    }

    private void OnEnable()
    {
        _ammoSystem.OnAmmoChanged += UpdateAmmo;
    }

    private void OnDisable()
    {
        _ammoSystem.OnAmmoChanged -= UpdateAmmo;
    }
    private void Update()
    {
        _ammoView.position = _ammoSystem.transform.position + _offset;
    }

    private void UpdateAmmo(float value)
    {
        _capacityAmmoText.text = _ammoSystem.CapacityAmmo.ToString();
    }
}
