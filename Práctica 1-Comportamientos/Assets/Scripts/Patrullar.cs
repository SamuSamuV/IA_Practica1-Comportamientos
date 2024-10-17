using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrullar : MonoBehaviour
{

    private NavMeshAgent enemyAgent; //respete la maya de navegación
    public Transform[] waypoints;
    int currentWaypoint = 0;
    private bool rutaCompleta = false;

    //[SerializeField] private float VelocidadMovimiento;

    //[SerializeField] private Transform[] puntosMovimiento;//lugares x donde se mueven los enemigos

    //[SerializeField] private float DistanciaMinima; //q no llegue justo al punto

    //int seleccionPunto; // a q punto se va a mover
    //private SpriteRenderer spriteRendenderer;// para controlar como se ve el personaje

    //private bool hasRotated = false;


    //public void Start()
    //{
    //    seleccionPunto = Random.Range(0,puntosMovimiento.Length);
    //    spriteRendenderer = GetComponent<SpriteRenderer>();


    //    enemyAgent = GetComponent<NavMeshAgent>();
    //}

    //private void Update()
    //{
    //    enemyAgent.destination = Vector3.zero;


    //    transform.position = Vector3.MoveTowards(transform.position, puntosMovimiento[seleccionPunto].position, VelocidadMovimiento * Time.deltaTime);

    //    if (Vector3.Distance(transform.position, puntosMovimiento[seleccionPunto].position) < DistanciaMinima)

    //    {

    //        seleccionPunto = Random.Range(0, puntosMovimiento.Length);
    //        Girar();
    //    }


    //}


    //private void Girar()
    //{
    //    if (transform.position.x < (puntosMovimiento[seleccionPunto].position.x))
    //    {
    //        transform.Rotate(0, 180, 0);
    //        hasRotated = true;
    //    }
    //}



    public void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();//la maya que hemos creado


        if (waypoints.Length > 0)
        {
            enemyAgent.SetDestination(waypoints[currentWaypoint].position);
        }

    }


    private void Update()
    {

        if (!enemyAgent.pathPending && enemyAgent.remainingDistance < 0.5f) //si el agente NO está actualmente calculando una nueva ruta o si la distancia de llegada al punto es menor a 0.5 unidades es la distancia mínima considerada como “llegada”.
        {
            
            if (!rutaCompleta)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = waypoints.Length - 1;
                    rutaCompleta = true;
                }
            }

            else
            {
                currentWaypoint--;
                if (currentWaypoint < 0)
                {
                    currentWaypoint = 0;
                    rutaCompleta = false;
                }
            }

            enemyAgent.SetDestination(waypoints[currentWaypoint].position);
        }
    }




}
