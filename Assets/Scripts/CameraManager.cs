using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public List<CinemachineVirtualCamera> interactuableCameras;

    ////public void OnTriggerEnter(Collider other)
    ////{
    ////    //Debug.Log($"Triggered by: {other.name}"); 

    ////    if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
    ////    {
    ////        virtualCamera.Priority = 10; // Activa esta cámara (mayor prioridad)
    ////    }
    ////}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        virtualCamera.Priority = 0; // Desactiva esta cámara (menor prioridad)
    //    }
    //}

    public void FocusCameraOnInteractable(int interactuableID)
    {
        interactuableCameras[interactuableID].Priority = 100; // Activa esta cámara (mayor prioridad)
    }

    public void UnfocusCameraOnInteractable(int interactuableID)
    {
        interactuableCameras[interactuableID].Priority = 0; // Activa esta cámara (mayor prioridad)
    }

    private void Update()
    {
        
    }
}
