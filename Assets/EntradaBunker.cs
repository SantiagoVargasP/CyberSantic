using UnityEngine;

public class EntradaBunker : MonoBehaviour
{
    [Header("Conectar con la misión")]
    public DecisionMision scriptMisionTormenta;

    // Esta función se activa cuando alguien choca/entra en la zona
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos que el que entró sea el astronauta (Player)
        if (other.CompareTag("Player"))
        {
            if (scriptMisionTormenta != null)
            {
                scriptMisionTormenta.MostrarMenuTormenta();
            }
        }
    }
}