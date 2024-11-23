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
    public GameObject visionCone;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    [SerializeField] float moveSpeed = 5f;

    [SerializeField] public Vector3 lastSeenPosition;
    private bool isChasingPlayer = false;
    private float destinationOfLastSeenPlayer = 0.5f;

    [SerializeField] public GameManager gM;

    private Patrol patrolAccess; //referencia al script Patrullar

    void Start()
    {
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new Transform[enemyObjects.Length];
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemies[i] = enemyObjects[i].transform;
        }

        enemy = gameObject;
        visionCone = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        patrolAccess = GetComponent<Patrol>();
    }

    void Update()
    {
        RayCastLogic();

        if (isChasingPlayer && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= destinationOfLastSeenPlayer)
        {
            isChasingPlayer = false;
            StartCoroutine(SearchPlayerRoutine());
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
                animator.SetBool("IsWatched", false);

                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.red);
            }

            else if (hit.collider.gameObject == this.gameObject)
            {
                animator.SetBool("IsWatched", false);

                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.blue);
            }

            else if (hit.collider.gameObject == visionCone)
            {
                animator.SetBool("IsWatched", true);

                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.green);

                lastSeenPosition = player.position;
                FollowPlayer();
            }

            else
            {
                animator.SetBool("IsWatched", false);

                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.yellow);
            }
        }
    }

    public void FollowPlayer()
    {
        patrolAccess.TogglePatrol(false);//detiene la patrulla
        navMeshAgent.SetDestination(lastSeenPosition);
        isChasingPlayer = true;
        StopAllCoroutines();
    }

    IEnumerator SearchPlayerRoutine()
    {
        yield return RotateRight();
        yield return new WaitForSeconds(1f);
        yield return RotateLeft();
        yield return new WaitForSeconds(1f);
        yield return RotateGoBack();


        // Después de buscar, llama al método reactivar la patrulla de patrullar
        patrolAccess.TogglePatrol(true);
    }

    IEnumerator RotateRight()
    {
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

    IEnumerator RotateLeft()
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
    }

    IEnumerator RotateGoBack()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gM.SetActiveLosePanel();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ColliderSonido"))
        {
            lastSeenPosition = player.position;
            FollowPlayer();
        }
    }
}
