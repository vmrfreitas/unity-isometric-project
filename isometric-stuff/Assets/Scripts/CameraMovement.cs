using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    private float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    private InputController temporaryReferenceToObject;


    // Start is called before the first frame update
    void Start()
    {
        temporaryReferenceToObject = GameObject.Find("player controlled").GetComponent<InputController>(); 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(temporaryReferenceToObject.BattleMode){
            target = GameObject.Find("cursor").transform;
            smoothing = 0.005f;
        } else {
            target = GameObject.Find("character").transform;
            smoothing = 1f;
        }
        if (transform.position != target.position) {
            Vector3 targetPosition = new Vector3(target.position.x,
                                        target.position.y,
                                        transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x,
                                        minPosition.x,
                                        maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y,
                                        minPosition.y,
                                        maxPosition.y);
            transform.position = Vector3.Lerp(transform.position,
                                            targetPosition,
                                            smoothing);
        }
    }
}
