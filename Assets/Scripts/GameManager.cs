using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        BallsController.Instance.CreateBalls(10, 4);
    }
}