using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float VelocidadMovimiento;

    [SerializeField] private Transform[] puntosMovimiento;//lugares x donde se mueven los enemigos

    [SerializeField] private float DistanciaMinima; //q no llegue justo al punto

    int seleccionPunto; // a q punto se va a mover
    private SpriteRenderer spriteRendenderer;// para controlar como se ve el personaje


    public void Start()
    {
        seleccionPunto = Random.Range(0,puntosMovimiento.Length);
        spriteRendenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, puntosMovimiento[seleccionPunto].position, VelocidadMovimiento * Time.deltaTime);

        if (Vector3.Distance(transform.position, puntosMovimiento[seleccionPunto].position) < DistanciaMinima)
            
        {

            seleccionPunto = Random.Range(0, puntosMovimiento.Length);
        }

        
    }


    private void Girar()
    {
        if (transform.position.x < (puntosMovimiento[seleccionPunto].position.x))
        {

        }
    }
}
