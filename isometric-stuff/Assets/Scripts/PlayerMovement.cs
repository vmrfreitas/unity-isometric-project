using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private float moveHorizontal, moveVertical;
    [SerializeField] private float moveSpeed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed;
        moveVertical = Input.GetAxis("Vertical")* moveSpeed;
        playerRigidBody.velocity = new Vector2(moveHorizontal, moveVertical);
    }
}
