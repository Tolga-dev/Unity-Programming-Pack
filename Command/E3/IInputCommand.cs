namespace Command.E3
{
  public interface IInputCommand
  {
    void Execute(Player plObject);
  }

  public class RotateRightCommand : IInputCommand
  {
    public void Execute(Player plObject)
    {
      plObject.RotateRight();
    }
    
  }

  public class RotateLeftCommand : IInputCommand
  {
    public void Execute(Player plObject)
    {
      plObject.RotateLeft();
    }
  }

  public class RunUpCommand : IInputCommand
  {
    public void Execute(Player plObject)
    {
      plObject.RunForward();
    }
  }

  public class RunBackCommand : IInputCommand
  {
    public void Execute(Player plObject)
    {
      plObject.RunBack();
    }
  }

  public class JumpCommand : IInputCommand
  {
    public void Execute(Player plObject)
    {
      plObject.Jump();
    }
  }
}