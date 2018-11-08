using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour {

    public int maxPower = 1;
    public float chanceToSpawn = 0.5f;
    private GameObject[] powerups;
    // Use this for initialization
    private void Awake()
    {
        powerups = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            powerups[i] = transform.GetChild(i).gameObject;
        }

        OnDisable();

    }
    private void OnEnable()
    {
        if (Random.Range(0.0f, 1.0f) > chanceToSpawn)
        {
            return;
        }

        int r = Random.Range(0, maxPower);
        for (int i = 0; i <= r; i++)
        {
            powerups[i].SetActive(true);
        }

    }

    private void OnDisable()
    {
        foreach (GameObject go in powerups)
        {
            go.SetActive(false);
        }
    }
}
