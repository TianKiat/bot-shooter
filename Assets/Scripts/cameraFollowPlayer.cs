using UnityEngine;

public class cameraFollowPlayer : MonoBehaviour
{
    public Transform player; // reference to the transform of the player
    public float smooth = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private float ZOffset;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            throw new System.Exception("Player transform not assigned");
        }
        ZOffset = Mathf.Abs(transform.position.z) - player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // set the camera's x and z position to the player's position so the camera follows the player
        if (player != null)
        {
            Vector3 pos = new Vector3(player.position.x, transform.position.y, player.transform.position.z - ZOffset);
            transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
        }
    }
}
