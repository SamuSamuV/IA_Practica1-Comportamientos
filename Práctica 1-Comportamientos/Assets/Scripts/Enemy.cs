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

    [SerializeField] public Vector3 lastSeenPosition;
    private bool isChasingPlayer = false;
    private float destinationOfLastSeenPlayer = 0.5f;

    [SerializeField] public GameManager gM;

    private Patrullar accesoPatrullar; //referencia al script Patrullar

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
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        accesoPatrullar = GetComponent<Patrullar>();
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
        accesoPatrullar.TogglePatrulla(false);//detiene la patrulla
        navMeshAgent.SetDestination(lastSeenPosition);
        isChasingPlayer = true;
        StopAllCoroutines();
    }

    IEnumerator SearchPlayerRoutine()
    {
        yield return GirarDer();
        yield return new WaitForSeconds(1f);
        yield return GirarIzq();
        yield return new WaitForSeconds(1f);
        yield return GirarVolver();


        // Después de buscar, llama al método reactivar la patrulla de patrullar
        accesoPatrullar.TogglePatrulla(true);
    }

    IEnumerator GirarDer()
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
    }

    IEnumerator GirarVolver()
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
