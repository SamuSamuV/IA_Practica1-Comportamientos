using UnityEngine;
using UnityEngine.AI;

public class FollowState : State
{
    private Animator animator;
    private Enemy enemy;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Vector3 lastSeenPosition;
    private bool isChasingPlayer = false;

    public FollowState(StateMachine stateMachine, Animator animator, Enemy enemy, NavMeshAgent navMeshAgent) : base(stateMachine)
    {
        this.animator = animator;
        this.enemy = enemy;
        this.navMeshAgent = navMeshAgent;
        this.lastSeenPosition = Vector3.zero;
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Enter()
    {
        Debug.Log("Entering Follow State");
        isChasingPlayer = true;
    }

    public override void Execute()
    {
        enemy.lastSeenPosition = player.position;

        if (!this.enemy.playerHeared)
        {
            FollowPlayer();
        }

        else
        {
            Debug.Log("Player Lost - Going to Last Known Position");
            navMeshAgent.SetDestination(enemy.lastSeenPosition);
            enemy.playerHeared = false;

            animator.SetBool("Patroll", false);
            animator.SetBool("Search", true);
            animator.SetBool("Follow", false);
        }

        if (animator.GetBool("Patroll"))
        {
            stateMachine.ChangeState(new PatrolState(stateMachine, animator, enemy, navMeshAgent));
        }
        else if (animator.GetBool("Search"))
        {
            stateMachine.ChangeState(new SearchState(stateMachine, animator, enemy, navMeshAgent));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Follow State");
        isChasingPlayer = false;
    }

    private void FollowPlayer()
    {
        navMeshAgent.SetDestination(player.position);
        isChasingPlayer = true;
    }
}
