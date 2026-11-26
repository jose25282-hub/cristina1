
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class AguilaController : MonoBehaviour
{
    public float velocidad = 5f;

    [SerializeField] private int puntosGema = 10;
    [SerializeField] private int puntosCereza = -5;
    [SerializeField] private TMP_Text textoPuntaje;

    [SerializeField] private JoystickVirtual joystick;

    private int puntaje = 0;
    private Rigidbody2D rb;
    private Vector2 direccion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ActualizarTextoPuntaje();
    }

    void Update()
    {
        Vector2 movimientoTeclado = Vector2.zero;

        if (Keyboard.current != null)
        {
            float x = 0;
            float y = 0;

            if (Keyboard.current.wKey.isPressed) y = 1;
            if (Keyboard.current.sKey.isPressed) y = -1;
            if (Keyboard.current.aKey.isPressed) x = -1;
            if (Keyboard.current.dKey.isPressed) x = 1;

            movimientoTeclado = new Vector2(x, y).normalized;
        }

        Vector2 movimientoJoystick = Vector2.zero;

        if (joystick != null)
        {
            movimientoJoystick = joystick.Direccion;
        }

        if (movimientoJoystick.sqrMagnitude > 0.01f)
        {
            direccion = movimientoJoystick;
        }
        else
        {
            direccion = movimientoTeclado;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direccion * velocidad;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Gema"))
        {
            CambiarPuntaje(puntosGema);
            Destroy(otro.gameObject);
        }
        else if (otro.CompareTag("Cereza"))
        {
            CambiarPuntaje(puntosCereza);
            Destroy(otro.gameObject);
        }
    }

    void CambiarPuntaje(int cantidad)
    {
        puntaje += cantidad;
        Debug.Log("Puntaje: " + puntaje);
        ActualizarTextoPuntaje();
    }

    void ActualizarTextoPuntaje()
    {
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Tu puntaje: " + puntaje;
        }
    }
}
