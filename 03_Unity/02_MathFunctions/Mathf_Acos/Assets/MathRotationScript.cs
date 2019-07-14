using UnityEngine;

public class MathRotationScript : MonoBehaviour
{
    [SerializeField] Transform Target;
    
    void Update ()
    {
        if(Target == null) 
            return;
        
        Vector3 normPos = Target.position.normalized;  
        float Angle = Mathf.Acos(normPos.z) * Mathf.Rad2Deg;
        //Mathf.Acos returns the angle in a Range [0 .. 180] degrees.
        //To deal with that we simply invert the angle sign if the X position is less than 0 
        if(normPos.x < 0)
        {
            Angle = -Angle;
        }
        
        transform.Rotate(Vector3.up, Angle - transform.rotation.eulerAngles.y);
    }    
}
