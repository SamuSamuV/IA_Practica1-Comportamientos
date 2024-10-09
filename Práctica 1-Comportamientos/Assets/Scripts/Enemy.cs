using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask enemyLayer;
    public LayerMask wallLayer;
    public float raycastDistance = 100f;
    public Transform player;
    public Transform[] enemies;
    public GameObject enemy;

    [SerializeField] float moveSpeed = 5f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        enemies = new Transform[enemyObjects.Length];
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemies[i] = enemyObjects[i].transform;
        }

        enemy = gameObject;
    }

    void Update()
    {
        RayCastLogic();
    }

    public void RayCastLogic()
    {
        //for (int i = 0; i < enemies.Length; i++) //Este bucle for se usaba para que cada enemigo pudiese saber la posición de los otros enemigos

        Vector3 directionToEnemy = (enemy.transform.position - player.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(player.position, directionToEnemy, out hit, raycastDistance, enemyLayer))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.red);
            }

            else if (hit.collider.CompareTag("Enemy"))
            {
                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.blue);
            }

            else if (hit.collider.CompareTag("ConoVision"))
            {
                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.green);
            }
        }
    }

    //public Transform player;
    //public Transform ojos;

    //[Range(0f, 360f)]
    //public float visionAngle = 30f;
    //public float visionDistance = 10f;

    //bool detected = false;

    //private void Update()
    //{
    //    detected = false;
    //    Vector2 playerVector = player.position - ojos.position;
    //    if(Vector3.Angle(playerVector.normalized, ojos.right) < visionAngle * 0.5f)
    //    {
    //        detected = true;
    //    }

    //}

    //private void OnDrawGizmos()
    //{
    //    if (visionAngle <= 0f) return;

    //    float halfVisionAngle = visionAngle * 0.5f;

    //    Vector2 p1, p2;

    //    p1 = PointForAngle(halfVisionAngle, visionDistance);
    //    p2 = PointForAngle(halfVisionAngle, visionDistance);


    //    Gizmos.color = detected ? Color.green : Color.red;
    //    Gizmos.DrawLine(player.position, (Vector2)player.position + p1);
    //    Gizmos.DrawLine(player.position, (Vector2)player.position + p2);

    //    Gizmos.DrawRay(player.position, ojos.right * 4f);
    //}

    //Vector2 PointForAngle(float angle, float distance)
    //{
    //    return ojos.TransformDirection(
    //        new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),
    //        Mathf.Sin(angle * Mathf.Deg2Rad))
    //        * distance);


    //}

}
