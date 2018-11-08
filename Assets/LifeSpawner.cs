using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpawner : MonoBehaviour {

    private GameObject go;
    // Use this for initialization

    private void Awake()
    {
        go = transform.GetChild(0).gameObject;
        //go.SetActive(false);

    }
    void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.coinScore > 2)
        {
            if (GameManager.Instance.lifeScore == 1)
            {
                go.SetActive(false);
                return;

            }
            if (GameManager.Instance.lifeScore == 0)
            {
                go.SetActive(true);
            }

        }
	}

    void SpawnLife() {
        if (true)
        {

        }
    }
}
