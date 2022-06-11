using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        //var ballController = other.GetComponent<BallController>();
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        BallsController.Instance.RemoveBall(other.gameObject);
    }
}