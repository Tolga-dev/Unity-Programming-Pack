using UnityEngine;

namespace PrimaryPlayer.GameEngine
{
    public class KeyCodeManager
    {
        public Keycode Keycode = new Keycode();
    }

    public class Keycode
    {
        // MOVEMENT        
        public readonly KeyCode Left = KeyCode.A;
        public readonly KeyCode Right = KeyCode.D;
        public readonly KeyCode Forward = KeyCode.W;
        public readonly KeyCode Back = KeyCode.S;
        public readonly KeyCode Run = KeyCode.LeftControl;
        public readonly KeyCode Jump = KeyCode.Space;
        public readonly KeyCode Crouch = KeyCode.C;

        // ATTACK
        public readonly KeyCode Shoot = KeyCode.Mouse0;
        public readonly KeyCode SelectTarget = KeyCode.Mouse1;
        public readonly KeyCode Reload = KeyCode.R;
        
        // Inventory
        public readonly KeyCode Inventory = KeyCode.E;

        
        // Mouse
        public readonly int MouseRight = 1;

    }
}