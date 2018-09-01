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
    class PlasmaParticleSystem : ParticleSystem
    {
        public PlasmaParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "plasma_cloud";

            settings.MaxParticles = 50;

            settings.Duration = TimeSpan.FromSeconds(0.35f);
            settings.DurationRandomness = 1;

            settings.MinHorizontalVelocity = 200 / 30;
            settings.MaxHorizontalVelocity = 200 / 30;

            settings.MinVerticalVelocity = -200 / 30;
            settings.MaxVerticalVelocity = 200 / 30;

            settings.Gravity = Vector3.Zero;

            settings.EndVelocity = 0;

            settings.MinColor = Color.White;
            settings.MaxColor = Color.White;

            settings.MinRotateSpeed = -0.1f;
            settings.MaxRotateSpeed = 0.1f;

            settings.MinStartSize = 100;
            settings.MaxStartSize = 125;

            settings.MinEndSize = 125;
            settings.MaxEndSize = 200;

            // Use additive blending.
            settings.BlendState = BlendState.Additive;
        }
    }
}
