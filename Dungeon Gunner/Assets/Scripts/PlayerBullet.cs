using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet according to its own coordinates, so movement is always to the right, and movement in the world coords are based on it's rotation.
        theRb.velocity = transform.right * speed;
    }
}
