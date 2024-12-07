using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private Animator animator;
    private Enemy enemy;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private Vector3 lastSeenPosition;
    



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
         currentWaypoint = 0; //cada vez q se crea el estado establecemos el punto de inicio a 0
        
        this.enemy.Ruta = enemy.Ruta;
       
        for (int i = 0; i < enemy.Ruta.Length; i++)
        {
            
            this.enemy.Ruta[i].position = enemy.Ruta[i].position;
        }
    }

    public override void Enter()
    {
        //Debug.Log("Entering Patrol State");
        Debug.Log("Entering Patrol State");
        isPatrolling = false; // Inicia la patrulla desde el primer waypoint.
        currentWaypoint = 0;
        routeComplete = false;

    }

    public override void Execute()
    {

        if (!enemy.isChasingPlayer && !enemy.playerHeared)
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
        Debug.Log("entering Patrol Patrol for the love of god");
        if (!isPatrolling)
        {
            currentWaypoint = 0;
            navMeshAgent.SetDestination(enemy.Ruta[currentWaypoint].position);
            isPatrolling = true;
        }
        else
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f) // Verificar si el enemigo ha llegado al waypoint.
            {
                if (!routeComplete)
                {
                    currentWaypoint++; // Avanzar al siguiente waypoint.
                    if (currentWaypoint >= this.enemy.Ruta.Length) // Si el último waypoint ha sido alcanzado, ir al anterior.
                    {
                        currentWaypoint = this.enemy.Ruta.Length - 1;
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

              navMeshAgent.SetDestination(enemy.Ruta[currentWaypoint].position);// Establecer el siguiente waypoint como destino.
            }


        }

    }
}
