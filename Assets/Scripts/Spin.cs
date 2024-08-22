using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.3f;

    public void Update()
    {
        transform.Rotate(0, _speed, 0);
    }
}
