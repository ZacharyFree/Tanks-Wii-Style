using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    //This variable I'm adding to represent to disance between the camera and the ball. I intend to rotate the camera with f and j keys or something
    private Vector3 fallingOffset;



    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        fallingOffset.y = player.transform.position.y;
    }

    
    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)//so game doesn't throw error when player dies
        {
            if (player.transform.position.y <= -10f)
            {
                transform.position = player.transform.position + offset - fallingOffset;
            }
            else
            {
                transform.position = player.transform.position + offset;
            }
        }
        else
        {
            //display You Lose screen
            //option to restart level
        }
    }
}
