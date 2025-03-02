using UnityEngine;

public class CoinCollectTrigger : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    private void OnTriggerEnter(Collider triggeredObject)
    {
        // Check if the object that triggered the collider is the player
        if (triggeredObject.CompareTag("Player"))
        {
            // Destroy the coin object
            Destroy(gameObject);

            gameManager.IncrementScore();
        }
    }


}
