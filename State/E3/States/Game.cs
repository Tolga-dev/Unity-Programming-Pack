using UnityEngine;

namespace State.E3
{
    public class Game : MenuStateBase
    {
        
        public Game(MenuController currentMenuController) : base(currentMenuController)
        {
            states = MenuController.Menus.Game;
        }
        
    }
}