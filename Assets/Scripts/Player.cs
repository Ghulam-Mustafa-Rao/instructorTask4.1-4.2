using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody plRigidbody;
    public float plMoveSpeed;
    public float plRotateSpeed;
    public GameObject focalPoint;

    public int score = 0;

    GameObject lastHitBy;

    public Material material;
    public float strenght;
    public string name;
    GameObject collidersParent;
    private void Awake()
    {
       

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0)
        {
            plRigidbody.AddForce(focalPoint.transform.forward * verticalInput * plMoveSpeed * plRigidbody.mass);
        }
        else
            plRigidbody.velocity = Vector3.zero;

        transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime * plRotateSpeed);

        plRigidbody.angularVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("mainCollider"))
        {
            collidersParent = collision.gameObject.transform.parent.gameObject;
            lastHitBy = collidersParent;
            Vector3 awayFromPlayer = (transform.position - collidersParent.transform.position);
            float forceApplied = 0;
            if (collidersParent.TryGetComponent<Player>(out Player pla))
            {
                strenght = GameManager.gameManager.force * pla.plRigidbody.mass;


            }
            else
            {

                strenght = GameManager.gameManager.force * collidersParent.GetComponent<Enemy_Agent>().rigidbody.mass;

            }


            gameObject.GetComponent<Rigidbody>().AddForce(awayFromPlayer * strenght, ForceMode.Impulse);// agent.enabled = !agent.enabled;

            //StartCoroutine(ActivateNavMeshAgentCo());
            //.AddForce(collision.gameObject.transform.right * 
            // (collision.gameObject.GetComponent<Rigidbody>().mass) * GameManager.gameManager.force
            // , ForceMode.Impulse);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DestroyerGround"))
        {
            try
            {
                if (lastHitBy.TryGetComponent<Player>(out Player pla))
                {
                    pla.score++;
                    pla.transform.localScale += new Vector3(0.3f,0.3f,0.3f);
                    pla.plRigidbody.mass++;
                }
                else
                {
                    lastHitBy.GetComponent<Enemy_Agent>().score++;
                    lastHitBy.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
                    lastHitBy.GetComponent<Enemy_Agent>().GetComponent<Rigidbody>().mass++;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
            GameManager.gameManager.allPlayers.Remove(this.gameObject);
            Destroy(this.gameObject);
        }

        

    }
}
