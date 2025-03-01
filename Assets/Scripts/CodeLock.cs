using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importar TextMeshPro

public class CodeLock : MonoBehaviour
{
    public GameObject panel; // Panel de ingreso de código
    public TMP_InputField[] inputFields = new TMP_InputField[4]; // 4 campos de texto
    public TMP_Text messageText; // Texto para mostrar mensajes (TMP)
    public Button submitButton; // Botón para enviar el código

    private int[] correctNumbers = { 12, 34, 56, 78 }; // Números correctos para cada campo

    private bool isPlayerNear = false;

    void Start()
    {
        // Desactivar el panel al inicio
        panel.SetActive(false);

        // Asignar el método CheckCode al botón
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(CheckCode);
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E)) // Pulsar "E" para abrir el panel
        {
            panel.SetActive(true);
            Cursor.lockState = CursorLockMode.None; // Libera el cursor
            Cursor.visible = true; // Hace visible el cursor

            // Limpiar los campos de texto
            foreach (var field in inputFields)
            {
                field.text = "";
            }

            StartCoroutine(ActivateInputField()); // Activa el primer InputField automáticamente
        }
    }

    IEnumerator ActivateInputField()
    {
        yield return new WaitForSeconds(0.1f); // Espera un pequeño tiempo
        if (inputFields.Length > 0)
        {
            inputFields[0].Select(); // Selecciona el primer campo de texto
            inputFields[0].ActivateInputField(); // Lo activa para recibir entrada
        }
    }

    public void CheckCode()
    {
        bool isCorrect = true;

        for (int i = 0; i < inputFields.Length; i++)
        {
            if (!int.TryParse(inputFields[i].text, out int enteredNumber) || enteredNumber != correctNumbers[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
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