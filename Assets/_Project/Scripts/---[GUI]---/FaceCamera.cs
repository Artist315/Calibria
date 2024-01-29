using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 dir = transform.position - _mainCamera.transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z));
    }
}
