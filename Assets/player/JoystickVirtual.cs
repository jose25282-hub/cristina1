
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickVirtual : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    [SerializeField] private RectTransform fondo;
    [SerializeField] private RectTransform joystick;

    [SerializeField] private float radio = 70f;

    public Vector2 Direccion { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 posicion;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            fondo,
            eventData.position,
            eventData.pressEventCamera,
            out posicion
        );

        Direccion = Vector2.ClampMagnitude(
            posicion / radio, 1f
        );

        joystick.anchoredPosition = Direccion * radio;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direccion = Vector2.zero;
        joystick.anchoredPosition = Vector2.zero;
    }
}
