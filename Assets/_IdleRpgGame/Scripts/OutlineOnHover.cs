using UnityEngine;

public class OutlineOnHover : MonoBehaviour
{
    public Material outlineMaterial; // Материал с рамкой
    public Material defaultMaterial; // Обычный материал

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnMouseEnter()
    {
        rend.material = outlineMaterial; // Активировать материал с рамкой
    }

    void OnMouseExit()
    {
        rend.material = defaultMaterial; // Вернуть обычный материал
    }
}
