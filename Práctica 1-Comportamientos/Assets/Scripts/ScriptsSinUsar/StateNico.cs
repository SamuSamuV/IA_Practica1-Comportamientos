//using System.Collections.Generic;
//using UnityEngine;

//public abstract class StateNico
//{
//    protected Enemy _enemy;
//    protected List<Transform> _pathPositions;
//    protected bool _canSeePlayer;
//    protected StateMachine _stateMachine;

//    public StateNico(StateMachine stateMachine, Enemy enemy, List<Transform> pathPositions)
//    {
//        this._stateMachine = stateMachine;
//        this._enemy = enemy;
//        this._pathPositions = pathPositions;
//    }

//    //ESTADO PATRULLAR -> SI VE AL JUGADOR O NO LO VE. LO GUARDA EN UNA VARIABLE
//    //ESTADO PERSEGUIR -> SIGO VIENDO AL JUGADOR . LO GUARDA EN UNA VARIABLE
//    //ESTADO MERODEAR -> SI VE AL JUGADOR Y SI HA TERMINADO DE MERODEAR. LOS GUARDA EN UNA VARIABLE
//    public void Percieve()
//    {
//        this._canSeePlayer = CanSeePlayer();
//    }



//    //ESTADO PERSEGUIR -> SI YA NO VE AL JUGADOR -> CAMBIAR AL ESTADO MERODEAR
//    //ESTADO MERODEAR -> SI HA TERMINADO DE MERODEAR -> CAMBIA AL ESTADO PATRULLA
//    //                      -> SI VE AL JUGADOR -> CAMBIA AL ESTADO PERSEGUIR                
//    public abstract void Think();


//    //ESTADO PATRULLAR -> MANDA AL ENEMIGO  AL PUNTO ACTUAL O AL SIGUIENTE PUNTO
//    //ESTADO PERSEGUIR -> MANDA AL ENEMIGO  A LA UBICACION DEL JUGADOR
//    //ESTADO MERODEAR -> MANDA AL ENEMIGO AL PUNTO ACTUAL AL SIGUIENTE PUNTO DE MERODEAR
//    public abstract void Act();

//    //METODO PARA COMPROBAR SI VE AL JUGADOR O NO
//    public bool CanSeePlayer()
//    {
//        return default;
//    }
//}