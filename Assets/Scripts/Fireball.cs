using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;

    [SerializeField]
    private int _damage = 1;

    private void Update()
    {
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        if (player != null)
        {
            Debug.Log("Player hit");
            player.Hurt(_damage);
        }
        
        Destroy(gameObject);
    }
}
