using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class CursorMovement : MonoBehaviour
{
    public Vector2 moveVal;
    public Grid grid;
    private Vector2 oldMoveVal = new Vector2(0, 0);
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void OnSelectTile(){
        //vai ter q abrir uma UI pra escolher oq fazer no tile
        //por enquanto vo fazer o boneco andar até o tile e eras isso
        playerMovement.MoveToTile(grid.WorldToCell(transform.position));
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
        transform.position = grid.GetCellCenterWorld(currentGridPos);
    }
}
