using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	public ObstacleType type;
	private Obstacle currentObstacle;

	/*public void SpawnObst(){
	
		/*int nbObj = 0;
		switch (type) {
		case ObstacleType.block:
			nbObj = ObstacleManager.Instance.block.Count;
			break;
		case ObstacleType.glazier:
			nbObj = ObstacleManager.Instance.glazier.Count;
			break;
		case ObstacleType.iceBurg:
			nbObj = ObstacleManager.Instance.iceBurg.Count;
			break;
		case ObstacleType.rampIce:
			nbObj = ObstacleManager.Instance.rampIce.Count;
			break;
		case ObstacleType.slideblocker:
			nbObj = ObstacleManager.Instance.slideblocker.Count;
			break;
		}
		currentObstacle = ObstacleManager.Instance.GetObstacle (type,1);*/
		/*currentObstacle.gameObject.SetActive (true);
		currentObstacle.transform.SetParent (transform, false);


	}

	/*public void DespawnObs(){
		currentObstacle.gameObject.SetActive (false);
	}*/
}
