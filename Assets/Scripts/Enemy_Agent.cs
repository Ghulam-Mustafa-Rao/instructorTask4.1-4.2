using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_Agent : MonoBehaviour
{
    List<GameObject> otherPlayers;

    NavMeshAgent agent;

    public GameObject destinationObject;

    public Rigidbody rigidbody;

    GameObject lastHitBy;

    public int score = 0;

    public Material material;


    public float strenght;
    private void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();

    }
    // Start is called before the first frame update
    void Start()
    {
        setDestination();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection;
        if (destinationObject != null)
        {
            lookDirection = (destinationObject.transform.position - transform.position).normalized;
            transform.LookAt(destinationObject.transform);
            rigidbody.AddForce(lookDirection * GameManager.gameManager.speed * (rigidbody.mass * 1.5f));
        }
        //agent.SetDestination(destinationObject.transform.position);
        else
        {
            lookDirection = Vector3.zero;
            rigidbody.AddForce(lookDirection * GameManager.gameManager.speed * rigidbody.mass);
            setDestination();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DestroyerGround"))
        {
            try
            {
                if (lastHitBy != null)
                {
                    if (lastHitBy.TryGetComponent<Player>(out Player pla))
                    {
                        pla.score++;
                        pla.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
                        pla.plRigidbody.mass++;
                        pla.material.color = Random.ColorHSV();
                    }
                    else
                    {
                        lastHitBy.GetComponent<Enemy_Agent>().score++;
                        lastHitBy.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
                        lastHitBy.GetComponent<Enemy_Agent>().rigidbody.mass++;
                        lastHitBy.GetComponent<Enemy_Agent>().material.color = Random.ColorHSV();
                    }
                }

            }
            catch (System.Exception)
            {

                //Exception
            }

            GameManager.gameManager.allPlayers.Remove(this.gameObject);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("mainCollider"))
        {
            lastHitBy = collision.gameObject;
            Vector3 awayFromPlayer = (transform.position - collision.gameObject.transform.position);
            float forceApplied = 0;
            if (collision.gameObject.TryGetComponent<Player>(out Player pla))
            {
                strenght = GameManager.gameManager.force * pla.plRigidbody.mass;


            }
            else
            {

                strenght = GameManager.gameManager.force * collision.gameObject.GetComponent<Enemy_Agent>().rigidbody.mass;

            }

            gameObject.GetComponent<Rigidbody>().AddForce(awayFromPlayer * strenght, ForceMode.Impulse);
            //StartCoroutine(setLastHitBy());
            // agent.enabled = !agent.enabled;

            //StartCoroutine(ActivateNavMeshAgentCo());
            //.AddForce(collision.gameObject.transform.right * 
            // (collision.gameObject.GetComponent<Rigidbody>().mass) * GameManager.gameManager.force
            // , ForceMode.Impulse);

        }

    }

    IEnumerator setLastHitBy()
    {
        yield return new WaitForSeconds(1);
        lastHitBy = null;
    }

    IEnumerator setDestinationCo()
    {
        while (!GameManager.gameManager.gameOver)
        {
            setDestination();
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator ActivateNavMeshAgentCo()
    {
        yield return new WaitForSeconds(3);
        agent.enabled = !agent.enabled;
    }

    void setDestination()
    {
        otherPlayers = new List<GameObject>();
        foreach (var item in GameManager.gameManager.allPlayers)
        {
            if (item != this.gameObject)
                otherPlayers.Add(item);
        }
        int index = Random.Range(0, otherPlayers.Count);
        if (otherPlayers.Count == 0)
            destinationObject = null;
        else
            destinationObject = otherPlayers[index];
    }


}
