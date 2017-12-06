using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{

    public GameObject player;//stores the GameObject which the camera should follow
    private Vector3 offset;//Private variable to store the offset distance between the player and camera
    public bool vertical = false;


    void Start()
    {
        //calculating the offset
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (vertical)
        {
            followver();
        }
        else
        {
            followhor();
        }
    }

    //Is called to follow player horizontally
    void followhor()
    {
        //transforms the x-position of the camera each fixed interval to follow the player
        Vector3 cameraPosx = transform.position;
        cameraPosx.x = player.transform.position.x;
        transform.position = cameraPosx;

        if (transform.position.y + 3.5 < player.transform.position.y)
        {
            Vector3 cameraPosy = transform.position;
            cameraPosy.y = player.transform.position.y + offset.y - 3.5f;
            transform.position = Vector3.Lerp(transform.position, cameraPosy, (Time.deltaTime * 2));
        }

        if (transform.position.y > player.transform.position.y + offset.y)
        {
            Vector3 cameraPosy = transform.position;
            cameraPosy.y = player.transform.position.y + offset.y;
            transform.position = Vector3.Lerp(transform.position, cameraPosy, Time.smoothDeltaTime * 3);
        }
    }

    //is called to follow the player vertically to move up towers or anything like that
    void followver()
    {
        //transforms the y-position of the camera each fixed interval to follow the player
        Vector3 cameraPosy = transform.position;
        cameraPosy.y = player.transform.position.y;
        transform.position = cameraPosy;

        if (transform.position.x + 5 < player.transform.position.x ||
            transform.position.x - 5 > player.transform.position.x)
        {
            Vector3 cameraPosx = transform.position;
            cameraPosx.x = player.transform.position.x + offset.x;
            transform.position = Vector3.Lerp(transform.position, cameraPosx, Time.deltaTime);
        }
    }

    //Method is called when the player respawns to set the camera on the player again
    public void Respawn()
    {
        transform.position = player.transform.position + offset;
    }
}
