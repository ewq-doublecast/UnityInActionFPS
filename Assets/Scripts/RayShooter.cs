using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class RayShooter : MonoBehaviour
{
    private const string CentralSymbol = "*";

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);

            Ray ray = _camera.ScreenPointToRay(point);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                ReactiveTarget reactiveTarget = hitObject.GetComponent<ReactiveTarget>();

                if (reactiveTarget != null)
                {
                    reactiveTarget.ReactToHit();

                    Messenger.Broadcast(GameEvent.EnemyHit);
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }

            }
        }
    }

    private void OnGUI()
    {
        int size = 12;
        float positionX = _camera.pixelWidth / 2 - size / 4;
        float positionY = _camera.pixelHeight / 2 - size / 4;

        GUI.Label(new Rect(positionX, positionY, size, size), CentralSymbol);
    }

    private IEnumerator SphereIndicator(Vector3 point)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = point;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
