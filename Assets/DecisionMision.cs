using UnityEngine;
using TMPro; // Para usar textos
using UnityEngine.UI; // Para usar botones y barras

public class DecisionMision : MonoBehaviour
{
    [Header("Pantalla de Decisión")]
    public GameObject panelMenu; // El cuadro con los botones
    public TextMeshProUGUI textoAviso; // Donde diremos qué pasó al final

    [Header("Recursos de la Base")]
    public float energia = 100f;
    public float comida = 100f;

    [Header("Conexión con el Astronauta")]
    public RadiationSystem sistemaTraje;

    [Header("Misiones")]
    public DecisionMision scriptMisionTormenta;

    // Esta función se activará al presionar el Botón A
    public void ElegirEscudoMaximo()
    {
        energia -= 60f; // Gasta mucha energía
        comida -= 20f;  // Las plantas se dañan por falta de luz
        
        panelMenu.SetActive(false); // Escondemos el menú
        textoAviso.text = "Escudo al 100%. Gastaste mucha energía y las plantas no crecerán hoy.";
        textoAviso.color = Color.yellow;
        
        sistemaTraje.isSafe = true; // El astronauta no recibe daño
    }

    // Esta función se activará al presionar el Botón B
    public void ElegirEscudoMedio()
    {
        energia -= 10f; // Ahorras energía
        
        panelMenu.SetActive(false);
        textoAviso.text = "Escudo al 50%. Ahorraste energía, pero la radiación entró a la base.";
        textoAviso.color = new Color(1f, 0.3f, 0.2f); // Naranja/Rojo

        sistemaTraje.isSafe = false;
        sistemaTraje.currentHealth -= 30f; // El traje pierde vida por la radiación que logró entrar
    }
void Start()
{
    // Apaga el menú en el primer segundo del juego
    if (panelMenu != null) panelMenu.SetActive(false);
}

public void MostrarMenuTormenta()
{
    // DONKI llamará a esta función para encender el menú
    if (panelMenu != null) panelMenu.SetActive(true);
}
}