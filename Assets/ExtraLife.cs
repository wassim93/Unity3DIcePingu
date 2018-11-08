using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            GameManager.Instance.GetLife();
            GameManager.Instance.activePowerUp = true;
            //GameManager.Instance.powerUpAnim.SetTrigger("TimerHide");
           // Invoke("ResetJump", 4);//this will happen after 2 seconds

            //GameManager.Instance.timerPowerup.fillAmount -= Time.deltaTime / 5;

            //Destroy(gameObject);
        }



    }


    void ResetJump() {

        GameManager.Instance.playerMovement.jumpForce = 4.0f;

    }
}
