using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




namespace NPC.Core
{
    

    public class AIBrain : MonoBehaviour
    {

        public Accion AccionElegida {  get;  set; }
        private EnemyController enemigo;
        // Start is called before the first frame update
        void Start()
        {
            enemigo = GetComponent<EnemyController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Pensar(Accion accion)
        {

        }

        public void Actuar(Accion[] accionesPosibles)
        {
            
        }



    }

}

