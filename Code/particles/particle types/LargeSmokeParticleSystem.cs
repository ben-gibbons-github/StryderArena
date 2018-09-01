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
    class LargeSmokeParticleSystem : ParticleSystem
    {
        public LargeSmokeParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "smoke";

            settings.MaxParticles = 20;

            settings.Duration = TimeSpan.FromSeconds(2f);
            settings.DurationRandomness = 1;

            settings.MinHorizontalVelocity = 400;
            settings.MaxHorizontalVelocity = 400;

            settings.MinVerticalVelocity = -400;
            settings.MaxVerticalVelocity = 400;

            settings.EndVelocity = 0;

            settings.Gravity = Vector3.Zero;

            settings.MinColor = Color.White;
            settings.MaxColor = Color.White;

            settings.MinRotateSpeed = -1;
            settings.MaxRotateSpeed = 1;

            settings.MinStartSize = 300;
            settings.MaxStartSize = 300;

            settings.MinEndSize = 1500;
            settings.MaxEndSize = 1500;

            //Use additive blending.
            //settings.BlendState = BlendState
        }
    }
}
