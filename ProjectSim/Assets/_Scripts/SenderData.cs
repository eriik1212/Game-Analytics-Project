using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SenderData : MonoBehaviour
{
    public string serverURL = "https://tu-servidor.com/api"; // Reemplaza con la URL de tu servidor

    private void OnEnable()
    {
        Simulator.OnNewPlayer += SendData;   
    }
    private void OnDisable()
    {
        Simulator.OnNewPlayer -= SendData;   
    }

    public void SendData(string name, string country, DateTime date)
    {
        StartCoroutine(SendUserDataCoroutine(name, country, date));
    }

    private IEnumerator SendUserDataCoroutine(string name, string country, DateTime date)
    {
        // Crear un formulario para los datos
        WWWForm form = new WWWForm();
        form.AddField("Name", name);
        form.AddField("Country", country);
        form.AddField("Date", date.ToString());

        // Crear una solicitud POST con el formulario
        UnityWebRequest www = UnityWebRequest.Post(serverURL, form);

        // Enviar la solicitud al servidor
        yield return www.SendWebRequest();

        // Verificar si hubo un error en la solicitud
        if (www.result == UnityWebRequest.Result.Success)
        {
            uint userId = 2; // !!!!! TEMPORAL - La ID aquesta ens la inventem

            CallbackEvents.OnAddPlayerCallback?.Invoke(userId);

            Debug.Log("Datos enviados con éxito al servidor.");
        }
        else
        {
            // ----------------------------------------------------------------------- TEMPORAL - Això ha de passar només quan es SUCCESS
            //
                uint userId = 2; // !!!!! TEMPORAL - La ID aquesta ens la inventem
            //
            //
                CallbackEvents.OnAddPlayerCallback?.Invoke(userId);
            //
            // -----------------------------------------------------------

            Debug.LogError("Error al enviar datos al servidor: " + www.error);
        }
    }

}
