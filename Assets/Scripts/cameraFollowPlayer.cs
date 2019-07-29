using UnityEngine;

public class cameraFollowPlayer : MonoBehaviour
{
    public Transform player; // reference to the transform of the player
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            throw new System.Exception("Player transform not assigned");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // set the camera's x and z position to the player's position so the camera follows the player
        if (player != null)
        {

            transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        }
    }
}
