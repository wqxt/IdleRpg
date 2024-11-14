using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[assembly: InternalsVisibleTo("EditModeTest")]
public class HealthView : MonoBehaviour
{
    [SerializeField] internal protected TextMeshProUGUI _currentHealthValueText;
    [SerializeField] internal protected TextMeshProUGUI _maxHealthValueText;
    [SerializeField] internal protected Slider _healthSlider;

    internal void Initialize(TextMeshProUGUI currentHealthText, TextMeshProUGUI maxHealthText, Slider healthSlider)
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

    public void SetupHealth(int startHealthValue, int maxHealthValue)
    {
        _healthSlider.maxValue = maxHealthValue;
        _healthSlider.value = startHealthValue;

        _maxHealthValueText.text = maxHealthValue.ToString();
        _currentHealthValueText.text = startHealthValue.ToString();
    }
}