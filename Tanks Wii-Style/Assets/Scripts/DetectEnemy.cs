using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    public Enemy enemy;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == enemy.player.GetComponent<Collider2D>())//can call directly (i.e. no variable created) if player is "static"
        {
            enemy.canSeePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy.canSeePlayer = false;
    }
}
