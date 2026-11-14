using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Sensibilidad del visor")]
    public float mouseSensitivity = 100f;

    [Header("Referencia al cuerpo")]
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        // Oculta y bloquea el cursor del mouse en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. Leer el movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 2. Calcular la rotación vertical (mirar arriba/abajo)
        xRotation -= mouseY;
        // Limitar la rotación a 90 grados para que el cuello no gire como un búho
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 3. Aplicar la rotación vertical solo a la cámara
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 4. Aplicar la rotación horizontal a todo el cuerpo del jugador
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
