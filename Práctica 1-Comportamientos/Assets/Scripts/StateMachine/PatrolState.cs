using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private Animator animator;
    private Enemy enemy;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Vector3 lastSeenPosition;
    private bool isChasingPlayer = false;

    public PatrolState(StateMachine stateMachine, Animator animator, Enemy enemy, NavMeshAgent navMeshAgent) : base(stateMachine)
    {
        this.animator = animator;
        this.enemy = enemy;
        this.navMeshAgent = navMeshAgent;
        this.player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador al inicializar el estado.
    }

    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    public override void Execute()
    {
        if (animator.GetBool("Follow"))
        {
            stateMachine.ChangeState(new FollowState(stateMachine, animator, enemy, navMeshAgent));
        }
        else if (animator.GetBool("Search"))
        {
            stateMachine.ChangeState(new SearchState(stateMachine, animator, enemy, navMeshAgent));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Patrol State");
    }
}
