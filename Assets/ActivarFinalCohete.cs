
using UnityEngine;

public class ActivarFinalCohete : MonoBehaviour
{
    public FinalJuego finalJuego;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (finalJuego != null)
                finalJuego.FinalizarMision();
        }
    }
}
