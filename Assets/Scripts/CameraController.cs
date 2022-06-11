using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; set; }
    
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void OnFinish()
    {
        virtualCamera.Follow = null;
        virtualCamera.LookAt = null;
    }
}