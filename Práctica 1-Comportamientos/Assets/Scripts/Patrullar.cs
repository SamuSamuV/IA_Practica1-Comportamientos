using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float VelocidadMovimiento;

    [SerializeField] private Transform[] puntosMovimiento;

    [SerializeField] private float[] DistanciaMinima;

    int seleccionPunto;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, puntosMovimiento[seleccionPunto].position, VelocidadMovimiento * Time.deltaTime);

        if(Vector3.Distance(transform.position, puntosMovimiento[seleccionPunto].position < DistanciaMinima)
        {

        }
    }
}
