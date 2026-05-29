using System;
using System.Collections.Generic;
using PrimaryPlayer.PlayerComponentManagers.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Chest.ChestManagers
{
    
    public class ChestController : MonoBehaviour
    {
        
        // chest -> save to json items + load items in parallel    
        // big chest -> 10 item + usual(99%) and rare(1%) items
        // small chest -> 5 item + usual items
        // alien chest -> 2 item + alien items
        // rare chest -> 5 item + rare items
        
        [SerializeField] InputField nameInput;
        [SerializeField] string filename;

        List<InputEntry> entries = new List<InputEntry> ();
        private Random _random = new Random();
        public Canvas chestCanvas;
        public List<ItemWorldController> itemControllers;
        
        private void Start () {
            entries = FileHandler.ReadListFromJSON<InputEntry> (filename);
        }

        public void AddNameToList () {
            entries.Add (new InputEntry (nameInput.text, _random.Next(0, 100)));
            nameInput.text = "";

            FileHandler.SaveToJSON<InputEntry> (entries, filename);
        }
        

    }

    [Serializable]
    public class InputEntry {
        public string playerName;
        public int points;

        public InputEntry (string name, int points) {
            playerName = name;
            this.points = points;
        }
    }
     
    
}