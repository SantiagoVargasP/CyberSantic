using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float speed = 5.0f;
    
    [Header("Física Espacial")]
    // Gravedad real de Marte: -3.72 m/s² (En la Tierra es -9.81 m/s²)
    public float gravity = -3.72f; 

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        // Unity busca el Character Controller automáticamente
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. Detección de suelo: Si estamos tocando el piso, detenemos la caída
        if (controller.isGrounded && velocity.y < 0)
        {
            // Se usa -2f en lugar de 0 para asegurar que el motor registre el contacto continuo
            velocity.y = -2f; 
        }

        // 2. Leer las teclas WASD o flechas direccionales
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 3. Mover al personaje respetando hacia dónde está mirando
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // 4. Aplicar la fuerza de gravedad marciana
        velocity.y += gravity * Time.deltaTime;
        
        // El movimiento vertical (caída) se aplica por separado
        controller.Move(velocity * Time.deltaTime); 
    }
}