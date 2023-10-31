using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour, IUnitService
{
    private static readonly int MOVE_PARAM_HASH = Animator.StringToHash("Move");
    private static readonly int ATTACK_PARAM_HASH = Animator.StringToHash("Attack");

    private UnitController _unitController;
    
    private Animator _animator;

    public event Action OnAttack;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        if (_unitController != null)
        {
            _unitController.OnUnitStateChanged -= OnCharacterStateChanged;
        }
    }

    void IUnitService.Init(UnitController controller)
    {
        _unitController = controller;

        _unitController.OnUnitStateChanged += OnCharacterStateChanged;
    }

    private void OnCharacterStateChanged(UnitStateEnum state)
    {
        ResetAnimationStates();
        
        switch (state)
        {
            case UnitStateEnum.Idle:
            {
                TriggerParam(MOVE_PARAM_HASH,false);
                break;
            }
            case UnitStateEnum.Moving:
            {
                TriggerParam(MOVE_PARAM_HASH,true);
                break;
            }
            case UnitStateEnum.Attacking:
            {
                TriggerParam(ATTACK_PARAM_HASH,true);
                break;
            }
        }
    }

    private void ResetAnimationStates()
    {
        TriggerParam(MOVE_PARAM_HASH,false);
        TriggerParam(ATTACK_PARAM_HASH,false);
    }

    private void TriggerParam(int id, bool value)
    {
        _animator.SetBool(id, value);
    }

    public void Attack_Event()
    {
        OnAttack?.Invoke();
    }
}
