using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SenderData : MonoBehaviour
{
    public string serverURL = "https://citmalumnes.upc.es/~davidbo5/ServerPhP.php"; // Reemplaza con la URL de tu servidor
    private bool startingSessionBool = true;
    private string startSessionTime;
    private string endSessionTime;

    private void OnEnable()
    {
        Simulator.OnNewPlayer += SendData;
        Simulator.OnNewSession += SendSessionTime;
        Simulator.OnEndSession += SendSessionTime;

    }
    private void OnDisable()
    {
        Simulator.OnNewPlayer -= SendData;
        Simulator.OnNewSession -= SendSessionTime;
        Simulator.OnEndSession -= SendSessionTime;

    }

    // -------------------------------------------------------------------------------------------------------------------- Send Player Data & Get ID
    public void SendData(string name, string country, string gender, int age, DateTime date)
    {
        StartCoroutine(SendUserDataCoroutine(name, country, gender, age, date));
    }

    private IEnumerator SendUserDataCoroutine(string name, string country, string gender, int age, DateTime date)
    {
        // Define un formato de fecha personalizado
        string formatoPersonalizado = "yyyy-MM-dd HH:mm:ss";

        // Convierte la fecha en una cadena con el formato personalizado
        string fechaFormateada = date.ToString(formatoPersonalizado);

        // Crear un formulario para los datos
        WWWForm form = new WWWForm();
        form.AddField("Name", name);
        form.AddField("Age", age);
        form.AddField("Gender", gender);
        form.AddField("Country", country);
        form.AddField("Date", fechaFormateada);

        // Crear una solicitud POST con el formulario
        UnityWebRequest www = UnityWebRequest.Post(serverURL, form);


        // Enviar la solicitud al servidor
        yield return www.SendWebRequest();

        // Verificar si hubo un error en la solicitud
        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Datos DEL USER enviados con exito al servidor.");

            string userId_String = www.downloadHandler.text;
            uint userId_uInt;

            if (uint.TryParse(userId_String, out userId_uInt))
            {
                // La conversión fue exitosa, y valorComoInt contiene el valor entero.
                CallbackEvents.OnAddPlayerCallback?.Invoke(userId_uInt);
            }
            else
            {
                // La conversión falló, puedes manejar el error aquí.
                Debug.Log(www.downloadHandler.text);
            }

        }
        else
        {
            Debug.LogError("Error al enviar datos DEL USER al servidor: " + www.error);
        }
    }

    // --------------------------------------------------------------------------------------------------------------------
    // -------------------------------------------------------------------------------------------------------------------- Send StartSessionTime
    public void SendSessionTime(DateTime sessionTime)
    {
        StartCoroutine(SendSessionTimeStamp(sessionTime));
    }

    private IEnumerator SendSessionTimeStamp(DateTime sessionTime)
    {
        // Define un formato de fecha personalizado
        string formatoPersonalizado = "yyyy-MM-dd HH:mm:ss";

        // Convierte la fecha en una cadena con el formato personalizado
        string fechaFormateada = sessionTime.ToString(formatoPersonalizado);

        // Crear un formulario para los datos
        WWWForm form = new WWWForm();

        if (startingSessionBool)
        {
            startSessionTime = fechaFormateada;
            startingSessionBool = false;
        }

        else
        {
            endSessionTime = fechaFormateada;
            
            form.AddField("startSessionTime", startSessionTime);
            form.AddField("endSessionTime", endSessionTime);

            startingSessionBool = true;
        }

        // Crear una solicitud POST con el formulario
        UnityWebRequest www = UnityWebRequest.Post(serverURL, form);


        // Enviar la solicitud al servidor
        yield return www.SendWebRequest();

        // Verificar si hubo un error en la solicitud
        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Datos DEL INICIO DE LA SESION enviados con exito al servidor.");

            //string userId_String = www.downloadHandler.text;
            //uint userId_uInt;

            //if (uint.TryParse(userId_String, out userId_uInt))
            //{
            //    // La conversión fue exitosa, y valorComoInt contiene el valor entero.
            //    CallbackEvents.OnAddPlayerCallback?.Invoke(userId_uInt);
            //}
            //else
            //{
            //    // La conversión falló, puedes manejar el error aquí.
            //    Debug.Log(www.downloadHandler.text);
            //}

        }
        else
        {
            Debug.LogError("Error al enviar datos DEL INICIO DE LA SESION al servidor: " + www.error);
        }
    }
}
    // --------------------------------------------------------------------------------------------------------------------
    // -------------------------------------------------------------------------------------------------------------------- Send EndSessionTim