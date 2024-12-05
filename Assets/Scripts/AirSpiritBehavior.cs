using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AirSpiritBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform player;
    private NavMeshAgent agent;
    private float minDistance = 10f;
    private float rotationSpeed = 1.0f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; 

        agent.SetDestination(player.position);

        if (Vector3.Distance(agent.transform.position, player.position) <= minDistance)
        {
            agent.isStopped = true;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
            agent.isStopped = false;
    }
}
