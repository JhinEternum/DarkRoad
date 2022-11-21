using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum CurrentPlayerState { IDLE, JUMP, ATTACK }
    public static CurrentPlayerState currentPlayerState = CurrentPlayerState.IDLE;
}
