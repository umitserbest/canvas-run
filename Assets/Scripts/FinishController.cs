using UnityEngine;

public class FinishController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallHead")) return;

        var ballController = other.GetComponent<BallController>();

        BallsController.Instance.Explode(ballController.Row);
    }
}