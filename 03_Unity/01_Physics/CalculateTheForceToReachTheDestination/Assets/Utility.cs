using UnityEngine;

public class Utility
{
    public static void Throw(GameObject obj, Vector3 destination)
    {
        var body = obj.GetComponent<Rigidbody>();
        var position = obj.transform.position;
        float distance = Vector3.Distance(position, destination);
        Vector3 direction = (destination - position).normalized;
        
        //We set angle to 45 degrees that eliminates Sin from the formula
        direction.y = 0.5f;         
        
        var force = Mathf.Sqrt(distance * Mathf.Abs(Physics.gravity.y)) * direction * body.mass;
  
        body.AddForce(force, ForceMode.Impulse); 
    }
}
