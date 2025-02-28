using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importar TextMeshPro

public class CodeLock : MonoBehaviour
{
    public GameObject panel; // Panel de ingreso de código
    public TMP_InputField inputField; // Campo donde se ingresa el código (TMP)
    public TMP_Text messageText; // Texto para mostrar mensajes (TMP)
    public string correctCode = "123456"; // Código correcto

    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E)) // Pulsar "E" para abrir el panel
        {
            panel.SetActive(true);
            Cursor.lockState = CursorLockMode.None; // Libera el cursor
            Cursor.visible = true; // Hace visible el cursor
            inputField.text = ""; // Limpia el input
            StartCoroutine(ActivateInputField()); // Activa el InputField automáticamente
        }

        if (panel.activeSelf) // Pulsar Enter para activar el InputField
        {
        }
    }

    IEnumerator ActivateInputField()
    {
        yield return new WaitForSeconds(0.1f); // Espera un pequeño tiempo
        inputField.gameObject.SetActive(false); // Desactiva temporalmente el inputField
        inputField.gameObject.SetActive(true);  // Lo vuelve a activar
        inputField.Select(); // Selecciona el campo de texto
        inputField.ActivateInputField(); // Lo activa para recibir entrada
    }

    public void CheckCode()
    {
        if (inputField.text == correctCode)
        {
            messageText.text = "¡Código correcto!";
            panel.SetActive(false); // Oculta el panel
            Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor de nuevo
            Cursor.visible = false; // Oculta el cursor
            gameObject.SetActive(false); // Desactiva el objeto que tiene este script
        }
        else
        {
            messageText.text = "Código incorrecto. Intenta de nuevo.";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            panel.SetActive(false); // Oculta el panel si el jugador se aleja
        }
    }
}
