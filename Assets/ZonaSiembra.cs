
using UnityEngine;

public class ZonaSiembra : MonoBehaviour
{
    public GameObject botonSembrar;

    void Start()
    {
        botonSembrar.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            botonSembrar.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            botonSembrar.SetActive(false);
    }
}

