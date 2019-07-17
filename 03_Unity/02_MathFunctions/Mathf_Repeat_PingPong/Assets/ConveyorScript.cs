using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    int distance = 5;
    
    void Update ()
    {
        transform.position = new Vector3(Mathf.Repeat(Time.time, distance), transform.position.y, transform.position.z);
    }
}
