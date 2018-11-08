using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Transform lookAt; // our penguin  object looking at 
	public Vector3 offset = new Vector3(0,5.0f,-10.0f);
    //public Vector3 rotation = new Vector3(14.0f, 0.0f, 0.0f);
    public bool IsMoving { set; get; }

	// Use this for initialization
	

	// Update is called once per frame
	void Update () {

	}

	// we use late update to make sure that camear move after player movment
	private void LateUpdate(){
        if (!IsMoving)
        {
            return;
        }
		Vector3 desiredPosition = lookAt.position + offset;
		desiredPosition.x = 0;
		transform.position = Vector3.Lerp (transform.position, desiredPosition, Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(14,0,0),0.1f);
	}
}
