using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NasaDonkiManager : MonoBehaviour
{
    [Header("Conexión NASA API")]
    // DEMO_KEY funciona para pruebas, pero puedes pedir tu clave gratuita en api.nasa.gov
    public string apiKey = "DEMO_KEY";
    public int daysToLookBack = 3;

    [Header("Referencias del Juego")]
    public RadiationSystem radiationSystem;

    [Header("Modo Demostración")]
    [Tooltip("Actívalo si hoy no hay tormentas solares reales y quieres probar la mecánica")]
    public bool forceStormSimulation = false;

    // Estructuras para traducir los datos JSON de la NASA a C#
    [Serializable]
    public class SolarFlare
    {
        public string flrID;
        public string beginTime;
        public string classType; // Clasificación: C (leve), M (moderada), X (extrema)
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
            Debug.LogWarning("MODO SIMULACIÓN: Tormenta solar activada manualmente.");
        }
        else
        {
            StartCoroutine(CheckSolarFlares());
        }
    }

    IEnumerator CheckSolarFlares()
    {
        // Calculamos el rango de fechas de los últimos días (formato YYYY-MM-DD)
        string endDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
        string startDate = DateTime.UtcNow.AddDays(-daysToLookBack).ToString("yyyy-MM-dd");

        string url = $"https://ccmc.gsfc.nasa.gov/DONKI-API/get/FLR?startDate={startDate}&endDate={endDate}";
        Debug.Log("Consultando clima espacial en NASA DONKI...");

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string rawJson = request.downloadHandler.text.Trim();

                // Si la respuesta está vacía o es "[]", no hubo llamaradas recientes
                if (string.IsNullOrEmpty(rawJson) || rawJson == "[]")
                {
                    radiationSystem.isSolarStormActive = false;
                    Debug.Log("Clima espacial despejado. No hay tormentas solares recientes.");
                }
                else
                {
                    // Envolvemos la lista JSON para que Unity pueda leerla
                    string wrappedJson = "{\"flares\":" + rawJson + "}";
                    SolarFlareList flareData = JsonUtility.FromJson<SolarFlareList>(wrappedJson);

                    if (flareData.flares != null && flareData.flares.Length > 0)
                    {
                        SolarFlare latest = flareData.flares[flareData.flares.Length - 1];
                        radiationSystem.isSolarStormActive = true;
                        Debug.LogWarning($"¡ALERTA NASA! Llamarada Solar Clase {latest.classType} detectada el {latest.beginTime}. ¡Ve al refugio!");
                    }
                }
            }
            else
            {
                Debug.LogError("Error conectando con la API de la NASA: " + request.error);
            }
        }
    }
}