using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameInputReaderSO _input;
    private RaycastHit _hit;
    private Ray _ray;

    private void OnEnable()
    {
        _input.ClickEvent += ClickCheck;
        _input.TouchEvent += ClickCheck;
    }

    private void OnDisable()
    {
        _input.ClickEvent -= ClickCheck;
        _input.TouchEvent -= ClickCheck;
    }

    private void CameraCheck()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit, 100);
    }

    private void ClickCheck()
    {
        CameraCheck();
    }
}
