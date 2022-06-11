using System.Collections.Generic;
using UnityEngine;

public class BallsController : MonoBehaviour
{
    public static BallsController Instance { get; private set; }
    
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform[] ballHeadPositions;

    private List<List<GameObject>> _balls = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        MoveHeads();
        MoveMinions();
    }

    private void MoveHeads()
    {
        foreach (var head in ballHeadPositions)
        {
            var newPos = head.position - Vector3.forward;
            var followTime = Time.deltaTime * 1f;
            head.SetPositionAndRotation(Vector3.Lerp(head.position, newPos, followTime), head.transform.rotation);
        }
    }

    private void MoveMinions()
    {
        for (var i = 0; i < _balls.Count; i++)
        {
            for (var j = 0; j < _balls[i].Count; j++)
            {
                var head = i == 0 ? ballHeadPositions[j] : _balls[i - 1][j].transform;

                var current = _balls[i][j];
                var distance = Vector3.Distance(head.position, current.transform.position);
                var target = head.position;
                var time = Time.deltaTime * distance * 1f;

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
            var widthList = new List<GameObject>();
            
            for (var j = 0; j < width; j++)
            {
                var ball = Instantiate(ballPrefab, transform, false);
                ball.transform.localPosition = new Vector3(0 - j, 0.5f, i);
                widthList.Add(ball);
            }

            _balls.Add(widthList);
        }
    }
}