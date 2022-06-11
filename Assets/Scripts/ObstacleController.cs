using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallHead")) return;
        
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        BallsController.Instance.RemoveBall(other.gameObject);
    }
}