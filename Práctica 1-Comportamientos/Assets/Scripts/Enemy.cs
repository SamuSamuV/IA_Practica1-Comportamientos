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

            else if (hit.collider.gameObject == this.gameObject)
            {
                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.blue);
            }

            else if (hit.collider.CompareTag("ConoVision"))
            {
                Debug.DrawRay(player.position, directionToEnemy * raycastDistance, Color.green);
                FollowPlayer();
            }
        }
    }

    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, player.position, 2 * Time.deltaTime);

        Vector3 directionToPlayer = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(directionToPlayer);
    }
}