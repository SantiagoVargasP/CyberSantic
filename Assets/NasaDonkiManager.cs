using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class NasaDonkiManager : MonoBehaviour
{
    [Header("Interfaz de Usuario")]
    public TextMeshProUGUI nasaAlertText;

    [Header("Conexión NASA API")]
    public string apiKey = "DEMO_KEY";
    public int daysToLookBack = 3;

    [Header("Referencias del Juego")]
    public RadiationSystem radiationSystem;

    [Header("Modo Demostración")]
    public bool forceStormSimulation = false;

    [Serializable]
    public class SolarFlare
    {
        public string flrID;
        public string beginTime;
        public string classType;
    }

    [Serializable]
    public class SolarFlareList
    {
        public SolarFlare[] flares;
    }

    void Start()
    {
        if (forceStormSimulation)
        {
            radiationSystem.isSolarStormActive = true;
            ActualizarTextoNASA("ALERTA SIMULADA: Tormenta Solar Clase X activa. ¡Busca refugio!", Color.red);
        }
        else
        {
            ActualizarTextoNASA("Conectando con telemetría espacial NASA DONKI...", Color.white);
            StartCoroutine(CheckSolarFlares());
        }
    }

    IEnumerator CheckSolarFlares()
    {
        string endDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
        string startDate = DateTime.UtcNow.AddDays(-daysToLookBack).ToString("yyyy-MM-dd");

        string url = $"https://ccmc.gsfc.nasa.gov/DONKI-API/get/FLR?startDate={startDate}&endDate={endDate}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string rawJson = request.downloadHandler.text.Trim();

                if (string.IsNullOrEmpty(rawJson) || rawJson == "[]")
                {
                    radiationSystem.isSolarStormActive = false;
                    ActualizarTextoNASA("NASA DONKI: Clima espacial estable. Superficie segura.", Color.green);
                }
                else
                {
                    string wrappedJson = "{\"flares\":" + rawJson + "}";
                    SolarFlareList flareData = JsonUtility.FromJson<SolarFlareList>(wrappedJson);

                    if (flareData.flares != null && flareData.flares.Length > 0)
                    {
                        SolarFlare latest = flareData.flares[flareData.flares.Length - 1];
                        radiationSystem.isSolarStormActive = true;
                        ActualizarTextoNASA($"¡ALERTA NASA! Hay mucho calor del Sol. ¡Corre al búnker! (Datos reales de la nasa)", new Color(1f, 0.2f, 0.12f));
                    }
                }
            }
            else
            {
                ActualizarTextoNASA("Sin conexión con NASA DONKI (Modo offline)", Color.yellow);
            }
        }
    }

    void ActualizarTextoNASA(string mensaje, Color color)
    {
        if (nasaAlertText != null)
        {
            nasaAlertText.text = mensaje;
            nasaAlertText.color = color;
        }
    }
}