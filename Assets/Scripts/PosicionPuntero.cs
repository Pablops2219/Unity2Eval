using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PosicionPuntero : MonoBehaviour
{
    public Vector3 destino { get; set; }
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject sphere;
    [SerializeField] private Transform foregroundPosition;
    [SerializeField] private GameObject currentInteractable; // Objeto interactuable actual


    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private bool PerformRaycast(out RaycastHit hit)
    {
        hit = default;

        // Generar un rayo desde la posición del mouse
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Comprobar si el puntero está sobre la UI o si el raycast golpea algo
        if (!EventSystem.current.IsPointerOverGameObject() &&
            Physics.Raycast(ray, out hit))
        {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {

        PerformRaycast(out raycastHit);
        // Lógica de los interactuables
        HandleInteractables(raycastHit);

        // Detectar clic izquierdo para movimiento
        if (Input.GetMouseButtonDown(1))
        {
            HandleMovement();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("?");
            GameObject foregorundObject = raycastHit.collider.gameObject;

            // Comprobar si es interactuable
            if (foregorundObject.CompareTag("Interactuable"))
            {
                ShowInForeground(foregorundObject);
            }
        }
    }

    private void HandleInteractables(RaycastHit raycastHit)
    {
        // Resetear el highlight previo
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        // Usar el raycast modularizado para detectar objetos interactuables
        if (PerformRaycast(out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Interactuable"))
            {
                Outline outline = highlight.gameObject.GetComponent<Outline>() ??
                                  highlight.gameObject.AddComponent<Outline>();
                outline.enabled = true;
                highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                
                //Script interaction = highlight.gameObject.GetComponent<Script>() ??
                                  //highlight.gameObject.AddComponent<Script>();
            }
            else
            {
                highlight = null;
            }
        }
    }

    private void HandleMovement()
    {
        if (PerformRaycast(out raycastHit))
        {
            destino = raycastHit.point;
            sphere.transform.position = destino;
        }
    }
    
    private void ShowInForeground(GameObject interactable)
    {
        // Si ya hay un objeto en primer plano, devolverlo a su posición original
        if (currentInteractable != null)
        {
            currentInteractable.transform.parent = null;
        }

        // Guardar el nuevo objeto interactuable actual
        currentInteractable = interactable;

        // Mover el objeto a la posición en primer plano
        interactable.transform.parent = foregroundPosition; // Vincular al espacio en primer plano
        interactable.transform.localPosition = Vector3.zero; // Centrar en la posición
        interactable.transform.localRotation = Quaternion.identity; // Reiniciar la rotación
    }
}