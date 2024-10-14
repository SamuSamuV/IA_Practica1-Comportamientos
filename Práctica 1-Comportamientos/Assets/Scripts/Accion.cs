using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NPC.Core
{
    public abstract class Accion : ScriptableObject //ScriptableObject permite almacenar datos de manera centralizada y compartida, como estad�sticas o configuraciones, pero cada instancia de enemigo sigue teniendo su propio estado individual. Es un m�todo de optimizaci�n de memoria.    {
    {
        public string Name;
        private float _puntuacion; //determina la urgencia de la acci�n del 0-1 


        public float puntuacion
        {
            get { return _puntuacion;}
            set { _puntuacion = value;}
        }


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

