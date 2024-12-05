using UnityEngine;
using UnityEngine.AI;

public class SearchState : State
{
    private Animator animator;
    private Enemy enemy;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Vector3 lastSeenPosition;
    private bool isChasingPlayer = false;

    public SearchState(StateMachine stateMachine, Animator animator, Enemy enemy, NavMeshAgent navMeshAgent) : base(stateMachine)
    {
        this.animator = animator;
    }

    public override void Enter()
    {
        this.animator = animator;
        this.enemy = enemy;
        this.navMeshAgent = navMeshAgent;
        this.player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador al inicializar el estado.

        Debug.Log("Entering Search State");
    }

    public override void Execute()
    {
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
}
