using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
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
    private StateMachine stateMachine;

    [SerializeField] float moveSpeed = 5f;

    [SerializeField] public Vector3 lastSeenPosition;
    private bool isChasingPlayer = false;
    private float destinationOfLastSeenPlayer = 0.5f;

    [SerializeField] public GameManager gM;
    [SerializeField] private List<Transform> Path;

    public bool playerHeared = false;

    private void Awake()
    {
        stateMachine = new StateMachine();
    }

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

        stateMachine.ChangeState(new PatrolState(stateMachine, animator));

        animator.SetBool("Patroll", true);
    }

    void Update()
    {
        RayCastLogic();
        stateMachine.Update();
        //if (isChasingPlayer && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= destinationOfLastSeenPlayer)
        //{
        //    isChasingPlayer = false;
        //    StartCoroutine(SearchPlayerRoutine());
        //}
    }

    public void RayCastLogic()
    {
        Vector3 directionToEnemy = (enemy.transform.position - player.position).normalized;
        float distanceToEnemy = Vector3.Distance(player.position, enemy.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(player.position, directionToEnemy, out hit, raycastDistance, enemyLayer))
        {
            if (hit.collider.CompareTag("Wall") && !playerHeared)
            {
                if (animator.GetBool("Patroll"))
                {
                    animator.SetBool("Patroll", true);
                    animator.SetBool("Search", false);
                    animator.SetBool("Follow", false);
                }

                else if (!animator.GetBool("Patroll"))
                {
                    animator.SetBool("Search", true);
                    animator.SetBool("Follow", false);
                    animator.SetBool("Patroll", false);
                }

                else if (animator.GetBool("Follow"))
                {
                    animator.SetBool("Search", true);
                    animator.SetBool("Follow", false);
                    animator.SetBool("Patroll", false);
                }

                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.red);
            }

            else if (hit.collider.gameObject == this.gameObject && !playerHeared)
            {
                if (animator.GetBool("Patroll"))
                {
                    animator.SetBool("Patroll", true);
                    animator.SetBool("Search", false);
                    animator.SetBool("Follow", false);
                }

                else if (!animator.GetBool("Patroll"))
                {
                    animator.SetBool("Search", true);
                    animator.SetBool("Follow", false);
                    animator.SetBool("Patroll", false);
                }

                else if (animator.GetBool("Follow"))
                {
                    animator.SetBool("Search", true);
                    animator.SetBool("Follow", false);
                    animator.SetBool("Patroll", false);
                }

                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.blue);
            }

            else if (hit.collider.gameObject == visionCone)
            {
                animator.SetBool("Follow", true);
                animator.SetBool("Patroll", false);
                animator.SetBool("Search", false);

                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.green);
            }

            else if (!playerHeared)
            {
                if (animator.GetBool("Patroll"))
                {
                    animator.SetBool("Patroll", true);
                    animator.SetBool("Search", false);
                    animator.SetBool("Follow", false);
                }

                else if (!animator.GetBool("Patroll"))
                {
                    animator.SetBool("Search", true);
                    animator.SetBool("Follow", false);
                    animator.SetBool("Patroll", false);
                }

                else if (animator.GetBool("Follow"))
                {
                    animator.SetBool("Search", true);
                    animator.SetBool("Follow", false);
                    animator.SetBool("Patroll", false);
                }

                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.yellow);
            }
        }
    }

    //public void FollowPlayer()
    //{
    //    patrolAccess.TogglePatrol(false);//detiene la patrulla
    //    navMeshAgent.SetDestination(lastSeenPosition);
    //    isChasingPlayer = true;
    //    StopAllCoroutines();
    //}

    //IEnumerator SearchPlayerRoutine()
    //{
    //    yield return RotateRight();
    //    yield return new WaitForSeconds(1f);
    //    yield return RotateLeft();
    //    yield return new WaitForSeconds(1f);
    //    yield return RotateGoBack();


    //    // Después de buscar, llama al método reactivar la patrulla de patrullar
    //    patrolAccess.TogglePatrol(true);
    //}

    //IEnumerator RotateRight()
    //{
    //    float totalRotation = 0f;
    //    float rotationSpeed = 150f;

    //    while (totalRotation < 160f)
    //    {
    //        float rotationStep = rotationSpeed * Time.deltaTime;
    //        transform.Rotate(0f, rotationStep, 0f);

    //        totalRotation += rotationStep;

    //        yield return null;
    //    }
    //}

    //IEnumerator RotateLeft()
    //{
    //    float totalRotation = 0f;
    //    float rotationSpeed = 150f;

    //    while (totalRotation > -320f)
    //    {
    //        float rotationStep = -rotationSpeed * Time.deltaTime;
    //        transform.Rotate(0f, rotationStep, 0f);

    //        totalRotation += rotationStep;

    //        yield return null;
    //    }
    //}

    //IEnumerator RotateGoBack()
    //{
    //    float totalRotation = 0f;
    //    float rotationSpeed = 150f;

    //    while (totalRotation < 160f)
    //    {
    //        float rotationStep = rotationSpeed * Time.deltaTime;
    //        transform.Rotate(0f, rotationStep, 0f);

    //        totalRotation += rotationStep;

    //        yield return null;
    //    }
    //}

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
            playerHeared = true;

            animator.SetBool("Search", false);
            animator.SetBool("Follow", true);
            animator.SetBool("Patroll", false);
        }
    }
}
