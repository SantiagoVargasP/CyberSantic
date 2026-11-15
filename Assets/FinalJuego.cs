
using UnityEngine;

public class FinalJuego : MonoBehaviour
{
    public GameObject panelFinal;
    public GameObject coheteRegreso;

    public void FinalizarMision()
    {
        if (coheteRegreso != null)
            coheteRegreso.SetActive(false);

        if (panelFinal != null)
            panelFinal.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("¡CYBERSANTIC COMPLETADO!");
    }
}
