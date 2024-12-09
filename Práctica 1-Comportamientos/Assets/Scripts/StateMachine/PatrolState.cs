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
    private int currentWaypoint;
    private bool isPatrolling = false;
    private bool routeComplete = false;

    public PatrolState(StateMachine stateMachine, Animator animator, Enemy enemy, NavMeshAgent navMeshAgent) : base(stateMachine)
    {
        this.animator = animator;
        this.enemy = enemy;
        this.navMeshAgent = navMeshAgent;
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
         currentWaypoint = 0;
        
        this.enemy.Ruta = enemy.Ruta;
       
        for (int i = 0; i < enemy.Ruta.Length; i++)
        {
            
            this.enemy.Ruta[i].position = enemy.Ruta[i].position;
        }
    }

    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
        isPatrolling = false;
        currentWaypoint = 0;
        routeComplete = false;

    }

    public override void Execute()
    {

        if (!enemy.isChasingPlayer && !enemy.playerHeared)
        {
            Patrol();   
        }
        
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
        if (!isPatrolling)
        {
            currentWaypoint = 0;
            navMeshAgent.SetDestination(enemy.Ruta[currentWaypoint].position);
            isPatrolling = true;
        }
        else
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                if (!routeComplete)
                {
                    currentWaypoint++;
                    if (currentWaypoint >= this.enemy.Ruta.Length)
                    {
                        currentWaypoint = this.enemy.Ruta.Length - 1;
                        routeComplete = true;
                    }
                }
                else
                {
                    currentWaypoint--;
                    if (currentWaypoint < 0)
                    {
                        currentWaypoint = 0;
                        routeComplete = false;
                    }
                }

              navMeshAgent.SetDestination(enemy.Ruta[currentWaypoint].position);
            }


        }

    }
}
