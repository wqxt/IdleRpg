using UnityEngine;

public class OutlineOnHover : MonoBehaviour
{
    public Material outlineMaterial; // �������� � ������
    public Material defaultMaterial; // ������� ��������

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnMouseEnter()
    {
        rend.material = outlineMaterial; // ������������ �������� � ������
    }

    void OnMouseExit()
    {
        rend.material = defaultMaterial; // ������� ������� ��������
    }
}
