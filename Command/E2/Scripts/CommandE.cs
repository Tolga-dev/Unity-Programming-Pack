using UnityEngine;

namespace Command.E2.Scripts
{
    public abstract class CommandE
    {
        public abstract void Execute(CommandE command);

        public virtual void Undo(){}
    
    }

    public class Forward : CommandE
    {
        public override void Execute(CommandE command)
        {
            Debug.Log("Forward Execute!");
        }

        public override void Undo()
        {
            Debug.Log("Forward Undo Operation");   
        }
    }

    public class Back : CommandE
    {
        public override void Execute(CommandE command)
        {
            Debug.Log("Back Execute!");
        }

        public override void Undo()
        {
            Debug.Log("Back Undo Operation");   
        } 
    }

    public class DoNothing : CommandE
    {
        public override void Execute(CommandE command)
        {
        
            Debug.Log("Nothing Execute!");
        }
 
    }
}