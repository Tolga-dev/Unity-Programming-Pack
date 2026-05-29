using UnityEngine;

namespace PrimaryPlayer.PlayerComponentManagers.Inventory
{
    public class ItemAssets : MonoBehaviour
    {
        public static ItemAssets Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        
        public Sprite Gun;
        public GameObject GunPref;
      //  public Transform GunTransform;
      
        public Sprite Ammo;
        public GameObject AmmoPref;
        //public Transform AmmoTransform;

        
    }
}

