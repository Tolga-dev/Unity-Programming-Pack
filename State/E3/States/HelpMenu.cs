using UnityEngine;

namespace State.E3
{
    public class HelpMenu : MenuStateBase
    {
        
        public HelpMenu(MenuController currentMenuController) : base(currentMenuController)
        {
            states = MenuController.Menus.Help;
        }
        
    }
}