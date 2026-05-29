namespace Command.E1.Scripts
{
    public interface ICommand
    {
        public void Execute();
        public void ExecuteUndo();
    }
    
}