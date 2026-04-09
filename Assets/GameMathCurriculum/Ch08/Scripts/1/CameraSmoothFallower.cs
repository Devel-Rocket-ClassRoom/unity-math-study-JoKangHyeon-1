using UnityEngine;

public class CameraSmoothFallower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 targetPosition = target.position + (-target.forward * offset.x) + new Vector3(0, offset.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);

        Quaternion targetRotation = Quaternion.LookRotation((target.position - transform.position).normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);
    }
}
