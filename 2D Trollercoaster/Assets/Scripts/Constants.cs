using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static class Animations
    {
        public const string Run = "isRunning";
        public const string Jump = "isJumping";
        public const string Landed = "isLanded";
        public const string Died = "died";
    }

    public static class Layers
    {
        public const string Ground = "Ground";
    }

    public static class Others
    {
        public const float PlayerMovementThreshold = 0.01f;
    }


}
