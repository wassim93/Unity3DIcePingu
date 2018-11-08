using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObstacleType{
	slideblocker = 1,
	glazier = 2,
	iceBurg = 3,
	rampIce = 4,
	block = 5,
	rampWood =6,
	blockLog = 7
}

public class Obstacle : MonoBehaviour {

	public ObstacleType type;
	public int visulalIndex;



}
