using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : StateMachineBehaviour
{
    [SerializeField] float speed;
    public Transform player;
    Enemy enemy;
    //private Patrol patrolAccess; //referencia al script Patrullar
    private NavMeshAgent navMeshAgent;
    [SerializeField] public Vector3 lastSeenPosition;
    private bool isChasingPlayer = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = animator.gameObject.GetComponent<Enemy>();
        navMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>();
        //patrolAccess = animator.gameObject.GetComponent<Patrol>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lastSeenPosition = player.position;

        if (!enemy.playerHeared)
        {
            FollowPlayer();
        }

        else
        {
            Debug.Log("MACACO");
            navMeshAgent.SetDestination(lastSeenPosition);

            enemy.playerHeared = false;

            animator.SetBool("Patroll", false);
            animator.SetBool("Search", true);
            animator.SetBool("Follow", false);
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

    public void FollowPlayer()
    {
        //patrolAccess.TogglePatrol(false);//detiene la patrulla
        navMeshAgent.SetDestination(lastSeenPosition);
        isChasingPlayer = true;
        //StopAllCoroutines();
    }
}
