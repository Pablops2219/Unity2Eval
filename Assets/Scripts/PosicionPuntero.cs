using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PosicionPuntero : MonoBehaviour
{
    public Vector3 destino { get; set; }
    public Camera cam;
    public GameObject sphere;

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
        // Lógica de highlight
        HandleHighlight();

        // Detectar clic izquierdo para movimiento
        if (Input.GetMouseButtonDown(0))
        {
            HandleMovement();
        }
    }

    private void HandleHighlight()
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
            }
            else
            {
                highlight = null;
            }
        }
    }

    private void HandleMovement()
    {
        // Usar el raycast modularizado para obtener la posición de impacto
        if (PerformRaycast(out raycastHit))
        {
            destino = raycastHit.point;
            sphere.transform.position = destino;
        }
    }
}