#region File Description
//-----------------------------------------------------------------------------
// ExplosionParticleSystem.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Orb
{
    /// <summary>
    /// Custom particle system for creating the fiery part of the explosions.
    /// </summary>
    class SmokeParticleSystem : ParticleSystem
    {
        public SmokeParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "smoke";

            settings.MaxParticles = 400;

            settings.Duration = TimeSpan.FromSeconds(2f);
            settings.DurationRandomness = 1;

            settings.MinHorizontalVelocity = 40;
            settings.MaxHorizontalVelocity = 40;

            settings.MinVerticalVelocity = -40;
            settings.MaxVerticalVelocity = 40;

            settings.EndVelocity = 0;

            settings.Gravity = Vector3.Zero;

            settings.MinColor = Color.White;
            settings.MaxColor = Color.White;

            settings.MinRotateSpeed = -1;
            settings.MaxRotateSpeed = 1;

            settings.MinStartSize = 70;
            settings.MaxStartSize = 70;

            settings.MinEndSize = 100;
            settings.MaxEndSize = 200;

             //Use additive blending.
            //settings.BlendState = BlendState
        }
    }
}
