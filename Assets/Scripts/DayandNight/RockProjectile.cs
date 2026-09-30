using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    public float speed = 7f;
    public Vector2 direction;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime); //throw rock forward
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DayNightEnemy dayNightEnemy = other.GetComponent<DayNightEnemy>();
        if (dayNightEnemy != null)
        {
            dayNightEnemy.Stun();
            Destroy(gameObject); //destroy the thrown rock
        }
    }
}
