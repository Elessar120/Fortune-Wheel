using DefaultNamespace;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Prize", menuName = "New Prize", order = 0)]
    public class PrizeData : ScriptableObject
    {
        public int ID;
        public PrizeType Type;
        public int Amount;
    }
}