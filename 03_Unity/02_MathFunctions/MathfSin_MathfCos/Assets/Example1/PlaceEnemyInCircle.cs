using UnityEngine;

public class PlaceEnemyInCircle : MonoBehaviour
{
    public Object EnemyPrefab;
    //Distance from Enemy to Player
    public float Distance = 4;
    //Angle around Player that Enemy can be placed
    public float Angle = 360;
    //Enemy's count  
    public int count = 15;

    void Start ()
    {
        Vector3 position = transform.position;
        Angle = Angle * Mathf.Deg2Rad;
        for(int i = 1; i <= count; i++)
        {
            float _z = transform.position.z + Mathf.Cos(Angle/count*i)*Distance;
            float _x = transform.position.x + Mathf.Sin(Angle/count*i)*Distance;
            position.x = _x;
            position.z = _z;
            Instantiate(EnemyPrefab, position, Quaternion.identity);
        }
    }
}
