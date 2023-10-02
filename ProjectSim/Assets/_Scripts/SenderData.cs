using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SenderData : MonoBehaviour
{

    public string serverURL = "https://tu-servidor.com/api"; // Reemplaza con la URL de tu servidor

    public void SendData(string name, string country, string date)
    {
        StartCoroutine(SendDataCoroutine(name, country, date));
    }

    private IEnumerator SendDataCoroutine(string name, string country, string date)
    {
        // Crear un formulario para los datos
        WWWForm form = new WWWForm();
        form.AddField("Name", name);
        form.AddField("Country", country);
        form.AddField("Date", date);

        // Crear una solicitud POST con el formulario
        UnityWebRequest www = UnityWebRequest.Post(serverURL, form);

        // Enviar la solicitud al servidor
        yield return www.SendWebRequest();

        // Verificar si hubo un error en la solicitud
        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Datos enviados con éxito al servidor.");
        }
        else
        {
            Debug.LogError("Error al enviar datos al servidor: " + www.error);
        }
    }

}
