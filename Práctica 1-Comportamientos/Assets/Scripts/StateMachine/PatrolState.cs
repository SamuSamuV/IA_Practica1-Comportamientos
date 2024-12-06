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



    public Transform[] waypoints;
    // Puntos de patrullaje (waypoints) a seguir por el enemigo.
    private int currentWaypoint; // Índice del waypoint actual.
    private bool isPatrolling = false;
    private bool routeComplete = false; // Indica si la ruta ha sido completada.

    public PatrolState(StateMachine stateMachine, Animator animator, Enemy enemy, NavMeshAgent navMeshAgent) : base(stateMachine)
    {
        this.animator = animator;
        this.enemy = enemy;
        this.navMeshAgent = navMeshAgent;
        this.player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador al inicializar el estado.
        for(int i = 0; i < enemy.Path.Length; i++)
        {
            waypoints[i] = enemy.Path[i];
        }
    }

    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    public override void Execute()
    {
        if(!isChasingPlayer && !this.enemy.playerHeared) 
        {
            Patrol();
        
        }
        //transicion a otros estados 
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
        isPatrolling = false;
        Debug.Log("Exiting Patrol State");
    }



    private void Patrol()
    {
       isChasingPlayer = false; //NS COMO FUNCIONA
        if (!isPatrolling)
        {
            currentWaypoint = 0;
            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
            isPatrolling = true;
        }
        else
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f) // Verificar si el enemigo ha llegado al waypoint.
            {
                if (!routeComplete)
                {
                    currentWaypoint++; // Avanzar al siguiente waypoint.
                    if (currentWaypoint >= waypoints.Length) // Si el último waypoint ha sido alcanzado, ir al anterior.
                    {
                        currentWaypoint = waypoints.Length - 1;
                        routeComplete = true;
                    }
                }
                else
                {
                    currentWaypoint--; // Retroceder a un waypoint anterior si la ruta ha sido completada.
                    if (currentWaypoint < 0) // Si llegamos al principio de la ruta, iniciar de nuevo.
                    {
                        currentWaypoint = 0;
                        routeComplete = false;
                    }
                }

                navMeshAgent.SetDestination(waypoints[currentWaypoint].position); // Establecer el siguiente waypoint como destino.
            }


        }

    }
}
