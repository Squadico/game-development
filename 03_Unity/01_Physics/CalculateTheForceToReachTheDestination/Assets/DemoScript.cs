using System.Collections;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    [SerializeField] GameObject Target;
    [SerializeField] GameObject Projectile;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        Utility.Throw(Projectile, Target.GetComponent<Transform>().position);
    }
}
