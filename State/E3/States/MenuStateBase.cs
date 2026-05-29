using System;
using UnityEngine;

namespace State.E3
{
    public class MenuStateBase : MonoBehaviour
    {

        public MenuController.Menus states;
        public MenuController menuController;

        public MenuStateBase(MenuController currentMenuController)
        {
            menuController = currentMenuController;
        }

        public void Execute()
        {
            
        }

        public void Exit()
        {
            
        }
        
        


    }
}
