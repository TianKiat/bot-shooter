using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10; // move speed of the player
    float currentHealth;
    public float maxHealth = 100; // current and max health
    public Transform handAnchor;
    public GameObject defaultGun;
    private Gun currentgunInstance;
    Rigidbody rb; // rigid body component of this game object
    private float iFrames = 1;
    private float nextDamageTime; // next time the player can take damage
    private bool canTakeDmg = true;
    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody component attached to this game object
        rb = GetComponent<Rigidbody>();
        //set current health to max health
        currentHealth = maxHealth;
        // spawn the default gun in the hand anchor position
        currentgunInstance = Instantiate(defaultGun, handAnchor).GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        rotatePlayer();

        if (Input.GetButton("Fire2"))
        {
            currentgunInstance.Shoot();
        }
        if (Time.time >= nextDamageTime && canTakeDmg == false)
        {
            canTakeDmg = true;
        }
    }

    public void TakeDamage(float damage)
    {
        if (canTakeDmg)
        {
            currentHealth -= damage;
            nextDamageTime = Time.time + iFrames;
            canTakeDmg = false;
            Debug.Log("Took " + damage + "amount of damage. Current health: " + currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void movePlayer()
    {
        // construct a "move" vector based on input and use that vector to translate the player in world space
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime), Space.World);
    }

    private void rotatePlayer()
    {
        //// check if the player is facing up
        //if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") > 0)
        //{
        //    transform.localEulerAngles = Vector3.zero; // set rotation of player to face up
        //}
        //// check if player is facing down
        //else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") < 0)
        //{
        //    transform.localEulerAngles = new Vector3(0, 180, 0); // set rotation of player to face down
        //}
        //// check if player is facing left
        //else if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") == 0)
        //{
        //    transform.localEulerAngles = new Vector3(0, 270, 0); // set rotation of player to face left
        //}
        //// check if player is facing right
        //else if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") == 0)
        //{
        //    transform.localEulerAngles = new Vector3(0, 90, 0); // set rotation of player to face right
        //}
        //// check if player is facing top left
        //else if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)
        //{
        //    transform.localEulerAngles = new Vector3(0, 45, 0); // set rotation of player to face top left
        //}
        //// check if player is facing top right
        //else if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0)
        //{
        //    transform.localEulerAngles = new Vector3(0, 315, 0); // set rotation of player to face top right
        //}
        //// check if player is facing bottom left
        //else if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0)
        //{
        //    transform.localEulerAngles = new Vector3(0, 225, 0); // set rotation of player to face bottom left
        //}
        //// check if player is facing bottom right
        //else if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0)
        //{
        //    transform.localEulerAngles = new Vector3(0, 135, 0); // set rotation of player to face bottom right
        //}

        // player face the mouse
        //if (Input.GetButton("Fire1"))
        //{
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDist = 0.0f;
            if (playerPlane.Raycast(ray, out hitDist))
            {
                Vector3 targetPoint = ray.GetPoint(hitDist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position); // gets a direction and transform it into a quaternion
                targetRotation.x = targetRotation.z = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7.0f * Time.deltaTime);
            }

        //}
    }
}
