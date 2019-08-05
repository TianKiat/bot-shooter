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
    private void Awake()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        PCon = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
            // check if the enemy is following a path to the player and if the agent is below the attack range from the player
            if (agent.hasPath && agent.remainingDistance < attackRangeThreshold)
            {
                PCon.TakeDamage(damage); // deal damage to the player
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
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddScore(1); // add score
        Destroy(gameObject);
    }
}
