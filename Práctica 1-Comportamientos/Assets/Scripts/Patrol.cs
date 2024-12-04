using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : StateMachineBehaviour
{

   // Componente que maneja el movimiento del enemigo a través de NavMesh.
    public Transform[] waypoints; // Puntos de patrullaje (waypoints) a seguir por el enemigo.
    private int currentWaypoint = 0; // Índice del waypoint actual.
    private bool routeComplete = false; // Indica si la ruta ha sido completada.
    private bool isPatrolling = true;
    private NavMeshAgent navMeshAgent;
    Enemy enemy;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>(); // Obtener el NavMeshAgent asociado al enemigo.
        enemy = animator.gameObject.GetComponent<Enemy>();

        if (waypoints.Length > 0)
        {
           navMeshAgent.SetDestination(waypoints[currentWaypoint].position); // Establecer el destino inicial en el primer waypoint.
        }

        // Iniciar el patrullaje si no se ha iniciado ya.
       
        isPatrolling = true;
    }

    //NICO
    public void StateEnter(List<Transform> transforms)
    {
        waypoints = transforms.ToArray();
    }


    // OnStateUpdate es llamado cada frame mientras el enemigo esté en el estado de patrullaje.
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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

    private void GoToClosestWaypoint(Animator animator)
    {
        float closestDistance = Mathf.Infinity;
        int closestWaypointIndex = 0;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Vector3.Distance(animator.transform.position, waypoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWaypointIndex = i;
            }
        }

        currentWaypoint = closestWaypointIndex;
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        isPatrolling = true;
    }
}


    //private NavMeshAgent enemyAgent; //respete la maya de navegación
    //public Transform[] waypoints;
    //int currentWaypoint = 0;
    //private bool ruteComplete = false;
    //private bool isPatrolling = true;
    //Search search;



//    public void Start()
//    {
//      //  enemyAgent = //GetComponent<NavMeshAgent>();//la maya que hemos creado


//        if (waypoints.Length > 0)
//        {
//            enemyAgent.SetDestination(waypoints[currentWaypoint].position);
//        }

//    }


//    private void Update()
//    {

//        if (!enemyAgent.pathPending && enemyAgent.remainingDistance < 0.5f) //si el agente NO está actualmente calculando una nueva ruta o si la distancia de llegada al punto es menor a 0.5 unidades es la distancia mínima considerada como “llegada”.
//        {

//            if (!ruteComplete)
//            {
//                currentWaypoint++;
//                if (currentWaypoint >= waypoints.Length)
//                {
//                    currentWaypoint = waypoints.Length - 1;
//                    ruteComplete = true;
//                }
//            }

//            else
//            {
//                currentWaypoint--;
//                if (currentWaypoint < 0)
//                {
//                    currentWaypoint = 0;
//                    ruteComplete = false;
//                }
//            }

//            enemyAgent.SetDestination(waypoints[currentWaypoint].position);
//        }
//    }


//    public void TogglePatrol(bool status)
//    {
//        isPatrolling = status;

//        // Si se reactiva la patrulla, ir al waypoint más cercano
//        if (status)
//        {
//            GoToClosestWaypoint();
//        }
//        else
//        {
//            enemyAgent.ResetPath(); // Detener el movimiento mientras no esté en modo patrulla
//        }
//    }

//    private void GoToClosestWaypoint()
//    {
//       // float closestDistance = Mathf.Infinity;
//        int closestWaypointIndex = 0;

//        for (int i = 0; i < waypoints.Length; i++)
//        {
//            float distance = Vector3.Distance(//transform.position, waypoints[i].position);
//              if (//distance < closestDistance)
//            {
//                closestDistance = distance;
//                closestWaypointIndex = i;
//            }
//        }

//        currentWaypoint = closestWaypointIndex;
//        enemyAgent.SetDestination(waypoints[currentWaypoint].position);
//        isPatrolling = true;
//    }
//}


