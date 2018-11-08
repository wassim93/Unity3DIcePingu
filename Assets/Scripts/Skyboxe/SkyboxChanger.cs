using UnityEngine;
using UnityEngine.UI;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] Skyboxes;

    public void Update()
    {
        //RenderSettings.skybox.SetFloat("_Rotation", 1);
        //	InvokeRepeating("ChangeSky",10,0.0001f);
        if (GameManager.Instance.score > 20000)
        {
            ChangeSky(3);
        }
        else if (GameManager.Instance.score > 80000)
        {
            ChangeSky(3);

        }
        else if (GameManager.Instance.score > 100000)
        {
            ChangeSky(2);

        }


    }

	void ChangeSky(int indexSky){
		RenderSettings.skybox = Skyboxes[indexSky];

	}

   
}