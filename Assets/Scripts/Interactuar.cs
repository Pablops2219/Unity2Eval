using UnityEngine;

public class Interactuar : MonoBehaviour
{
    public float distanciaInteraccion = 3f; // Distancia máxima para interactuar
    public KeyCode teclaInteraccion = KeyCode.E; // Tecla para interactuar
    [SerializeField] private CameraManager camManager; // Asignar desde el Inspector
    private GameObject objetoInteractuable; // Objeto con el que se puede interactuar

    void Start()
    {
        // Buscar automáticamente el CameraManager si no está asignado
        if (camManager == null)
        {
            camManager = FindObjectOfType<CameraManager>();
            if (camManager == null)
            {
                Debug.LogError("No se encontró un CameraManager en la escena.");
            }
        }
    }

    void Update()
    {
        // Buscar objetos cercanos con el tag "Interactuable"
        BuscarObjetoInteractuable();

        // Si hay un objeto interactuable cerca y se presiona la tecla de interacción
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

            // Si el objeto está dentro del rango de interacción
            if (distancia <= distanciaInteraccion)
            {
                // Asignar el objeto como interactuable
                objetoInteractuable = obj;
                break; // Salir del bucle después de encontrar el primer objeto cercano
            }
        }
    }

    void _Interactuar()
    {
        if (objetoInteractuable != null)
        {
            // Obtener el nombre del objeto interactuable
            string nombreObjeto = objetoInteractuable.name;

            // Extraer el último número del nombre
            string ultimoNumero = "";
            for (int i = nombreObjeto.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(nombreObjeto[i]))
                {
                    ultimoNumero = nombreObjeto[i] + ultimoNumero; // Agregar el dígito al principio
                }
                else
                {
                    break; // Detenerse cuando se encuentre un carácter no numérico
                }
            }

            // Convertir el último número a entero (si es necesario)
            int ultimoNumeroEntero = int.Parse(ultimoNumero);

            // Aquí puedes agregar la lógica de interacción
            Debug.Log("Interactuando con: " + nombreObjeto);

            // Ejemplo: Desactivar el objeto después de interactuar
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
                    camManager.FocusCameraOnInteractable(ultimoNumeroEntero); // Pasar el último número como parámetro

                }

            }
            else
            {
                Debug.LogError("camManager no está asignado.");
            }
        }
    }

    // Opcional: Dibujar un gizmo en el editor para visualizar el rango de interacción
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaInteraccion);
    }
}