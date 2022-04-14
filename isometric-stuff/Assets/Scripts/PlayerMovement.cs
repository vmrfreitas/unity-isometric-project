using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly float SIN = 0.44721359f; //of 26.5650512 degrees
    private static readonly float COS = 0.89442719f;
    private Rigidbody2D playerRigidBody;
    public Vector2 moveVal;
    public Grid grid;
    [SerializeField] private float moveSpeed = 1.0f;
    private InputController temporaryReferenceToObject;

    // Start is called before the first frame update
    void Start()
    {
        temporaryReferenceToObject = GameObject.Find("player controlled").GetComponent<InputController>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        //Vector3Int currentGridPos = grid.WorldToCell(transform.position);
        //playerRigidBody.MovePosition(grid.CellToWorld(currentGridPos)); move player to grid center
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!temporaryReferenceToObject.BattleMode)
        {
            Vector2 currentPos = playerRigidBody.position;

            float newPosX = currentPos.x;
            float newPosY = currentPos.y;
            int direction = DirectionToIndex(moveVal);
            switch (direction)
            {
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

            playerRigidBody.MovePosition(new Vector2(newPosX, newPosY));
            FindObjectOfType<PlayerAnimation>().SetDirection(direction);
        }

    }

    public void MoveToTile(Vector3Int destinationTile)
    {
        Debug.Log(destinationTile);
        playerRigidBody.MovePosition(grid.GetCellCenterWorld(destinationTile));
    }

    void OnMovement(InputValue value)
    {
        moveVal = value.Get<Vector2>();
    }

    public int DirectionToIndex(Vector2 _direction)
    {
        return _direction switch
        {
            Vector2 v when v.Equals(Vector2.up) => 0,
            Vector2 v when v.x == -1 && v.y == 1 => 1,
            Vector2 v when v.Equals(Vector2.left) => 2,
            Vector2 v when v.x == -1 && v.y == -1 => 3,
            Vector2 v when v.Equals(Vector2.down) => 4,
            Vector2 v when v.x == 1 && v.y == -1 => 5,
            Vector2 v when v.Equals(Vector2.right) => 6,
            Vector2 v when v.x == 1 && v.y == 1 => 7,
            _ => 8,
        };
    }
}
