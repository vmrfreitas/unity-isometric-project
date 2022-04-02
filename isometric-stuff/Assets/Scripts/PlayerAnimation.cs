using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    private string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    int lastDirection;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //MARKER each direction will match with one string element
    //MARKER We used direction to determine their animation
    public void SetDirection(Vector2 _direction)
    {
        string[] directionArray;

        if(_direction.magnitude < 0.01)//MARKER Character is static. And his velocity is close to zero
        {
            directionArray = staticDirections;
        }
        else
        {
            directionArray = runDirections;

            lastDirection = FindObjectOfType<PlayerMovement>().DirectionToIndex(_direction);//MARKER Get the index of the slcie from the direction vector
        }

        anim.Play(directionArray[lastDirection]);
    }
}
