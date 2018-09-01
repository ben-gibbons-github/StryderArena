using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orb
{
    public class Comentator
    {
        public int KillCount = 0;
        public bool HasSaidWin = false;
        public float ComentatorVolume = 0.3f;
        public bool HasSaidBegin=false;
        public float PlayTimer = 0;
        public const float MaxPlayTimer = 80;
        public bool FlagIsHeld = false;

        public void PlaySound(Game1 game,string SoundName)
        {
            game.soundHolder.soundEffects[SoundName].Play(ComentatorVolume, 0, 0);
            PlayTimer = 0;
        }

        public void Update(Game1 game)
        {
            ComentatorVolume = 0.3f * game.SoundEffectsVolume;

            PlayTimer++;
            //if (PlayTimer > MaxPlayTimer)
            
                if (PlayTimer > MaxPlayTimer)
                {
                    if (game.gamemode == Game1.GameMode.KeepAway)
                    {
                        if (game.flag.IsCarried != FlagIsHeld)
                        {
                            if(game.flag.IsCarried)
                                PlaySound(game, "flag_taken");
                            if (!game.flag.IsCarried)
                                PlaySound(game, "flag_dropped");

                            FlagIsHeld = game.flag.IsCarried;
                        }

                    }


                    int NumbWinning = 0;
                    int BestKills = 0;

                    foreach (BasicOrb orb in game.Orbs)
                        if (orb.relevent)
                        {
                            int oldKills = orb.MyController.Kills;
                            if (game.gamemode == Game1.GameMode.KeepAway)
                                orb.MyController.Kills = orb.MyController.FlagScore;

                            if (orb.MyController.Kills > BestKills)
                            {
                                NumbWinning = 1;
                                BestKills = orb.MyController.Kills;
                            }
                            else if (orb.MyController.Kills == BestKills)
                                NumbWinning++;

                            if (game.gamemode == Game1.GameMode.KeepAway)
                                orb.MyController.Kills = oldKills;
                        }
                    if (PlayTimer > MaxPlayTimer)
                    foreach (LocalPlayer player in game.Localplayers)
                        if (PlayTimer > MaxPlayTimer)
                        if (player.Relevent)
                        {
                            if (player.Kills < BestKills)
                            {
                                if (player.HasSaidLeading || player.HasSaidTied)
                                {
                                    PlaySound(game, "lost_the_lead");
                                    //game.soundHolder.soundEffects["lost_the_lead"].Play(ComentatorVolume, 0, 0);
                                }
                                player.HasSaidLeading = false;
                                player.HasSaidTied = false;
                            }

                            if (player.Kills >= BestKills)
                            {
                                if (NumbWinning == 1 && !player.HasSaidLeading)
                                {
                                    player.HasSaidTied = false;
                                    PlaySound(game, "taken_the_lead");
                                    player.HasSaidLeading = true;
                                    // game.soundHolder.soundEffects["taken_the_lead"].Play(ComentatorVolume, 0, 0);
                                }

                                else if (NumbWinning > 1 && !player.HasSaidTied)
                                {
                                    player.HasSaidLeading = false;
                                    PlaySound(game, "tied_the_leader");
                                    player.HasSaidTied = true;
                                    //game.soundHolder.soundEffects["tied_the_leader"].Play(ComentatorVolume, 0, 0);
                                }
                            }
                        }


                    if (PlayTimer > MaxPlayTimer)
                if (BestKills > KillCount)
                {
                    KillCount++;

                    if (KillCount == game.KillsToWin - 10)
                        PlaySound(game, "ten_to_win");
                    // game.soundHolder.soundEffects["ten_to_win"].Play(ComentatorVolume, 0, 0);
                    if (KillCount == game.KillsToWin - 5)
                        PlaySound(game, "five_to_win");
                    //  game.soundHolder.soundEffects["five_to_win"].Play(ComentatorVolume, 0, 0);
                    if (KillCount == game.KillsToWin - 3)
                        PlaySound(game, "three_to_win");
                    //  game.soundHolder.soundEffects["three_to_win"].Play(ComentatorVolume, 0, 0);
                    if (KillCount == game.KillsToWin - 2)
                        PlaySound(game, "two_to_win");
                    //  game.soundHolder.soundEffects["two_to_win"].Play(ComentatorVolume, 0, 0);
                    if (KillCount == game.KillsToWin - 1)
                        PlaySound(game, "one_to_win");
                    //  game.soundHolder.soundEffects["one_to_win"].Play(ComentatorVolume, 0, 0);
                }

                    if (PlayTimer > MaxPlayTimer)
                if (game.GameOver && !HasSaidWin)
                {
                    HasSaidWin = true;
                    PlaySound(game, "game_over");
                    // game.soundHolder.soundEffects["game_over"].Play(ComentatorVolume, 0, 0);
                }
                    if (PlayTimer > MaxPlayTimer)
                if (!HasSaidBegin)
                {
                    HasSaidBegin = true;
                    if (game.gamemode == Game1.GameMode.Assasin)
                        PlaySound(game, "assasin");
                    //game.soundHolder.soundEffects["assasin"].Play(ComentatorVolume, 0, 0);
                    if (game.gamemode == Game1.GameMode.DeathMatch)
                        PlaySound(game, "deathmatch");
                    // game.soundHolder.soundEffects["deathmatch"].Play(ComentatorVolume, 0, 0);
                    if (game.gamemode == Game1.GameMode.TeamDeathMatch)
                        PlaySound(game, "team_deathmatch");
                    // game.soundHolder.soundEffects["team_deathmatch"].Play(ComentatorVolume, 0, 0);
                    if (game.gamemode == Game1.GameMode.WarLord)
                        PlaySound(game, "warlord");

                    if (game.gamemode == Game1.GameMode.DownGrade)
                        PlaySound(game, "downgrade");

                    if (game.gamemode == Game1.GameMode.KeepAway)
                        PlaySound(game, "keepaway");

                    // game.soundHolder.soundEffects["warlord"].Play(ComentatorVolume, 0, 0);

                }
            }
        }

        public void Reset()
        {
            HasSaidBegin = false;
            KillCount = 0;
            HasSaidWin = false;

        }
        
    }
}
