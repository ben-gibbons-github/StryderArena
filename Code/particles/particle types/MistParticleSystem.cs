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
    class MistParticleSystem : ParticleSystem
    {
        public MistParticleSystem(Game game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "spark";

            settings.MaxParticles = 10;

            settings.Duration = TimeSpan.FromSeconds(5);
            settings.DurationRandomness = 1;

            settings.MinHorizontalVelocity = 200 / 3;
            settings.MaxHorizontalVelocity = 200 / 3;

            settings.MinVerticalVelocity = -200 / 3;
            settings.MaxVerticalVelocity = 200 / 3;

            settings.Gravity = Vector3.Zero;

            settings.EndVelocity = 0;

            settings.MinColor = Color.Aqua;
            settings.MaxColor = Color.Aqua;

            settings.MinRotateSpeed = -0.0f;
            settings.MaxRotateSpeed = 0.0f;

            float size = 1400;

            settings.MinStartSize = size;
            settings.MaxStartSize = size;

            settings.MinEndSize = size*2;
            settings.MaxEndSize = size*2;

            // Use additive blending.
            settings.BlendState = BlendState.Additive;
        }
    }
}
