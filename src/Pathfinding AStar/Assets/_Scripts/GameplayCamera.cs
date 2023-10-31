using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        if (_target == null)
            return;

        transform.position = _target.position;
    }
}
