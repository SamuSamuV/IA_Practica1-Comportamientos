using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SearchState : State
{
    private Animator animator;
    private Enemy enemy;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Vector3 lastSeenPosition;
    private bool isChasingPlayer = false;
    private float destinationOfLastSeenPlayer = 0.5f;

    public SearchState(StateMachine stateMachine, Animator animator, Enemy enemy, NavMeshAgent navMeshAgent) : base(stateMachine)
    {
        this.animator = animator;
        this.enemy = enemy;
        this.navMeshAgent = navMeshAgent;
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Enter()
    {
        isChasingPlayer = true;
        Debug.Log("Entering Search State");
    }

    public override void Execute()
    {
        if (isChasingPlayer && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= destinationOfLastSeenPlayer)
        {
            enemy.StartCoroutine(SearchPlayerRoutine());
            isChasingPlayer = false;
        }

        if (animator.GetBool("Follow"))
        {
            stateMachine.ChangeState(new FollowState(stateMachine, animator, enemy, navMeshAgent));
        }
        else if (animator.GetBool("Patroll"))
        {
            stateMachine.ChangeState(new PatrolState(stateMachine, animator, enemy, navMeshAgent));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Search State");
    }

    IEnumerator SearchPlayerRoutine()
    {
        yield return RotateRight();
        yield return new WaitForSeconds(1f);
        yield return RotateLeft();
        yield return new WaitForSeconds(1f);
        yield return RotateGoBack();

        animator.SetBool("Patroll", true);
    }

    IEnumerator RotateRight()
    {
        float totalRotation = 0f;
        float rotationSpeed = 150f;

        while (totalRotation < 160f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            enemy.transform.Rotate(0f, rotationStep, 0f);

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
            enemy.transform.Rotate(0f, rotationStep, 0f);

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
            enemy.transform.Rotate(0f, rotationStep, 0f);

            totalRotation += rotationStep;

            yield return null;
        }
    }
}
