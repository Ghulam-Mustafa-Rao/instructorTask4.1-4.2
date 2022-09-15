using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float yoffset = 20;

    float max = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        foreach (var item in GameManager.gameManager.allPlayers)
        {
            if (item.transform.position.x > max)
            {
                max = item.transform.position.x;
            }
            if (item.transform.position.z > max)
            {
                max = item.transform.position.z;
            }

        }
        if (max < -0.01)
            max = (-1) * max;
    }

    private void LateUpdate()
    {
        Debug.LogError(max);

        if (player != null)
            transform.position = new Vector3(player.transform.position.x, yoffset + max, player.transform.position.z);
        else
        {
            transform.position = new Vector3(0, yoffset + max, 0);
        }
    }
}
