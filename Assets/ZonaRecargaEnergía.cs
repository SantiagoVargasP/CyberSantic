
using UnityEngine;

public class ZonaRecargaEnergia : MonoBehaviour
{
    public GameObject botonRecargar;

    void Start()
    {
        botonRecargar.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            botonRecargar.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            botonRecargar.SetActive(false);
    }
}
