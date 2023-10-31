using Pathfinding;
using UnityEngine;

public abstract class NavigationBase : MonoBehaviour, IUnitService
{
    protected UnitController _unitController;

    protected IAstarAI _astarAI;

    // Movement

    public bool IsMoving => _isMoving;
    protected bool _isMoving;

    public abstract void Init(UnitController controller);

    protected virtual void MoveTo(Vector3 pos)
    {
        if (_astarAI != null)
            _astarAI.destination = pos;

        _unitController.ChangeCharacterState(UnitStateEnum.Moving);
    }
}
