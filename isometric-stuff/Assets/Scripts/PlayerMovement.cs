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
        float newPosX = currentPos.x;
        float newPosY = currentPos.y;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        float moveForce = inputVector.magnitude;

        switch(DirectionToIndex(inputVector)){
            case 0: //north
                newPosY += moveForce * moveSpeed * Time.fixedDeltaTime;
                break;
            case 2: //west
                newPosX -= moveForce * moveSpeed * Time.fixedDeltaTime;
                break;
            case 4: //south
                newPosY -= moveForce * moveSpeed * Time.fixedDeltaTime;
                break;
            case 6: //east
                newPosX += moveForce * moveSpeed * Time.fixedDeltaTime;
                break;
        }
        //Debug.Log("X = " + newPosX + " Y = " + newPosY);
        //Debug.Log(Time.fixedDeltaTime * moveForce * moveSpeed);
        playerRigidBody.MovePosition(new Vector2(newPosX, newPosY));
    }

    private int DirectionToIndex(Vector2 _direction)
    {
        Vector2 norDir = _direction.normalized;//MARKER return this vector with a magnitude of 1 and get the normalized to an index

        float step = 360 / 8;//MARKER 45 one circle and 8 slices//Calcuate how many degrees one slice is 
        float offset = step / 2;//MARKER 22.5//OFFSET help us easy to calcuate and get the correct index of the string array

        float angle = Vector2.SignedAngle(Vector2.up, norDir);//MARKER returns the signed angle in degrees between A and B

        angle += offset;//Help us easy to calcuate and get the correct index of the string array

        if(angle < 0)//avoid the negative number 
        {
            angle += 360;
        }

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
