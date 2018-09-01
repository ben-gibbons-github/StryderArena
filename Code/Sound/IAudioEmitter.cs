#region File Description
//-----------------------------------------------------------------------------
// IAudioEmitter.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace Orb
{
    /// <summary>
    /// Interface used by the AudioManager to look up the position
    /// and velocity of entities that can emit 3D sounds.
    /// </summary>
    public class IAudioEmitter
    {
        public Vector3 Position;
        public Vector3 Forward=Vector3.Forward;
        public Vector3 Up=Vector3.Up;
        public Vector3 Velocity=Vector3.Zero;
        public bool Taken = false;
    }
}
