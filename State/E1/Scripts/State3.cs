using UnityEngine;

namespace State.E1.Scripts
{
    public class State3 : MonoBehaviour
    {
        public enum UI
        {
            Help,
            MainMenu
        }
        private UIState _activeState;
        private GameUI _gameState;
        private Help _help;
    
        public void SetActiveState(UI ui)
        {
            _activeState.State = ui;
        }
    }

    public class UIState : MonoBehaviour
    {
        public State3.UI State { get; set; }

        public State3 state3;

        public virtual void InitState(State3 stat)
        {
            this.state3 = stat;
        }
    
    }

    public class GameUI : UIState
    {
        public override void InitState(State3 stat)
        {
            base.InitState(stat);
            State = State3.UI.MainMenu;
            Debug.Log("Main Menu");
        }

        public void ToHelp()
        {
            Debug.Log("Help");
            state3.SetActiveState(State3.UI.Help);
        }
    
    }

    public class Help : UIState
    {
        public override void InitState(State3 stat)
        {
            base.InitState(stat);
            State = State3.UI.Help;
            Debug.Log("Help Menu");
        }
    
    }
}