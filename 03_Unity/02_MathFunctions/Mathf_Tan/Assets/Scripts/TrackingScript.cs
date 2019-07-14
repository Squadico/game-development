using System;
using UnityEngine;

public class TrackingScript : MonoBehaviour
{
    public Action EnemyDetected;
    
    void FixedUpdate()
    {
        RaycastHit hit;
        //Can use layer mask to filter detected objects 
        if (Physics.Raycast(transform.position, transform.forward, out hit, 4.25f))
        {
            //More filtering can be added here, e.g. checking tags , etc.  
            
            float angle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
            if(Mathf.Cos(angle) > 0)
            {
                if(Mathf.Tan(angle) < 0)
                {
                    Debug.Log("Zone 1");
                }
                else
                {
                    Debug.Log("Zone 2");
                }
            }
            else                                
            {
                if(Mathf.Tan(angle) < 0)     
                {
                    Debug.Log("Zone 3");
                }
                else
                {
                    Debug.Log("Zone 4");
                }
            }
            
            //Notifying camera above to stop rotation (in our particular case)
            EnemyDetected?.Invoke();
        }
        Debug.DrawRay(transform.position, transform.forward * 4.25f, Color.red, 0.05f); 
    }
}
