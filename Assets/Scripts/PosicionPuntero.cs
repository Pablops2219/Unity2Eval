using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class PosicionPuntero : MonoBehaviour
{
    public Vector3 destino { get; set; }
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject sphere;
    [SerializeField] private Transform foregroundPosition;
    [SerializeField] private GameObject currentInteractable; // Objeto interactuable actual


    private CinemachineBrain cinemachineBrain;
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Obtener el CinemachineBrain desde la cámara principal
        cinemachineBrain = cam.GetComponent<CinemachineBrain>();

        if (cinemachineBrain == null)
        {
            Debug.LogError("No se encontró CinemachineBrain en la cámara principal.");
        }
    }

    public CinemachineVirtualCameraBase GetActiveVirtualCamera()
    {
        if (cinemachineBrain != null)
        {
            // Obtener la cámara virtual activa
            CinemachineVirtualCameraBase activeVirtualCamera =
                cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCameraBase;

            if (activeVirtualCamera != null)
            {
                //Debug.Log($"Cámara virtual activa: {activeVirtualCamera.Name}");
                return activeVirtualCamera;
            }
        }

        Debug.LogWarning("No hay ninguna cámara virtual activa.");
        return null;
    }

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
            MoveToClick();
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject foregroundObject = raycastHit.collider.gameObject;

            // Comprobar si es interactuable
            if (foregroundObject.CompareTag("Interactuable"))
            {
                float distance = Vector3.Distance(player.transform.position, foregroundObject.transform.position);

                if (distance <= 5f)
                {
                    ShowInForeground(foregroundObject);
                }
                else
                {
                    MoveToClick();
                    
                }
            }

            if (foregroundObject.CompareTag("Clon Interactuable"))
            {
                ShowInForeground(foregroundObject);
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
            }
            else
            {
                highlight = null;
            }
        }
    }

    private void MoveToClick()
    {
        if (PerformRaycast(out raycastHit))
        {
            destino = raycastHit.point;
            sphere.transform.position = destino;
        }
    }

    bool showingInteractable = false;

    public bool isShowingInteractable()
    {
        return showingInteractable;
    }

    private void ShowInForeground(GameObject interactable)
    {
        if (showingInteractable)
        {
            Destroy(currentInteractable);
            showingInteractable = false;
            return;
        }
        
        CinemachineVirtualCameraBase activeCamera = GetActiveVirtualCamera();
        if (activeCamera == null)
        {
            Debug.LogError("No se encontró una cámara activa.");
            return;
        }
        
        interactable.GetComponent<Outline>().enabled = false;
        GameObject clone = Instantiate(interactable);
        clone.tag = "Clon Interactuable";

        // Ajustar la posición del clon frente a la cámara activa
        Vector3 cameraForward = activeCamera.transform.forward;
        Vector3 cameraPosition = activeCamera.transform.position;
        Vector3 offset = cameraForward * 2.0f;
        clone.transform.position = cameraPosition + offset;

        //clone.transform.LookAt(activeCamera.transform);
        //clone.transform.Rotate(0, 180, 0); 

        clone.transform.localScale = interactable.transform.localScale;
        currentInteractable = clone;
        showingInteractable = true;
    }
}