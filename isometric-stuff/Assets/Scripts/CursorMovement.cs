using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class CursorMovement : MonoBehaviour
{
    public Vector2 moveVal;
    public Grid grid;
    private Vector2 oldMoveVal = new(0, 0);
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
        bool changeHappened = false;
        Vector3Int currentGridPos = grid.WorldToCell(transform.position);
        moveVal = value.Get<Vector2>();
        if(moveVal != Vector2.zero) {
            if (moveVal.x != oldMoveVal.x){
                currentGridPos.y -= (int)moveVal.x;
                changeHappened = true;
            }
            if (moveVal.y != oldMoveVal.y){
                currentGridPos.x += (int)moveVal.y;
                changeHappened = true;
            }
            if (changeHappened) {
                oldMoveVal = moveVal;
                Vector3 wrongCellCenter = grid.GetCellCenterWorld(currentGridPos);
                Vector3 rightCellCenter = new(wrongCellCenter.x, wrongCellCenter.y - 0.125f, wrongCellCenter.z); //the z is temporary
                transform.position = rightCellCenter;
            }
        }
        oldMoveVal = moveVal;
    }
}
