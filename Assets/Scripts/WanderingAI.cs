using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    private const float BaseSpeed = 3.0f;

    [SerializeField]
    private float _speed = BaseSpeed;

    [SerializeField]
    private float _obstacleRange = 5.0f;

    [SerializeField]
    private GameObject _fireballPrefab;

    private bool _isAlive;

    private GameObject _fireball;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SpeedChanged, OnSpeedChaged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SpeedChanged, OnSpeedChaged);
    }

    private void Start()
    {
        _isAlive = true;
    }

    private void Update()
    {
        if (_isAlive)
        {
            transform.Translate(0, 0, _speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.SphereCast(ray, 0.75f, out RaycastHit hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null)
                    {
                        GameObject fireball = Instantiate(_fireballPrefab);

                        fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < _obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetIsAlive(bool isAlive)
    {
        _isAlive = isAlive;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void OnSpeedChaged(float value)
    {
        _speed = BaseSpeed * value;
    }
}
