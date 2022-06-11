using UnityEngine;

public class BallsController : MonoBehaviour
{
    public static BallsController Instance { get; private set; }
    
    [SerializeField] private GameObject ballPrefab;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CreateBalls(int length,int width)
    {
        for (var i = 0; i < length; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var ball = Instantiate(ballPrefab, transform, false);
                ball.transform.localPosition = new Vector3(0 - j, 0.5f, i);
            }
        }
    }
}