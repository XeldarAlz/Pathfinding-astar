using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(IAstarAI))]
public class PlayerAgentController : NavigationBase
{
    private InputController _inputController;

    private void OnDestroy()
    {
        Unsubscribe();
    }

    public override void Init(UnitController controller)
    {
        _unitController = controller;
        _unitController.GetService(out _inputController);

        _isMoving = false;

        _astarAI = GetComponent<IAstarAI>();

        Subscribe();
    }

    private void Subscribe()
    {
        _inputController.OnTerrainHit += OnTerrainHit;
        _inputController.OnOtherCharacterHit += OnCharacterHit;

        _unitController.OnUnitStateChanged += OnCharacterStateChanged;
    }

    private void Unsubscribe()
    {
        if (_inputController != null)
        {
            _inputController.OnTerrainHit -= OnTerrainHit;
            _inputController.OnOtherCharacterHit -= OnCharacterHit;
        }

        if (_unitController != null)
        {
            _unitController.OnUnitStateChanged -= OnCharacterStateChanged;
        }
    }

    private void Update()
    {
        CheckMovement();
    }

    private void OnCharacterStateChanged(UnitStateEnum state)
    {
        if (state == UnitStateEnum.Attacking)
        {
            _isMoving = false;
        }
    }

    private void OnTerrainHit(Vector3 point)
    {
        MoveTo(point);
    }

    private void OnCharacterHit(Collider characterColl)
    {
        MoveTo(characterColl.transform.position);
    }

    private void Completed(Path p)
    {
        Debug.Log("path completed");
        _unitController.ChangeCharacterState(UnitStateEnum.Idle);
    }

    private void CheckMovement()
    {
        // bool isMoving_New = _seeker;
        //
        // if (_isMoving != isMoving_New)
        // {
        //     _isMoving = isMoving_New;
        //
        //     if (!_isMoving)
        //     {
        //         _unitController.ChangeCharacterState(UnitStateEnum.Idle);
        //     }
        // }
    }
}