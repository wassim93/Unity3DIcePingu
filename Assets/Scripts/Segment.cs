using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour {

	public int SegId {set;get;}
	public bool transition;
	public int beginY1, beginY2, beginY3;
	public int endY1, endY2, endY3;

	public int lenght;

	private ObstacleSpawner[] obstacles;

	public void Awake(){
		obstacles = gameObject.GetComponentsInChildren<ObstacleSpawner> ();
		/*for (int i = 0; i < obstacles.Length; i++) {
			foreach (MeshRenderer mr in obstacles[i].GetComponentsInChildren<MeshRenderer>())
				mr.enabled = false;
		}*/
	}

	public void Spawn(){
		gameObject.SetActive (true);
		for (int i = 0; i < obstacles.Length; i++) {
			obstacles [i].gameObject.SetActive (true);
			obstacles [i].transform.SetParent (transform, false);


		}
	}

	public void DeSpawn(){
		gameObject.SetActive (false);
		for (int i = 0; i < obstacles.Length; i++) {
			obstacles [i].gameObject.SetActive (false);
		}
	}


	// Use this for initialization
	void Start () {
			

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
