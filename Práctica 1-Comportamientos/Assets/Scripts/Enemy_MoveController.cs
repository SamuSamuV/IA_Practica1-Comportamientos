using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



namespace NPC.Core
{

    public class Enemy_MoveController : MonoBehaviour
    {
        private NavMeshAgent agent;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Desplazarse(Vector3 posicion)
        {
            agent.destination += posicion;
        }
    }

}
