
using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] PlayerController playerController;
    [SerializeField] UIController uiController;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            playerController.timeBonus = 150000 / (int)playerController.gameTimer;
            uiController.YouWin();
        }
    }
}
