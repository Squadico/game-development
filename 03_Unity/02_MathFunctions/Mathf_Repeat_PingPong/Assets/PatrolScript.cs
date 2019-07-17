using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    float speed = 1;
    float distance = 5;
    void Update ()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, distance), transform.position.y, transform.position.z);
    }
}
