using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private PosicionPuntero punteroScript;
    private NavMeshAgent agent;
    public GameObject GameManager;
    private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    void Start()
    {
        punteroScript = GameManager.GetComponent<PosicionPuntero>();
        _animator = this.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //StartCoroutine(NoisySteps());
    }

    void Update()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            // The agent is moving
            _animator.SetBool(IsMoving, true);
        }
        else
        {
            // The agent is not moving
            _animator.SetBool(IsMoving, false);
        }
        agent.SetDestination(punteroScript.destino);
    }

    IEnumerator NoisySteps()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if(punteroScript != null)
            {
                punteroScript.SpawnTerrainScanner(this.transform.position);
            }
        }
    }
}