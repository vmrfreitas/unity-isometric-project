using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CursorMovement : MonoBehaviour
{
    private bool battleMode = false;
    public Tilemap groundTilemap;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(FindObjectOfType<PlayerMovement>().battleMode){
            Vector3Int currentGridPos = groundTilemap.WorldToCell(transform.position);
            
            Vector3Int newGridPos = currentGridPos;
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);

            if(inputVector.magnitude > 0.01) {
                switch(DirectionToIndex(inputVector)){
                    case 0: //north
                        newGridPos.y += 1;
                        newGridPos.x += 1;
                        break;
                    case 1: //northwest
                        newGridPos.y += 1;
                        break;
                    case 2: //west
                        newGridPos.x -= 1;
                        newGridPos.y += 1;
                        break;
                    case 3: //southwest
                        newGridPos.x -= 1;
                        break;
                    case 4: //south
                        newGridPos.x -= 1;
                        newGridPos.y -= 1;
                        break;
                    case 5: //southeast
                        newGridPos.y -= 1;
                        break;
                    case 6: //east
                        newGridPos.x += 1;
                        newGridPos.y -= 1;
                        break;
                    case 7: //northeast
                        newGridPos.x += 1;
                        break;
                }
            }
            transform.position =  groundTilemap.CellToWorld(newGridPos);
        }
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

    public bool getBattleMode(){
        return this.battleMode;
    }

    public void setBattleMode(bool battleMode){
        this.battleMode = battleMode;
    }
}
