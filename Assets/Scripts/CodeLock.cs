using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importar TextMeshPro
public class CodeLock : MonoBehaviour

{
    public GameObject panel; // Panel de ingreso de código
    public TMP_InputField inputField; // Campo donde se ingresa el código (TMP)
    public TMP_Text messageText; // Texto para mostrar mensajes (TMP)
    public string correctCode = "123456"; // Código correcto
    public Vector3 newPosition; // Nueva posición del objeto cuando el código sea correcto

    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E)) // Pulsar "E" para abrir el panel
        {
            panel.SetActive(true);
            inputField.text = ""; // Limpia el input al abrirlo
        }
    }

    public void CheckCode()
    {
        if (inputField.text == correctCode)
        {
            messageText.text = "¡Código correcto!";
            MoveObject(); // Mueve el objeto
            panel.SetActive(false); // Oculta el panel
        }
        else
        {
            messageText.text = "Código incorrecto. Intenta de nuevo.";
        }
    }

    void MoveObject()
    {
        transform.position = newPosition; // Mueve el objeto a la nueva posición
        Debug.Log("¡El objeto se ha movido!");
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
