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
        Vector3Int currentGridPos = grid.WorldToCell(transform.position);

        int direction = playerMov.DirectionToIndex(moveVal);
        switch (direction)
        {
            case 0: //north
                currentGridPos.y += 1;
                currentGridPos.x += 1;
                break;
            case 1: //northwest
                currentGridPos.y += 1;
                break;
            case 2: //west
                currentGridPos.y += 1;
                currentGridPos.x -= 1;
                break;
            case 3: //southwest
                currentGridPos.x -= 1;
                break;
            case 4: //south
                currentGridPos.y -= 1;
                currentGridPos.x -= 1;
                break;
            case 5: //southeast
                currentGridPos.y -= 1;
                break;
            case 6: //east
                currentGridPos.y -= 1;
                currentGridPos.x += 1;
                break;
            case 7: //northeast
                currentGridPos.x += 1;
                break;
        }

        transform.position = grid.CellToWorld(currentGridPos);

    }

    void OnMovement(InputValue value)
    {
        moveVal = value.Get<Vector2>();
    }
}
