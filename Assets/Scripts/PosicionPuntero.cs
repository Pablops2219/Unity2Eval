using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PosicionPuntero : MonoBehaviour
{
    public Vector3 destino { get; set; }
    public Vector3 hitPoint { get; set; }
    public Camera cam;
    public GameObject sphere;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProcessCursorInputs();
            destino = hitPoint;
            sphere.transform.position = destino;
        }

        if (Input.GetMouseButtonDown(1))
        {
            ProcessCursorInputs();
            //SpawnTerrainScanner(hitPoint);
        }
    }

    private void ProcessCursorInputs()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            hitPoint = hit.point;
        }
    }

    public GameObject terrainScannerPrefab;
    public float terrainScannerDuration;
    public float terrainScannerSize;

    public void SpawnTerrainScanner(Vector3 spawnPosTerrainCollider)
    {
        GameObject terrainScanner =
            Instantiate(this.terrainScannerPrefab, spawnPosTerrainCollider, Quaternion.identity) as GameObject;
        ParticleSystem terrainScannerPS = terrainScanner.transform.GetChild(0).GetComponent<ParticleSystem>();
        if (!terrainScannerPS.IsUnityNull())
        {
            var main = terrainScannerPS.main;
            main.startLifetime = terrainScannerDuration;
            main.startSize = terrainScannerSize;
        }
        else
        {
            Debug.Log("error");
        }
        Destroy(terrainScanner, terrainScannerDuration+1);
    }
}