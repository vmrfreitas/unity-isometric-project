using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private float moveHorizontal, moveVertical;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private Grid grid;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = playerRigidBody.position;
        Vector3Int gridCurrPos = grid.WorldToCell((Vector3)playerRigidBody.position);
        int roundedHorizontalInput, roundedVerticalInput;
        Debug.Log(gridCurrPos);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput,verticalInput);

        if(horizontalInput>0){
            roundedHorizontalInput = (int)Math.Ceiling(horizontalInput);
        } else {
            roundedHorizontalInput = (int)Math.Floor(horizontalInput);
        }

        if(verticalInput>0){
            roundedVerticalInput = (int)Math.Ceiling(horizontalInput);
        } else {
            roundedVerticalInput = (int)Math.Floor(horizontalInput);
        }
        Vector3Int gridNewPos = gridCurrPos + new Vector3Int(roundedHorizontalInput, roundedVerticalInput, 0);

        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        float moveForce = inputVector.magnitude;

        Vector2 movement = moveForce * (Vector2)grid.CellToWorld(gridNewPos) * moveSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;

        playerRigidBody.MovePosition(newPos);
    }
}
