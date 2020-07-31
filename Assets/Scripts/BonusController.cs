using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    public enum BonusType
    {
        Coin = 0, Jump = 1, Speed = 2, Magnet = 3
    }

    public BonusType m_Type;

    private void OnTriggerEnter(Collider other)
    {
        switch (m_Type)
        {
            case BonusType.Coin:
                {
                    UIManager.UpdateCoinInfo();
                    Destroy(gameObject);
                } break;
            case BonusType.Jump:
                {
                    //TODO Jump Bonuss
                } break;
            case BonusType.Speed:
                {
                    //TODO Speed Bonuss
                }
                break;
            case BonusType.Magnet:
                {
                    //TODO Magnet Bonuss
                }
                break;
        }
    }
}
