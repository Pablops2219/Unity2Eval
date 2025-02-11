using UnityEngine;

public class Interactuar : MonoBehaviour
{
    public float distanciaInteraccion = 3f; // Distancia m�xima para interactuar
    public KeyCode teclaInteraccion = KeyCode.E; // Tecla para interactuar
    [SerializeField] private CameraManager camManager; // Asignar desde el Inspector
    private GameObject objetoInteractuable; // Objeto con el que se puede interactuar

    void Start()
    {
        // Buscar autom�ticamente el CameraManager si no est� asignado
        if (camManager == null)
        {
            camManager = FindObjectOfType<CameraManager>();
            if (camManager == null)
            {
                Debug.LogError("No se encontr� un CameraManager en la escena.");
            }
        }
    }

    void Update()
    {
        // Buscar objetos cercanos con el tag "Interactuable"
        BuscarObjetoInteractuable();

        // Si hay un objeto interactuable cerca y se presiona la tecla de interacci�n
        if (objetoInteractuable != null && Input.GetKeyDown(teclaInteraccion))
        {
            _Interactuar();
        }
    }

    void BuscarObjetoInteractuable()
    {
        // Reiniciar la referencia al objeto interactuable
        objetoInteractuable = null;

        // Buscar todos los objetos con el tag "Interactuable"
        GameObject[] interactuables = GameObject.FindGameObjectsWithTag("Interactuable");

        // Recorrer los objetos encontrados
        foreach (GameObject obj in interactuables)
        {
            // Calcular la distancia entre el jugador y el objeto
            float distancia = Vector3.Distance(transform.position, obj.transform.position);

            // Si el objeto est� dentro del rango de interacci�n
            if (distancia <= distanciaInteraccion)
            {
                // Asignar el objeto como interactuable
                objetoInteractuable = obj;
                break; // Salir del bucle despu�s de encontrar el primer objeto cercano
            }
        }
    }

    void _Interactuar()
    {
        if (objetoInteractuable != null)
        {
            // Obtener el nombre del objeto interactuable
            string nombreObjeto = objetoInteractuable.name;

            // Extraer el �ltimo n�mero del nombre
            string ultimoNumero = "";
            for (int i = nombreObjeto.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(nombreObjeto[i]))
                {
                    ultimoNumero = nombreObjeto[i] + ultimoNumero; // Agregar el d�gito al principio
                }
                else
                {
                    break; // Detenerse cuando se encuentre un car�cter no num�rico
                }
            }

            // Convertir el �ltimo n�mero a entero (si es necesario)
            int ultimoNumeroEntero = int.Parse(ultimoNumero);

            // Aqu� puedes agregar la l�gica de interacci�n
            Debug.Log("Interactuando con: " + nombreObjeto);

            // Ejemplo: Desactivar el objeto despu�s de interactuar
            //objetoInteractuable.SetActive(false);

            // Verificar que camManager no sea null antes de usarlo
            if (camManager != null)
            {
                if (camManager.interactuableCameras[ultimoNumeroEntero].Priority == 100)
                {
                    camManager.UnfocusCameraOnInteractable(ultimoNumeroEntero);
                }
                else
                {
                    camManager.FocusCameraOnInteractable(ultimoNumeroEntero); // Pasar el �ltimo n�mero como par�metro

                }

            }
            else
            {
                Debug.LogError("camManager no est� asignado.");
            }
        }
    }

    // Opcional: Dibujar un gizmo en el editor para visualizar el rango de interacci�n
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaInteraccion);
    }
}