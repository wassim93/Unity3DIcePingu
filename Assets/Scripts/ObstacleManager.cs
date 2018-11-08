using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

	private const float DISTANCE_BEFORE_SPAWN = 100.0f;
	private const float INITIAL_SEGMENTS = 5;
	private const float INITIAL_TRASITION_SEGMENTS = 2;

	private const float MAX_SEGMENTS_ON_SCREEN = 15;
	//public  bool SHOW_COLLIDER = true;
	private Transform cameraContainer;
	private int activeSegments;
	private int continiousSegments;
	private int currentSpawn;
	//private int currentLevel;
	//private int y1, y2, y3;





	//public List<Obstacle> slideblocker = new List<Obstacle>();
	//public List<Obstacle> glazier = new List<Obstacle>();
	//public List<Obstacle> iceBurg = new List<Obstacle>();
	//public List<Obstacle> rampIce = new List<Obstacle>();
	//public List<Obstacle> block = new List<Obstacle>();
	//private List<Obstacle> obstaclesPool = new List<Obstacle>();

	public List<Segment> availableSegments = new List<Segment> ();

	public List<Segment> availableTransitions = new List<Segment> ();
	private List<Segment> segments = new List<Segment> ();


	//private bool isMoving = false;

	public static ObstacleManager Instance { set; get;}
	/*public Obstacle GetObstacle(ObstacleType type , int visualIndex){
		Obstacle ob = obstaclesPool.Find (x => x.type == type && x.visulalIndex == visualIndex && !x.gameObject.activeSelf );
		if (ob == null) {
			GameObject go = null;
			if (type == ObstacleType.block) {
				go = block [visualIndex].gameObject;
			}
			else if (type == ObstacleType.glazier) {
				go = glazier [visualIndex].gameObject;
			}
			else if (type == ObstacleType.iceBurg) {
				go = iceBurg [visualIndex].gameObject;
			}
			else if (type == ObstacleType.rampIce) {
				go = rampIce [visualIndex].gameObject;
			}
			else if (type == ObstacleType.slideblocker) {
				go = slideblocker [visualIndex].gameObject;
			}

			go = Instantiate (go);
			ob = go.GetComponent<Obstacle> ();
			obstaclesPool.Add (ob);
		}
		return ob;
	}*/

	public Segment GetSegment(int id,bool transition){
		Segment s = null;
		s = segments.Find (x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);
		if (s == null) {
			GameObject go = Instantiate ((transition) ? availableTransitions [id].gameObject : availableSegments [id].gameObject);
			s = go.GetComponent<Segment> ();
			s.SegId = id;
			s.transition = transition;
			segments.Insert (0, s);
		} else {
			segments.Remove (s);
			segments.Insert (0, s);
		}

		return s;
	}

	private void Awake(){
		Instance = this;
		cameraContainer = Camera.main.transform;
		currentSpawn = 0;
		//currentLevel = 0;
	}

	private void Start(){
	
		for (int i = 0; i < INITIAL_SEGMENTS; i++) {
			if (i < INITIAL_TRASITION_SEGMENTS) {
				SpawnTransition ();
			} else {
				GenerateSegment ();

			}
		}
	}

	private void FixedUpdate(){
		if (currentSpawn - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN) {
			GenerateSegment ();
		}
		if (activeSegments >= MAX_SEGMENTS_ON_SCREEN) {
			segments [activeSegments - 1].DeSpawn ();
			activeSegments--;
		}
	
	}

	private void GenerateSegment(){
	
		SpawnSegment ();
		if (Random.Range (0f, 1f) < continiousSegments * 0.25f) {
			continiousSegments = 0;
			SpawnTransition ();
		} else {
			continiousSegments++;
		}


		//Debug.Log (continiousSegments);


	}


	private void SpawnSegment(){
		//List<Segment> possibleSeg = availableSegments.FindAll (x=>x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
		int id = Random.Range (0, availableSegments.Count);

		//Debug.Log (id);
		Segment s = GetSegment (id, false);
		//y1 = s.endY1;
		//y2 = s.endY2;
		//y3 = s.endY3;
		s.transform.SetParent (transform);
		s.transform.localPosition = Vector3.forward * currentSpawn;
		currentSpawn += s.lenght;
		activeSegments++;
		s.Spawn ();
	}
	private void SpawnTransition(){

		//List<Segment> possibleTrans = availableTransitions.FindAll (x=>x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
		int id = Random.Range (0, availableTransitions.Count);
		Segment s = GetSegment (id, true);
		//y1 = s.endY1;
		//y2 = s.endY2;
		//y3 = s.endY3;
		s.transform.SetParent (transform);
		s.transform.localPosition = Vector3.forward * currentSpawn;
		currentSpawn += s.lenght;
		activeSegments++;
		s.Spawn ();

	}

}
