using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Orb
{
    public class SoundManager
    {
        public AudioListener Listener = new AudioListener();
      
        List<ActiveSound> activeSounds = new List<ActiveSound>();

        AudioEmitter emitter = new AudioEmitter();

        public int LifeStealTimer = 0;

        public void Update()
        {
            LifeStealTimer++;

            // Loop over all the currently playing 3D sounds.
            int index = 0;

            while (index < activeSounds.Count)
            {
                ActiveSound activeSound = activeSounds[index];

                if (activeSound.Instance.State == SoundState.Stopped)
                {
                    activeSound.Emitter.Taken = false;
                    // If the sound has stopped playing, dispose it.
                    
                    activeSound.Instance.Dispose();

                    // Remove it from the active list.
                    activeSounds.RemoveAt(index);
                }
                else
                {
                    // If the sound is still playing, update its 3D settings.
                    Apply3D(activeSound);

                    index++;
                }
            }

           
        }



        public IAudioEmitter Play3DSound(Game1 game, SoundEffect soundEffect,Vector3 Position)
        {
            ActiveSound activeSound = new ActiveSound();

            // Fill in the instance and emitter fields.
            activeSound.Instance = soundEffect.CreateInstance();
            

            bool Found=false;
            foreach(IAudioEmitter emit in game.AudioEmtters)
                if(!Found)
                if (!emit.Taken)
                {
                    Found = true;
                    emit.Taken = true;
                    emit.Position = Position;

                    activeSound.Emitter = emit;
                }
           if(!Found)
           {
               IAudioEmitter emit = game.AudioEmtters[game.random.Next(50)];
               Found = true;
               emit.Taken = true;
               emit.Position = Position;

               activeSound.Emitter = emit;
           }
            
           


            // Set the 3D position of this sound, and then play it.
            Apply3D(activeSound);

            activeSound.Instance.Play();

            // Remember that this sound is now active.
            activeSounds.Add(activeSound);

            return activeSound.Emitter;

            //return activeSound.Instance;
        }



        private void Apply3D(ActiveSound activeSound)
        {
            emitter.Position = activeSound.Emitter.Position;
            emitter.Forward = activeSound.Emitter.Forward;
            emitter.Up = activeSound.Emitter.Up;
            emitter.Velocity = activeSound.Emitter.Velocity;

            activeSound.Instance.Apply3D(Listener, emitter);
        }

        class ActiveSound
        {
            public SoundEffectInstance Instance;
            public IAudioEmitter Emitter;
        }


    }

}
