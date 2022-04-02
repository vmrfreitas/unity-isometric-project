using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly float SIN = 0.44721359f; //of 26.5650512 degrees
    private static readonly float COS = 0.89442719f;
    private Rigidbody2D playerRigidBody;
    [SerializeField] private float moveSpeed = 1.0f;
    
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
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        if(inputVector.magnitude > 0.01) {
            switch(DirectionToIndex(inputVector)){
                case 0: //north
                    newPosY += moveSpeed * Time.fixedDeltaTime;
                    break;
                case 1: //northwest
                    newPosY += moveSpeed * Time.fixedDeltaTime * SIN;
                    newPosX -= moveSpeed * Time.fixedDeltaTime * COS;
                    break;
                case 2: //west
                    newPosX -= moveSpeed * Time.fixedDeltaTime;
                    break;
                case 3: //southwest
                    newPosY -= moveSpeed * Time.fixedDeltaTime * SIN;
                    newPosX -= moveSpeed * Time.fixedDeltaTime * COS;
                    break;
                case 4: //south
                    newPosY -= moveSpeed * Time.fixedDeltaTime;
                    break;
                case 5: //southeast
                    newPosY -= moveSpeed * Time.fixedDeltaTime * SIN;
                    newPosX += moveSpeed * Time.fixedDeltaTime * COS;
                    break;
                case 6: //east
                    newPosX += moveSpeed * Time.fixedDeltaTime;
                    break;
                case 7: //northeast
                    newPosY += moveSpeed * Time.fixedDeltaTime * SIN;
                    newPosX += moveSpeed * Time.fixedDeltaTime * COS;
                    break;
            }
        }
        playerRigidBody.MovePosition(new Vector2(newPosX, newPosY));
        FindObjectOfType<PlayerAnimation>().SetDirection(inputVector);
    }

    public int DirectionToIndex(Vector2 _direction)
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
