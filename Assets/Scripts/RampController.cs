using UnityEngine;

public class RampController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var rigidbody = other.gameObject.GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * -500f);
    }
}