using UnityEngine;

public class AITargetController : MonoBehaviour, ITargetController
{
    private UnitController _unitController;

    public void Init(UnitController controller)
    {
        _unitController = controller;
    }
}
