using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class CursorMovement : MonoBehaviour
{
    private PlayerMovement playerMov;
    public Vector2 moveVal;
    public Grid grid;
    private GameObject character;
    private Vector2 oldMoveVal = new Vector2(0, 0);
    //public Tilemap groundTilemap;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("character");
        playerMov = character.GetComponent<PlayerMovement>();
        //transform.position = groundTilemap.GetCellCenterLocal(groundTilemap.WorldToCell(GameObject.Find("character").transform.position));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*Vector3Int currentGridPos = grid.WorldToCell(transform.position);

        int direction = playerMov.DirectionToIndex(moveVal);
        switch (direction)
        {
            case 0: //north
                currentGridPos.y += 1;
                break;
           // case 1: //northwest
            //    currentGridPos.y += 1;
            //    break;
            case 1: //west
                currentGridPos.x -= 1;
                break;
            //case 3: //southwest
            //    currentGridPos.x -= 1;
            //    break;
            case 2: //south
                currentGridPos.y -= 1;
                break;
            //case 5: //southeast
            //    currentGridPos.y -= 1;
             //   break;
            case 3: //east
                currentGridPos.x += 1;
                break;
            //case 7: //northeast
            //    currentGridPos.x += 1;
             //   break;
        }

        transform.position = grid.CellToWorld(currentGridPos);*/

    }

    void OnCursorMovement(InputValue value)
    {        
        Vector3Int currentGridPos = grid.WorldToCell(transform.position);
        moveVal = value.Get<Vector2>();
        if (moveVal.x != oldMoveVal.x){
            currentGridPos.y -= (int)moveVal.x;
        }
        if (moveVal.y != oldMoveVal.y){
            currentGridPos.x += (int)moveVal.y;
        }
        oldMoveVal = moveVal;
        transform.position = grid.CellToWorld(currentGridPos);
        Debug.Log(moveVal);
    }

    public int DirectionToIndex(Vector2 _direction)
    {
        return _direction switch
        {
            Vector2 v when v.Equals(Vector2.up) => 0,
            Vector2 v when v.Equals(Vector2.left) => 1,
            Vector2 v when v.Equals(Vector2.down) => 2,
            Vector2 v when v.Equals(Vector2.right) => 3,
            _ => 4,
        };
    }
}
