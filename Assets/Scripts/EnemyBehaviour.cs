using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour
{
    public float maxHealth;
    public float damage = 10;
    private float currentHealth;
    private NavMeshAgent agent;

    private Transform player;
    private PlayerController PCon;
    private Rigidbody rb;

    private int ID;

    [Range(0,2)]
    public float attackRangeThreshold = 1;
    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        PCon = player.GetComponent<PlayerController>();
        agent.SetDestination(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
            if (agent.remainingDistance <= attackRangeThreshold && agent.destination != null)
            {
                PCon.TakeDamage(damage);
                //rb.AddExplosionForce(100, transform.position + transform.forward * .5f, .5f);
            }
        }
        else
        {
            agent.enabled = false;
        }
    }

    public virtual void SetID(int _ID)
    {
        ID = _ID;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage; // take damage

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
