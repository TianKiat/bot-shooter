using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 5;
    public float damage = 10;
    public float range = 10;
    public Transform muzzleTransform;
    private LineRenderer lr; // line renderer for the bullet
    private float nextShotTime;
    private bool lrEnable = false;
    private float lrDisabletime;
    public LayerMask mask;
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    public virtual void Shoot()
    {
        // can only shoot after next shot time
        if (Time.time > nextShotTime)
        {
            // perform raycast to see if it hits an enemy
            RaycastHit hit;
            if (Physics.Raycast(muzzleTransform.position, muzzleTransform.forward * range, out hit, mask))
            {
                // if it hits an object that is tagged as an enemy grab the enemy behaviour component and call the TakeDamage method passing in the gun's damage
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<EnemyBehaviour>().TakeDamage(damage);
                    lr.useWorldSpace = true;
                    lr.SetPosition(0, muzzleTransform.position);
                    lr.SetPosition(1, hit.transform.localPosition); // if the bullet hits an enemy set the end of the line renderer to the enemy
                }
                else
                {
                    lr.useWorldSpace = false;
                    Vector3 pos = new Vector3(0, 0, range);
                    lr.SetPosition(0, Vector3.zero);
                    lr.SetPosition(1, pos);
                }
            }
            

            nextShotTime = Time.time + fireRate / 10;
            lr.enabled = lrEnable = true;

        }
    }

    private void Update()
    {
        if (lrEnable)
        {
            lrDisabletime = Time.time + .01f;
            lrEnable = false;
        }
        if (Time.time > lrDisabletime)
        {
            lr.enabled = lrEnable;
        }
    }
}
