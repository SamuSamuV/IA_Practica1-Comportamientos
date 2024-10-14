using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC.Core
{
   

    public class EnemyController : MonoBehaviour
    {
        public Enemy_MoveController ControladorMovimiento { get; set; }
        public AIBrain npcBrain { get; set; }
        //public Accion[] AccionesDisponibles;


     
        void Start()
        {
            ControladorMovimiento = GetComponent<Enemy_MoveController>();
            npcBrain = GetComponent<AIBrain>();
        }

       
        void Update()
        {

        }
    }
}


