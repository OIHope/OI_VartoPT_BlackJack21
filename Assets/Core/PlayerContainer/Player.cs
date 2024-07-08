using System.Collections;
using UnityEngine;

namespace Assets.Core.PlayerContainer
{
    public abstract class Player : MonoBehaviour
    {
        public abstract IEnumerator StartTurn();
    }
}