using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	private Transform playerTransform;
	//private Transform BackTransform;

	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		//BackTransform = GameObject.FindGameObjectWithTag ("MountainsBack").transform;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Vector3.forward * playerTransform.position.z;
		//BackTransform.position = Vector3.forward * playerTransform.position.z;
		
	}
}
