using Pathfinding;

public class AIAgentController : NavigationBase
{
    private Seeker _seeker;

    private void OnDestroy()
    {
        Unsubscribe();
    }

    public override void Init(UnitController controller)
    {
        _unitController = controller;
        _astarAI = GetComponent<IAstarAI>();
        _seeker = GetComponent<Seeker>();

        _isMoving = false;

        Subscribe();

        BeginRandomMovement();
    }

    private void Subscribe()
    {
        _unitController.OnUnitStateChanged += OnCharacterStateChanged;
        _astarAI.onTargetReached += ProcessEndPath;
    }

    private void Unsubscribe()
    {
        if (_unitController != null)
        {
            _unitController.OnUnitStateChanged -= OnCharacterStateChanged;
        }

        if (_astarAI != null)
        {
            _astarAI.onTargetReached -= ProcessEndPath;
        }
    }

    private void OnCharacterStateChanged(UnitStateEnum state)
    {

    }

    private void BeginRandomMovement()
    {
        int theGScoreToStopAt = 50000;
        var path = RandomPath.Construct(transform.position, theGScoreToStopAt);
        path.spread = 5000;

        _seeker.StartPath(path);
    }

    private void ProcessEndPath()
    {
        BeginRandomMovement();
    }
}
