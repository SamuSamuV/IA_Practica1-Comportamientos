using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Search : StateMachineBehaviour
{
    private bool isChasingPlayer = true;
    private NavMeshAgent navMeshAgent;
    private float destinationOfLastSeenPlayer = 0.5f;
    public Transform player;
    Enemy enemy;
    Follow follow;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isChasingPlayer = true;
        navMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = animator.gameObject.GetComponent<Enemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isChasingPlayer && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= destinationOfLastSeenPlayer)
        {
            enemy.StartCoroutine(SearchPlayerRoutine(animator));
            isChasingPlayer = false;
        }
    }

    IEnumerator SearchPlayerRoutine(Animator animator)
    {
        yield return RotateRight(animator);
        yield return new WaitForSeconds(1f);
        yield return RotateLeft(animator);
        yield return new WaitForSeconds(1f);
        yield return RotateGoBack(animator);

        animator.SetBool("Patroll", true);

        // Después de buscar, llama al método reactivar la patrulla de patrullar
        //patrolAccess.TogglePatrol(true);
    }

    IEnumerator RotateRight(Animator animator)
    {
        float totalRotation = 0f;
        float rotationSpeed = 150f;

        while (totalRotation < 160f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            animator.transform.Rotate(0f, rotationStep, 0f);

            totalRotation += rotationStep;

            yield return null;
        }
    }

    IEnumerator RotateLeft(Animator animator)
    {
        float totalRotation = 0f;
        float rotationSpeed = 150f;

        while (totalRotation > -320f)
        {
            float rotationStep = -rotationSpeed * Time.deltaTime;
            animator.transform.Rotate(0f, rotationStep, 0f);

            totalRotation += rotationStep;

            yield return null;
        }
    }

    IEnumerator RotateGoBack(Animator animator)
    {
        float totalRotation = 0f;
        float rotationSpeed = 150f;

        while (totalRotation < 160f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            animator.transform.Rotate(0f, rotationStep, 0f);

            totalRotation += rotationStep;

            yield return null;
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
