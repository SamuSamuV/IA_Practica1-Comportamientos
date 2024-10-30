using System. Collections;
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

    private Vector3 lastSeenPosition;
    private bool isChasingPlayer = false;
    private float destinationOfLastSeenPlayer = 0.5f;

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

        if (isChasingPlayer && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= destinationOfLastSeenPlayer)
        {
            isChasingPlayer = false;
            OnReachedLastSeenPositionSEARCH();
        }
    }

    public void RayCastLogic()
    {
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

                lastSeenPosition = player.position;
                FollowPlayer();
            }

            else
            {
                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.yellow);
            }
        }
    }

    public void FollowPlayer()
    {
        navMeshAgent.SetDestination(lastSeenPosition);
        isChasingPlayer = true;
    }

    void OnReachedLastSeenPositionSEARCH()
    {
        StartCoroutine(GirarDer());
    }

    IEnumerator GirarDer()
    {
        yield return new WaitForSeconds(1f);

        float totalRotation = 0f;
        float rotationSpeed = 150f;

        while (totalRotation < 160f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotationStep, 0f);

            totalRotation += rotationStep;

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(GirarIzq());
    }

    IEnumerator GirarIzq()
    {
        float totalRotation = 0f;
        float rotationSpeed = 150f;

        while (totalRotation > -320f)
        {
            float rotationStep = -rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotationStep, 0f);

            totalRotation += rotationStep;

            yield return null;
        }

        StartCoroutine(GirarVolver());
    }

    IEnumerator GirarVolver()
    {
        yield return new WaitForSeconds(1f);

        float totalRotation = 0f;
        float rotationSpeed = 150f;

        while (totalRotation < 160f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotationStep, 0f);

            totalRotation += rotationStep;

            yield return null;
        }
    }
}
