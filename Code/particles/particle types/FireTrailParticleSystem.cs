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
    class FireParticleSystem : ParticleSystem
    {
        public FireParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "fire_cloud";

            settings.MaxParticles = 100;

            settings.Duration = TimeSpan.FromSeconds(0.5f);
            settings.DurationRandomness = 1;

            settings.MinHorizontalVelocity = 200 / 2;
            settings.MaxHorizontalVelocity = 200 / 2;

            settings.MinVerticalVelocity = -200 / 2;
            settings.MaxVerticalVelocity = 200 / 2;

            settings.Gravity = Vector3.Zero;

            settings.EndVelocity = 0;

            settings.MinColor = Color.White;
            settings.MaxColor = Color.White;

            settings.MinRotateSpeed = -1;
            settings.MaxRotateSpeed = 1;

            settings.MinStartSize = 150;
            settings.MaxStartSize = 150;

            settings.MinEndSize = 150;
            settings.MaxEndSize = 250;

            // Use additive blending.
            settings.BlendState = BlendState.Additive;
        }
    }
}
