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
    class SparkParticleSystem : ParticleSystem
    {
        public SparkParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "spark";

            settings.MaxParticles = 100;

            settings.Duration = TimeSpan.FromSeconds(0.35f);
            settings.DurationRandomness = 1;

            settings.MinHorizontalVelocity = 800;
            settings.MaxHorizontalVelocity = 1600;

            settings.MinVerticalVelocity = 300;
            settings.MaxVerticalVelocity = 1200;

            settings.Gravity = new Vector3(0, -100f, 0);

            settings.EndVelocity = 0;

            settings.MinColor = Color.White;
            settings.MaxColor = Color.White;

            settings.MinRotateSpeed = -1;
            settings.MaxRotateSpeed = 1;

            settings.MinStartSize = 50;
            settings.MaxStartSize = 50;

            settings.MinEndSize = 140;
            settings.MaxEndSize = 200;

            // Use additive blending.
            settings.BlendState = BlendState.Additive;
        }
    }
}
