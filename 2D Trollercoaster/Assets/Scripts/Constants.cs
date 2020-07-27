using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static class Animations
    {
        //bools
        public const string Run = "isRunning";
        public const string Jump = "isJumping";
        public const string Landed = "isLanded";

        //triggers
        public const string Died = "died";
        public const string Touched = "touched";
        public const string Collected = "collected";
        public const string Hurry = "hurry";
    }

    public static class Layers
    {
        public const string Ground = "Ground";
        public const string Enemies = "Enemies";
        public const string Background = "Background";
    }

    public static class Tags
    {
        public const string AudioListener = "AudioListener";
        public const string Canvas = "Canvas";
    }

    public static class Others
    {
        public const float PlayerMovementThreshold = 0.01f;
    }
}