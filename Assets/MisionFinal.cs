using UnityEngine;

public class MisionFinal : MonoBehaviour
{
    public GameObject panelMision2;
    public GameObject coheteRegreso;

    public void CompletarMision2()
    {
        if (panelMision2 != null)
            panelMision2.SetActive(false);

        if (coheteRegreso != null)
            coheteRegreso.SetActive(true);

        Debug.Log("¡Misión 2 completada!");
    }
}