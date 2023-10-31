public interface IAgentController 
{
    
}

public interface ITargetController : IUnitService
{
    
}

public interface IUnitService
{
    public void Init(UnitController controller);
}
