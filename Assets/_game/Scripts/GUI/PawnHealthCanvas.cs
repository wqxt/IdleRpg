using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PawnHealthCanvas : MonoBehaviour
{
    [SerializeField] private PawnConfiguration _pawnConfiguration;
    [SerializeField] private TextMeshProUGUI _currentHealthValueText;
    [SerializeField] private TextMeshProUGUI _maxHealthValueText;
    [SerializeField] private Slider _pawnHealthSlider;
    public event Action PawnHealthEnded;

    private void Start()
    {
        _pawnHealthSlider.maxValue = _pawnConfiguration.MaxHealthValue;
        _pawnHealthSlider.minValue = _pawnConfiguration.MinHealthValue;
    }

    private void LateUpdate()
    {
        _pawnHealthSlider.value = _pawnConfiguration.CurrentHealthValue;
        _currentHealthValueText.text = _pawnHealthSlider.value.ToString();
        _maxHealthValueText.text = _pawnHealthSlider.maxValue.ToString();

        if (_pawnConfiguration.CurrentHealthValue == 0)
        {
            PawnHealthEnded?.Invoke();
        }
    }
}