using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace FlyWieght.E1.Scripts
{
    public class SubTree
    {
        public List<Vector3> LeafPosition;
    }
 
    public class Tree : MonoBehaviour
    {
        private SubTree _subtree;
        private readonly List<SubTree> _allTrees = new List<SubTree>();
    
        public int densityOfLeaf;
        public int posLeaf;

        public void Start()
        {
            var leafPosition = GetLeafsPosition();

            for (int i = 0; i < densityOfLeaf; i++)
            {
                var tree = new SubTree
                {
                    LeafPosition = leafPosition
                };
                _allTrees.Add(tree);
            }
            
        }

        private void GetTree()
        {
            foreach (var tree in _allTrees)
            {
                Debug.Log(tree.LeafPosition.Count);
            }
        }

        private List<Vector3> GetLeafsPosition()
        {
            var leafsPos = new List<Vector3>();

            for (int i = 0; i < posLeaf; i++)
            {
                leafsPos.Add(new Vector3());
            }

            return leafsPos;
        }
    

    }
}