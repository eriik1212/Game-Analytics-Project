using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SenderData : MonoBehaviour
{
    public string serverURL = "https://citmalumnes.upc.es/~davidbo5/HelloWorldPhP.php"; // Reemplaza con la URL de tu servidor

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
        // Define un formato de fecha personalizado
        string formatoPersonalizado = "yyyy-MM-dd HH:mm:ss"; // Por ejemplo, "05/10/2023 14:30:00"

        // Convierte la fecha en una cadena con el formato personalizado
        string fechaFormateada = date.ToString(formatoPersonalizado);

        // Crear un formulario para los datos
        WWWForm form = new WWWForm();
        form.AddField("Name", name);
        form.AddField("Country", country);
        form.AddField("Date", fechaFormateada);

        // Crear una solicitud POST con el formulario
        UnityWebRequest www = UnityWebRequest.Post(serverURL, form);


        // Enviar la solicitud al servidor
        yield return www.SendWebRequest();

        // Verificar si hubo un error en la solicitud
        if (www.result == UnityWebRequest.Result.Success)
        {
            uint userId = 2; // !!!!! TEMPORAL - La ID aquesta ens la inventem

            CallbackEvents.OnAddPlayerCallback?.Invoke(userId);

            Debug.Log("Datos enviados con �xito al servidor.");
            Debug.Log(www.downloadHandler.text);

        }
        else
        {
            // ----------------------------------------------------------------------- TEMPORAL - Aix� ha de passar nom�s quan es SUCCESS
            //
            //    uint userId = 2; // !!!!! TEMPORAL - La ID aquesta ens la inventem
            //
            //
            //    CallbackEvents.OnAddPlayerCallback?.Invoke(userId);
            //
            // -----------------------------------------------------------

            Debug.LogError("Error al enviar datos al servidor: " + www.error);
        }
    }

}
