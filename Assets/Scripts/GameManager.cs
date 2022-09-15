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

    List<GameObject> laeaderBoardpanelsList;

    private void Awake()
    {
        if (gameManager == null)
            gameManager = this;

        allPlayersInStart = allPlayers;
        laeaderBoardpanelsList = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (allPlayers.Count <= 1)
        {
            gameOver = true;
            //Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        List<lederBoardDetails> n = new List<lederBoardDetails>();


        foreach (var item in allPlayersInStart)
        {
            lederBoardDetails lbd = new lederBoardDetails();
            if (item.gameObject.TryGetComponent<Player>(out Player pla))
            {
                lbd.name = pla.name;
                lbd.score = pla.score;
            }
            else
            { 
                lbd.name = item.gameObject.GetComponent<Enemy_Agent>().name;
                lbd.score = item.gameObject.GetComponent<Enemy_Agent>().score;
            }

            n.Add(lbd);
        }

        n.Sort(delegate (lederBoardDetails x, lederBoardDetails y)
        {
            return y.score.CompareTo(x.score);
        });
        //Debug.LogError("child" + leaderBoard.transform.childCount);
        if(laeaderBoardpanelsList.Count > 0)
        {
            foreach (var item in laeaderBoardpanelsList)
            {
                Destroy(item);
            }
           
        }
        int i = 1;
        laeaderBoardpanelsList = new List<GameObject>();
        foreach (var item in n)
        {
            GameObject ob = Instantiate(leaderBoardplayerpanel, leaderBoard.transform.position, Quaternion.identity);
            ob.transform.SetParent(leaderBoard.transform);
            ob.GetComponent<leaderBoardplayerpanel>().position.text = i.ToString();
            ob.GetComponent<leaderBoardplayerpanel>().name.text = item.name;
            ob.GetComponent<leaderBoardplayerpanel>().kills.text = item.score.ToString();
            laeaderBoardpanelsList.Add(ob);
        }

    }

    class lederBoardDetails
    {
        public string name;
        public int score;
    }
}
