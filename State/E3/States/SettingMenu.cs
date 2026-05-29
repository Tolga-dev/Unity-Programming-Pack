using UnityEngine;

namespace State.E3
{
    public class SettingMenu : MenuStateBase
    {
        
        public SettingMenu(MenuController currentMenuController) : base(currentMenuController)
        {
            states = MenuController.Menus.Main;
        }
        
    }
}