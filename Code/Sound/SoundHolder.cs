using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.Design;
using System.IO;
using Microsoft.Xna.Framework.Net;
using System.Xml.Serialization;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace Orb
{
    public class SoundHolder
    {
        public bool HasLoadedInGameSounds = false;
        public bool HasLoadedMenuSounds = false;

        static string[] InGameSounds =
        {
            "gun_sounds/machine_grenade_launch",
            "gun_sounds/machine_grenade_explosion",
            "gun_sounds/machine_shot0",
            "gun_sounds/machine_shot1",
            "gun_sounds/machine_shot2",
            "gun_sounds/machine_shot3",
            "gun_sounds/bullet_impact_player0",
            "gun_sounds/bullet_impact_player1",
            "gun_sounds/bullet_impact_wall0",
            "gun_sounds/bullet_impact_wall1",
            "gun_sounds/bullet_impact_wall2",
           
            "gun_sounds/plasma_explosion",
            "gun_sounds/plasma_grenade_launch",
            "gun_sounds/plasma_hit_player0",
            "gun_sounds/plasma_hit_player1",
            "gun_sounds/plasma_hit_wall0",
            "gun_sounds/plasma_hit_wall1",
            "gun_sounds/plasma_shot0",
            "gun_sounds/plasma_shot1",
            "gun_sounds/plasma_shot2",

             "gun_sounds/laser_shot0",
             "gun_sounds/laser_shot1",
             "gun_sounds/laser_grenade_explode",
             "gun_sounds/laser_grenade_launch",

             "gun_sounds/rocket_launch",
             "gun_sounds/rocket_launch2",

            
             "ability_sounds/shield_sound",
             "ability_sounds/shield_colapse",
             "ability_sounds/dart_sounds",
             "ability_sounds/cloak_sound",
             "ability_sounds/turret_activate",
            


            "player_sounds/player_dash",
            "player_sounds/player_walk0",
            "player_sounds/player_walk1",
            "player_sounds/player_walk2",
            "player_sounds/player_dash_reverse",
            "player_sounds/player_gain_life",
            "player_sounds/player_pickup_life",
            "player_sounds/player_dash_ready",
            "player_sounds/player_die",
            "player_sounds/player_fail",


            "comentator/assasin",
            "comentator/deathmatch",
            "comentator/five_to_win",
            "comentator/game_over",
            "comentator/one_to_win",
            "comentator/taken_the_lead",
            "comentator/team_deathmatch",
            "comentator/ten_to_win",
            "comentator/three_to_win",
            "comentator/tied_the_leader",
            "comentator/two_to_win",
            "comentator/warlord",
            "comentator/lost_the_lead",
            "comentator/flag_taken",
            "comentator/flag_dropped",
            "comentator/downgrade",
            "comentator/keepaway"
        };

        static string[] MenuSounds =
        {
            "menu/menu_move",
            "menu/menu_select",
            "menu/menu_back",
             "ability_sounds/energy_sound",
             "comentator/bad_rabbit"
        };

        public Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();


        public void LoadInGameSounds(Game1 Game)
        {
            if (!HasLoadedInGameSounds)
            {
                foreach (string soundName in InGameSounds)
                {
                    soundEffects.Add(Path.GetFileNameWithoutExtension(soundName), Game.Content.Load<SoundEffect>("sound_effects/" + soundName));
                }
             
                HasLoadedInGameSounds = true;
            }
        }

        public void LoadMenuSounds(Game1 Game)
        {
            if (!HasLoadedMenuSounds)
            {
                foreach (string soundName in MenuSounds)
                {
                    soundEffects.Add(Path.GetFileNameWithoutExtension(soundName), Game.Content.Load<SoundEffect>("sound_effects/" + soundName));
                }

                HasLoadedMenuSounds = true;
            }
        }

    }
}
