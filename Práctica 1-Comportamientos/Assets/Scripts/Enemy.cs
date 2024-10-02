using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Transform ojos;

    [Range(0f, 360f)]
    public float visionAngle = 30f;
    public float visionDistance = 10f;

    bool detected = false;

    private void Update()
    {
        detected = false;
        Vector2 playerVector = player.position - ojos.position;
        if(Vector3.Angle(playerVector.normalized, ojos.right) < visionAngle * 0.5f)
        {
            detected = true;
        }

    }

    private void OnDrawGizmos()
    {
        if (visionAngle <= 0f) return;

        float halfVisionAngle = visionAngle * 0.5f;

        Vector2 p1, p2;

        p1 = PointForAngle(halfVisionAngle, visionDistance);
        p2 = PointForAngle(halfVisionAngle, visionDistance);


        Gizmos.color = detected ? Color.green : Color.red;
        Gizmos.DrawLine(player.position, (Vector2)player.position + p1);
        Gizmos.DrawLine(player.position, (Vector2)player.position + p2);

        Gizmos.DrawRay(player.position, ojos.right * 4f);
    }

    Vector2 PointForAngle(float angle, float distance)
    {
        return ojos.TransformDirection(
            new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad))
            * distance);
            

    }
  
}
