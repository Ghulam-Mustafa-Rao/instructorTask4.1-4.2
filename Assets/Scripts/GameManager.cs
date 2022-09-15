using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<GameObject> allPlayers;

    public List<GameObject> allPlayersInStart;
    public static GameManager gameManager;

    public bool gameOver = false;

    public float force;
    public float speed;

    public GameObject leaderBoard;

    public GameObject leaderBoardplayerpanel;
    
    private void Awake()
    {
        if (gameManager == null)
            gameManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(allPlayers.Count <=1)
        {
            gameOver = true;
            Time.timeScale = 0;
        } 
    }

    private void FixedUpdate()
    {


        if(leaderBoard.transform.childCount > 0)
        {
            Transform[] allChildren = leaderBoard.GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
               Destroy(child.gameObject);
            }
        }


        
    }
}
