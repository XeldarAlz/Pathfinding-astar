using System;

public class UnitDataManager
{
    public UnitData UnitData => _unitData;
    private UnitData _unitData;

    private UnitController _unitController;
    private AnimationController _animationController;
    private TargetController targetController;

    public static event Action<UnitController> OnUnitDeath;

    public UnitDataManager(UnitData data, UnitController unitController)
    {
        _unitController = unitController;
        _unitController.GetService(out _animationController);
        // _unitController.GetService(out targetController);

        _unitData = data;

        Subscribe();
    }

    private void Subscribe()
    {
        _animationController.OnAttack += DealDamage;
    }

    private void DealDamage()
    {
        // targetController.Target
    }

    private void OnTakeDamage(int damage)
    {
        if (_unitData.Health <= 0)
            return;

        _unitData.Health -= damage;

        if (_unitData.Health <= 0)
        {
            OnUnitDeath?.Invoke(_unitController);
        }
    }
}