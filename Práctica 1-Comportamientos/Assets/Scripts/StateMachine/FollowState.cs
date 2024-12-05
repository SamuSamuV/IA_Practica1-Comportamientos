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
        this.player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador al inicializar el estado.
    }

    public override void Enter()
    {
        Debug.Log("Entering Follow State");
        isChasingPlayer = true;
    }

    public override void Execute()
    {
        // Actualiza la última posición conocida del jugador.
        lastSeenPosition = player.position;

        if (!enemy.playerHeared)
        {
            FollowPlayer(); // Persigue al jugador si está en rango de detección.
        }
        else
        {
            Debug.Log("Player Lost - Going to Last Known Position");
            navMeshAgent.SetDestination(lastSeenPosition);
            enemy.playerHeared = false;

            // Cambia a buscar después de llegar a la última posición conocida.
            animator.SetBool("Patroll", false);
            animator.SetBool("Search", true);
            animator.SetBool("Follow", false);
        }

        // Transiciones a otros estados.
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
