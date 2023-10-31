using System;
using System.Collections.Generic;
using UnityEngine;

public enum UnitStateEnum
{
    Idle = 0,
    Moving = 1,
    Attacking = 2
}

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitData _dataPreset;
    [SerializeField] private Collider _collider;

    public Collider Collider => _collider;

    private List<IUnitService> _services = new List<IUnitService>();

    private UnitDataManager _unitDataManager;

    public UnitStateEnum UnitState => _UnitState;
    public UnitStateEnum _UnitState; // Debug public

    public event Action<UnitStateEnum> OnUnitStateChanged;

    private void Start()
    {
        Init(); // TODO
    }

    public void Init()
    {
        Logger.Log($"Initing UnitController for: {gameObject.name}", LogTypeEnum.Service);

        var characterServices = GetComponentsInChildren<IUnitService>();
        _services = new List<IUnitService>(characterServices);

        foreach (var item in _services)
        {
            Logger.Log($"Initing <b>{item.GetType()}</b> service...", LogTypeEnum.Service);
            item.Init(this);
        }

        OnUnitStateChanged?.Invoke(UnitStateEnum.Idle);

        Logger.Log($"All services inited for: {gameObject.name}", LogTypeEnum.Service);

        _unitDataManager = new UnitDataManager(_dataPreset, this);
    }

    public void GetService<T>(out T service) where T : IUnitService
    {
        service = default;

        foreach (var item in _services)
        {
            if (item is T characterService)
            {
                service = characterService;

                break;
            }
        }

        if (service == null)
        {
            Debug.LogError($"Can't get service {typeof(T)} for {gameObject.name}");
        }
    }

    public void ChangeCharacterState(UnitStateEnum state)
    {
        if (_UnitState == state)
            return;

        _UnitState = state;
        OnUnitStateChanged?.Invoke(_UnitState);
    }
}
