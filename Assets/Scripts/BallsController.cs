using System.Collections.Generic;
using UnityEngine;

public class BallsController : MonoBehaviour
{
    public static BallsController Instance { get; private set; }

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform[] ballHeadPositions;

    [SerializeField] private float speed = 1f;

    private readonly List<List<GameObject>> _balls = new();
    private readonly List<GameObject> _ballRemoveList = new();

    private Swipe _swipeDirection;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        SwipeManager.OnSwipeDetected += OnSwipeDetected;
    }

    private void OnDisable()
    {
        SwipeManager.OnSwipeDetected -= OnSwipeDetected;
    }

    private void OnSwipeDetected(Swipe swipeDirection, Vector2 swipeVelocity)
    {
        _swipeDirection = swipeDirection;
    }

    private float _cleanTimer;
    
    private void Update()
    {
        MoveHeads();
        MoveMinions();

        _cleanTimer += Time.deltaTime;
        if (_cleanTimer > 2f)
        {
            _cleanTimer = 0f;
            CleanBallList();
        }
    }

    private void MoveHeads()
    {
        foreach (var head in ballHeadPositions)
        {
            var leftRightPositon = new Vector3();
        
            if (_swipeDirection == Swipe.Left)
            {
                leftRightPositon = Vector3.right;
            }
            else if (_swipeDirection == Swipe.Right)
            {
                leftRightPositon = Vector3.left;
            }

            var newPosition = head.position - Vector3.forward + leftRightPositon;
            var time = Time.deltaTime * speed;
            head.SetPositionAndRotation(Vector3.Lerp(head.position, newPosition, time), head.transform.rotation);
        }
    }

    private void MoveMinions()
    {
        for (var i = 0; i < _balls.Count; i++)
        {
            for (var j = 0; j < _balls[i].Count; j++)
            {
                var current = _balls[i][j];
                
                var head = i == 0 ? ballHeadPositions[j] : _balls[i - 1][j].transform;

                if (!current.GetComponent<BallController>().IsMoving) continue;

                var distance = Vector3.Distance(head.position, current.transform.position);
                var target = head.position;
                var time = Time.deltaTime * distance * speed;

                current.transform.SetPositionAndRotation(
                    Vector3.Lerp(current.transform.position, target, time),
                    Quaternion.Lerp(current.transform.rotation, head.rotation, time));
            }
        }
    }

    public void CreateBalls(int length, int width)
    {
        for (var i = 0; i < length; i++)
        {
            var columns = new List<GameObject>();
            
            for (var j = 0; j < width; j++)
            {
                var ball = Instantiate(ballPrefab, transform, false);
                ball.transform.localPosition = new Vector3(0 - j, 0.5f, i);
                columns.Add(ball);
                
                var ballController = ball.GetComponent<BallController>();
                ballController.Row = i;
                ballController.Column = j;
            }

            _balls.Add(columns);
        }
    }

    public void RemoveBall(GameObject ball)
    {
        _ballRemoveList.Add(ball);
    }

    private void CleanBallList()
    {
        foreach (var ball in _ballRemoveList)
        {
            if (ball == null) continue;
            
            var row = ball.GetComponent<BallController>().Row;
            _balls[row].Remove(ball);
            Destroy(ball);
        }
    }

    public void Explode(int row)
    {
        for (var i = 0; i < _balls[row].Count; i++)
        {
            var current = _balls[row][i];

            var rigidbody = current.GetComponent<Rigidbody>();
            rigidbody.AddForce(Random.Range(-5, 5) * 100f, 0f, Random.Range(-5, 5) * 100f);
            
            var ballController = current.GetComponent<BallController>();
            ballController.IsMoving = false;
        }
    }
}