using UnityEngine;

public class SceneController : MonoBehaviour
{
    private const float BaseEnemySpeed = 3.0f;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _enemySpeed = BaseEnemySpeed;

    private GameObject _enemy;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SpeedChanged, OnSpeedChaged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SpeedChanged, OnSpeedChaged);
    }

    private void Update()
    {
        if (_enemy == null)
        {
            _enemy = Instantiate(_enemyPrefab);
            _enemy.transform.position = new Vector3(0, 1, 0);
            
            float angle = Random.Range(0, 360);

            _enemy.transform.Rotate(0, angle, 0);

            WanderingAI wanderingAI = _enemy.GetComponent<WanderingAI>();
            wanderingAI.SetSpeed(_enemySpeed);
        }
    }

    private void OnSpeedChaged(float value)
    {
        _enemySpeed = BaseEnemySpeed * value;
    }
}
