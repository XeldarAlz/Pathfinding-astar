using UnityEngine;

public class TargetController : MonoBehaviour, ITargetController
{
    private UnitController _unitController;
    private InputController _inputController;

    public Transform Target => _target;
    private Transform _target;

    [SerializeField] private float _attackRange = 4f;

    public void Init(UnitController controller)
    {
        _unitController = controller;
        _unitController.GetService(out _inputController);

        _inputController.OnOtherCharacterHit += OnCharacterSelected;
        _inputController.OnTerrainHit += OnTerrainHit;
    }

    private void OnTerrainHit(Vector3 point)
    {
        _target = null;
    }

    private void Update()
    {
        if (_target == null) // TODO: if target gets killed - remember its last position
            return;

        float targetDistance = (_target.position - transform.position).sqrMagnitude;

        if (targetDistance <= _attackRange * _attackRange)
        {
            _unitController.ChangeCharacterState(UnitStateEnum.Attacking);
        }
        else
        {
            _unitController.ChangeCharacterState(UnitStateEnum.Moving);
        }
    }

    private void OnCharacterSelected(Collider targetColl)
    {
        _target = targetColl.transform;
    }
}
