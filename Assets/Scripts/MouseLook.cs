using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";
    
    [SerializeField]
    private RotationAxes _axes = RotationAxes.MouseXAndY;

    [SerializeField]
    private float _sensitivityHorizontal = 9.0f;

    [SerializeField]
    private float _sensitivityVertical = 9.0f;

    [SerializeField]
    private float _minimumVertical = -45.0f;

    [SerializeField]
    private float _maximumVertical = 45.0f;

    private float _rotationX = 0;

    private void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        } 
    }

    private void Update()
    {
        if (_axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis(MouseX) * _sensitivityHorizontal, 0);
        }
        else if (_axes == RotationAxes.MouseY)
        {
            Rotate();
        }
        else
        {
            float delta = Input.GetAxis(MouseX) * _sensitivityHorizontal;

            Rotate(delta);
        }
    }

    private void Rotate(float delta = 0)
    {
        _rotationX -= Input.GetAxis(MouseY) * _sensitivityVertical;
        _rotationX = Mathf.Clamp(_rotationX, _minimumVertical, _maximumVertical);

        float rotationY = transform.localEulerAngles.y + delta;

        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }
}
