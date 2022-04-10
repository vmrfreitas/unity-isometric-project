using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public PlayerInput playerInput;
    private bool _battleMode;
    // Start is called before the first frame update
    void Start()
    {
        BattleMode = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool BattleMode
    {
        get { return _battleMode; }
        set
        {
            if (_battleMode != value)
            {
                _battleMode = value;
                if(_battleMode){
                    playerInput.SwitchCurrentActionMap("In-battle");
                } else {
                    playerInput.SwitchCurrentActionMap("Off-battle");
                }
            }
        }
    }
}
