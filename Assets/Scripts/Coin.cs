using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        //rotate coin every frame
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
