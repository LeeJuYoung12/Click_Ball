using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ball") && (GameManager.isLevel2 || GameManager.isLevel3))
        {
            BallManager ball = GameManager.Ball_Manager;

            Vector3 income = ball._dir;
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflectedVec = Vector3.Reflect(income , normal);
            ball._dir = reflectedVec;
            //Debug.Log("Collide");

        }
    }
}
