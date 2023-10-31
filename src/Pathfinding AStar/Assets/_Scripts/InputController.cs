using System;
using UnityEngine;

public class InputController : MonoBehaviour, IUnitService
{
    private GameObject _target_Debug;

    private UnitController _unitController;

    private Ray _ray;
    private RaycastHit[] _hits;
    private Camera _mainCamera;

    public event Action<Vector3> OnTerrainHit;
    public event Action<Collider> OnOtherCharacterHit;

    private void Awake()
    {
        _mainCamera = Camera.main;

        if (_target_Debug == null)
        {
            _target_Debug = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _target_Debug.transform.localScale = Vector3.one;

            var coll = _target_Debug.GetComponent<Collider>();

            if (coll != null)
            {
                Destroy(coll);
            }
        }
    }

    public void Init(UnitController controller)
    {
        _unitController = controller;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse1))
            return;

        _hits = new RaycastHit[5];

        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        var hitsCount = Physics.RaycastNonAlloc(_ray, _hits);

        if (hitsCount == 0)
            return;

        Vector3 terrainHitPoint = Vector3.zero;

        for (int i = 0; i < hitsCount; i++)
        {
            var hit = _hits[i];

            switch (hit.transform.gameObject.layer)
            {
                case 10: // Unit
                    {
                        if (hit.collider == _unitController.Collider)
                            break;

                        OnOtherCharacterHit?.Invoke(hit.collider);

                        _target_Debug.transform.position = hit.point;

                        return;
                    }
                case 11: // Terrain
                    {
                        terrainHitPoint = hit.point;

                        _target_Debug.transform.position = hit.point;

                        break;
                    }
            }
        }

        if (terrainHitPoint != Vector3.zero)
        {
            OnTerrainHit?.Invoke(terrainHitPoint);
        }
    }
}