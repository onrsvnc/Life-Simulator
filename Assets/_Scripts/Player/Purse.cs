using System;
using UnityEngine;

namespace Player
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] float startingBalance = 250000f;

        float balance = 0;

        public event Action onChange;

        private void Awake() 
        {
            balance = startingBalance;
        }

        public float GetBalance()
        {
            return balance;
        }

        public void UpdateBalance(float amount)
        {
            balance += amount;
            if(onChange != null)
            {
                onChange();
            }
        }

    }

}

