using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI; 

public class Enemy : MonoBehaviour
{
    public LayerMask enemyLayer;
    public LayerMask wallLayer;
    public float raycastDistance = 100f;
    public Transform player;
    public Transform[] enemies;
    public GameObject enemy;
    public GameObject conoVision;

    private NavMeshAgent navMeshAgent;

    [SerializeField] float moveSpeed = 5f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        enemies = new Transform[enemyObjects.Length];
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemies[i] = enemyObjects[i].transform;
        }

        enemy = gameObject;
        conoVision = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        RayCastLogic();
    }

    public void RayCastLogic()
    {
        //for (int i = 0; i < enemies.Length; i++) //Este bucle for se usaba para que cada enemigo pudiese saber la posición de los otros enemigos

        Vector3 directionToEnemy = (enemy.transform.position - player.position).normalized;
        float distanceToEnemy = Vector3.Distance(player.position, enemy.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(player.position, directionToEnemy, out hit, raycastDistance, enemyLayer))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.red);
            }

            else if (hit.collider.gameObject == this.gameObject)
            {
                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.blue);
            }

            else if (hit.collider.gameObject == conoVision)
            {
                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.green);
                FollowPlayer();
            }
        }
    }

    public void FollowPlayer()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}