using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera; // Cámara virtual asignada a esta zona

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by: {other.name}"); // Verifica qué objeto activa el trigger

        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
        {
            virtualCamera.Priority = 10; // Activa esta cámara (mayor prioridad)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.Priority = 0; // Desactiva esta cámara (menor prioridad)
        }
    }
}
