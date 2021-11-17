using UnityEngine;

public class Unit : MonoBehaviour
{
    public BaseFaction UnitFaction;
    public Rigidbody2D Rigidbody2D;
    public Transform Target;
    public string PlanetTag;
    public Unit BaseUnitPrefab;
    public float MoveSpeedScale;
    
    private Vector3 direction;
    
    void Start()
    {
        LookAtTarget();
        direction = Target.transform.position - transform.position;
        Rigidbody2D.velocity = direction.normalized * MoveSpeedScale;
    }

    private void LookAtTarget()
    {
        float angle = 0;

        Vector3 relative = transform.InverseTransformPoint(Target.position);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.CompareTag("EnemyUnit"))
        // {
        //     Destroy(other.gameObject);
        //     Destroy(gameObject);
        // }
    }
}
