using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float speed = 5.0f;
    // Nueva variable para controlar qué tan alto salta el astronauta
    public float jumpHeight = 1.2f; 
    
    [Header("Física Espacial")]
    // Gravedad real de Marte: -3.72 m/s²
    public float gravity = -3.72f; 

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. Detección de suelo
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        // 2. Leer teclado para caminar
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // 3. Mecánica de Salto (¡NUEVO!)
        // Si presionamos Espacio (Jump) y estamos tocando el suelo...
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            // Aplicamos la fórmula física para la velocidad de salto
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 4. Aplicar la fuerza de gravedad y la caída
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); 
    }
}