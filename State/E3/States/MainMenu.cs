using UnityEngine;

namespace State.E3
{
    public class MainMenu : MenuStateBase
    {
        
        public MainMenu(MenuController currentMenuController) : base(currentMenuController)
        {
            states = MenuController.Menus.Main;
        }
        
    }
}