using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameInputReaderSO _input;
    [SerializeField] private RaycastHit _hit;
    [SerializeField] private Ray _ray;
    [SerializeField] private Vector3 _clickTransform;
    public event Action<Vector3> ClickTransform;
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
