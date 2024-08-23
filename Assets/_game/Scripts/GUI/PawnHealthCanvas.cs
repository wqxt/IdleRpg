using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PawnHealthCanvas : MonoBehaviour
{
    [SerializeField] protected internal Configuration _pawnConfiguration;
    [SerializeField] protected internal TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] protected internal Slider _pawnHealthSlider;

    private void LateUpdate()
    {
        _pawnHealthSlider.value = _pawnConfiguration.CurrentHealthValue;
        _textMeshProUGUI.text = _pawnHealthSlider.value.ToString();
    }
}
