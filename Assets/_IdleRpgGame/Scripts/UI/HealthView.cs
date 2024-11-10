using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentHealthValueText;
    [SerializeField] private TextMeshProUGUI _maxHealthValueText;
    [SerializeField] private Slider _healthSlider;


    //  Для тестирования
    public int CurrentHealthTextValue => int.Parse(_currentHealthValueText.text);
    public float HealthSliderValue => _healthSlider.value;


    public void Initialize(TextMeshProUGUI currentHealthText, TextMeshProUGUI maxHealthText, Slider healthSlider)
    {
        _currentHealthValueText = currentHealthText;
        _maxHealthValueText = maxHealthText;
        _healthSlider = healthSlider;
    }

    public void UpdateHealth(int currentHealthValue)
    {
        _healthSlider.value = currentHealthValue;
        _currentHealthValueText.text = currentHealthValue.ToString();
    }

    public void SetupHealth(int startHealthValue)
    {
        _healthSlider.maxValue = startHealthValue;
        _healthSlider.value = startHealthValue;
        _maxHealthValueText.text = startHealthValue.ToString();
        _currentHealthValueText.text = startHealthValue.ToString();
    }
}