using UnityEngine;

public class CoinCollectTrigger : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    private void OnTriggerEnter(Collider triggeredObject)
    {
        //if player collides with coin, increment score and destroy coin
        if (triggeredObject.CompareTag("Player"))
        {
            
            Destroy(gameObject);

            gameManager.IncrementScore();
        }
    }


}
