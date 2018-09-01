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

namespace Orb
{
    public class WeaponHolder
    {
        public bool HasLoaded = false;

        const int GunNumb = 7;
        public float[,] ROF= new float[GunNumb,2];
        public bool[,] UseTarget = new bool[GunNumb, 2];
        public float[,] Accuracy = new float[GunNumb,2];
        public float[,] ClipSize = new float[GunNumb,2];
        public float[,] BurstSize = new float[GunNumb,2];
        public int[,] ShotBulletNumb = new int[GunNumb, 2];
        public Vector3[,] GunLight = new Vector3[GunNumb, 2];
        public Vector3[,] ShotLight = new Vector3[GunNumb, 2];
        public float[,] ShotLightDist = new float[GunNumb, 2];
        public Vector2[,] ShotSize = new Vector2[GunNumb, 2];
        public Texture2D[] GunIcon = new Texture2D[GunNumb];
        public float[,] ReloadTime = new float[GunNumb,2];
        public float[,] BurstTime = new float[GunNumb,2];
        public float[] GunOffset = new float[GunNumb];
        public float[,] StartingAmmo = new float[GunNumb,2];
        public string[,] GunSoundEffect = new string[GunNumb, 2];
        public int[,] GunSoundEffectRange = new int[GunNumb, 2];
        public float[,] ChargeTime = new float[GunNumb, 2];
        public float[,] GainAmmo = new float[GunNumb,2];
        public float[,] BulletSpeed = new float[GunNumb,2];
        public float[,] BulletLifeTIme = new float[GunNumb,2];
        public float[,] BulletPushVelMult = new float[GunNumb, 2];
        public int[,] BulletType = new int[GunNumb,2];
        public Texture2D[] GunFlash = new Texture2D[GunNumb];
        public int[,] BulletExplosionSize = new int[GunNumb, 2];
        public int[,] BulletExplosionDamage = new int[GunNumb, 2];
        public float[,] BulletExplosionPushVelMult = new float[GunNumb, 2];
        public float[,] BulletExplosionPush = new float[GunNumb, 2];
        public float[,] BulletDamage = new float[GunNumb, 2];
        public float[,] BulletPush = new float[GunNumb, 2];
        public float[,] Pause = new float[GunNumb, 2];
        public float[,] ShootPause = new float[GunNumb, 2];
        public float[] AimHelpAmount = new float[GunNumb];
        public bool[,] StopCharge = new bool[GunNumb,2];
        public float[,] ChargeSpeed = new float[GunNumb,2];
        public Model[] GunModel = new Model[GunNumb];

        int Temp = -1;
        int Use = 0;

        const float LightMult = 2;


        public void Load(Game1 game)
        {
            if (!HasLoaded)
            {
                HasLoaded = true;
                //machine gun
                Temp += 1;

                Use = 0;
                GunOffset[Temp] = 40;
                GunFlash[Temp] = game.Content.Load<Texture2D>("machineflash");
                GunModel[Temp] = game.Content.Load<Model>("machinegun");
                GunSoundEffect[Temp, Use] = "machine_shot";
                GunSoundEffectRange[Temp, Use] = 4;
                GunIcon[Temp] = game.Content.Load<Texture2D>("gun_outline/machine_outline");
                ROF[Temp, Use] = 2;
                Accuracy[Temp, Use] = 23;
                ClipSize[Temp, Use] = 40;
                ReloadTime[Temp, Use] = 40;
                ChargeTime[Temp, Use] = 0;
                BurstSize[Temp, Use] = 8;
                BurstTime[Temp, Use] = 25;
                ShotBulletNumb[Temp, Use] = 1;
                ShotSize[Temp, Use] = new Vector2(0, 0);
                StartingAmmo[Temp, Use] = 120;
                GainAmmo[Temp, Use] = 60;
                BulletSpeed[Temp, Use] = 140;
                BulletLifeTIme[Temp, Use] = 8;
                BulletType[Temp, Use] = 0;
                BulletExplosionSize[Temp, Use] = 0;
                BulletExplosionDamage[Temp, Use] = 0;
                BulletExplosionPush[Temp, Use] = 0;
                BulletPushVelMult[Temp, Use] = 0;
                BulletDamage[Temp, Use] = 24;
                BulletPush[Temp, Use] = 0.9f;
                BulletPushVelMult[Temp, Use] = 1f;
                Pause[Temp, Use] = 0;
                ShootPause[Temp, Use] = 5;
                GunLight[Temp, Use] = new Vector3(4, 2, 1) * LightMult;
                ShotLight[Temp, Use] = new Vector3(0.5f, 0.25f, 0.5f);
                ShotLightDist[Temp, Use] = 0;
                AimHelpAmount[Temp] = 1;
                ChargeSpeed[Temp, Use] = 1;
                StopCharge[Temp, Use] = false;
                UseTarget[Temp, Use] = false;

                Use = 1;
                ROF[Temp, Use] = 0;
                Accuracy[Temp, Use] = 0;
                ClipSize[Temp, Use] = 1;
                GunSoundEffect[Temp, Use] = "machine_grenade_launch";
                GunSoundEffectRange[Temp, Use] = 0;
                ChargeTime[Temp, Use] = 20;
                ReloadTime[Temp, Use] = 80;
                ShotBulletNumb[Temp, Use] = 1;
                BurstSize[Temp, Use] = 1;
                BurstTime[Temp, Use] = 0;
                StartingAmmo[Temp, Use] = 8;
                ShotSize[Temp, Use] = new Vector2(0, 0);
                GainAmmo[Temp, Use] = 4;
                BulletSpeed[Temp, Use] = 24;
                BulletLifeTIme[Temp, Use] = 60;
                BulletType[Temp, Use] = 1;
                BulletExplosionSize[Temp, Use] = 475 + 50 + 25;
                BulletExplosionDamage[Temp, Use] = 150;
                BulletExplosionPush[Temp, Use] = 0.65f + 0.1f;
                BulletExplosionPushVelMult[Temp, Use] = 0.65f;
                BulletDamage[Temp, Use] = 0;
                BulletPush[Temp, Use] = 0;
                BulletPushVelMult[Temp, Use] = 0;
                Pause[Temp, Use] = 0;
                ShootPause[Temp, Use] = 10;
                GunLight[Temp, Use] = new Vector3(4, 2, 1) * LightMult;
                ShotLight[Temp, Use] = new Vector3(4, 2, 1) * 0.75f;
                ShotLightDist[Temp, Use] = 350;
                ChargeSpeed[Temp, Use] = 1;
                UseTarget[Temp, Use] = false;
                StopCharge[Temp, Use] = false;




                //plasma gun
                Temp += 1;
                GunOffset[Temp] = 60;
                GunFlash[Temp] = game.Content.Load<Texture2D>("plasmaflash");
                GunModel[Temp] = game.Content.Load<Model>("plasmagun");
                GunIcon[Temp] = game.Content.Load<Texture2D>("gun_outline/plasma_outline");
                Use = 0;
                GunSoundEffect[Temp, Use] = "plasma_shot";
                GunSoundEffectRange[Temp, Use] = 3;
                AimHelpAmount[Temp] = 1;
                ROF[Temp, Use] = 4;
                Accuracy[Temp, Use] = 0;
                ClipSize[Temp, Use] = 40;
                ReloadTime[Temp, Use] = 40;
                ChargeTime[Temp, Use] = 0;
                BurstSize[Temp, Use] = 4;
                BurstTime[Temp, Use] = 25;
                ShotBulletNumb[Temp, Use] = 1;
                ShotSize[Temp, Use] = new Vector2(0, 0);
                StartingAmmo[Temp, Use] = 120;
                GainAmmo[Temp, Use] = 60;
                BulletSpeed[Temp, Use] = 42-3;
                BulletLifeTIme[Temp, Use] = 80 / 2;
                BulletType[Temp, Use] = 4;
                BulletExplosionSize[Temp, Use] = 0;
                BulletExplosionDamage[Temp, Use] = 0;
                BulletExplosionPush[Temp, Use] = 0;
                BulletPushVelMult[Temp, Use] = 0;
                BulletDamage[Temp, Use] = 24 * 4;
                BulletPush[Temp, Use] = 0.9f;
                BulletPushVelMult[Temp, Use] = 1f;
                Pause[Temp, Use] = 0;
                ShootPause[Temp, Use] = 5;
                GunLight[Temp, Use] = new Vector3(1, 4, 2) * LightMult;
                ShotLight[Temp, Use] = new Vector3(0.5f, 2, 1) * 0.75f;
                ShotLightDist[Temp, Use] = 175;
                StopCharge[Temp, Use] = false;
                UseTarget[Temp, Use] = false;

                Use = 1;
                ROF[Temp, Use] = 0;
                GunSoundEffect[Temp, Use] = "plasma_grenade_launch";
                GunSoundEffectRange[Temp, Use] = 0;
                Accuracy[Temp, Use] = 0;
                ClipSize[Temp, Use] = 1;
                ChargeTime[Temp, Use] = 20;
                ReloadTime[Temp, Use] = 80;
                ShotBulletNumb[Temp, Use] = 1;
                BurstSize[Temp, Use] = 1;
                BurstTime[Temp, Use] = 0;
                StartingAmmo[Temp, Use] = 8;
                ShotSize[Temp, Use] = new Vector2(0, 0);
                GainAmmo[Temp, Use] = 4;
                BulletSpeed[Temp, Use] = 24;
                BulletLifeTIme[Temp, Use] = 70;
                BulletType[Temp, Use] = 5;
                BulletExplosionSize[Temp, Use] = 475 + 50 + 25;
                BulletExplosionDamage[Temp, Use] = 70;
                BulletExplosionPush[Temp, Use] = 0.65f + 0.1f;
                BulletExplosionPushVelMult[Temp, Use] = 0.65f;
                BulletDamage[Temp, Use] = 0;
                BulletPush[Temp, Use] = 0;
                BulletPushVelMult[Temp, Use] = 0;
                Pause[Temp, Use] = 0;
                ShootPause[Temp, Use] = 10;
                GunLight[Temp, Use] = new Vector3(1, 4, 2) * LightMult;
                ShotLight[Temp, Use] = new Vector3(1.5f, 3, 2.5f) * 0.25f;
                ShotLightDist[Temp, Use] = 350;
                StopCharge[Temp, Use] = false;
                ChargeSpeed[Temp, Use] = 1;
                UseTarget[Temp, Use] = false;


                //railgun 2 (unused)
                Temp += 1;

                Use = 0;

                AimHelpAmount[Temp] = 1;
                ROF[Temp, Use] = 4;
                Accuracy[Temp, Use] = 0;
                ClipSize[Temp, Use] = 150;
                ReloadTime[Temp, Use] = 0;
                ChargeTime[Temp, Use] = 75;
                BurstSize[Temp, Use] = 5;
                BurstTime[Temp, Use] = 45;
                ShotBulletNumb[Temp, Use] = 1;
                ShotSize[Temp, Use] = new Vector2(0, 0);
                StartingAmmo[Temp, Use] = 10;
                GainAmmo[Temp, Use] = 5;
                BulletSpeed[Temp, Use] = 200;
                BulletLifeTIme[Temp, Use] = 16;
                BulletType[Temp, Use] = 8;
                BulletExplosionSize[Temp, Use] = 0;
                BulletExplosionDamage[Temp, Use] = 0;
                BulletExplosionPush[Temp, Use] = 0;
                BulletPushVelMult[Temp, Use] = 0;
                BulletDamage[Temp, Use] = 140;
                BulletPush[Temp, Use] = 1f;
                BulletPushVelMult[Temp, Use] = 1f;
                Pause[Temp, Use] = 0;
                ShootPause[Temp, Use] = 5;
                GunLight[Temp, Use] = new Vector3(4, 1, 3) * LightMult;
                ShotLight[Temp, Use] = new Vector3(2, 0.5f, 1) * 0.75f;
                ShotLightDist[Temp, Use] = 250;
                ChargeSpeed[Temp, Use] = 0.5f;
                StopCharge[Temp, Use] = true;
                UseTarget[Temp, Use] = true;

                Use = 1;
                ROF[Temp, Use] = 0;
                Accuracy[Temp, Use] = 0;
                ClipSize[Temp, Use] = 1;
                ChargeTime[Temp, Use] = 20;
                ReloadTime[Temp, Use] = 80;
                ShotBulletNumb[Temp, Use] = 1;
                BurstSize[Temp, Use] = 1;
                BurstTime[Temp, Use] = 0;
                StartingAmmo[Temp, Use] = 8;
                ShotSize[Temp, Use] = new Vector2(0, 0);
                GainAmmo[Temp, Use] = 4;
                BulletSpeed[Temp, Use] = 24;
                BulletLifeTIme[Temp, Use] = 30;
                BulletType[Temp, Use] = 5;
                BulletExplosionSize[Temp, Use] = 475 + 50 + 25;
                BulletExplosionDamage[Temp, Use] = 70;
                BulletExplosionPush[Temp, Use] = 0.65f + 0.1f;
                BulletExplosionPushVelMult[Temp, Use] = 0.65f;
                BulletDamage[Temp, Use] = 0;
                BulletPush[Temp, Use] = 0;
                BulletPushVelMult[Temp, Use] = 0;
                Pause[Temp, Use] = 0;
                ShootPause[Temp, Use] = 10;
                GunLight[Temp, Use] = new Vector3(1, 4, 2) * LightMult;
                ShotLight[Temp, Use] = new Vector3(1, 4, 2) * 0.25f;
                ShotLightDist[Temp, Use] = 350;
                StopCharge[Temp, Use] = false;
                ChargeSpeed[Temp, Use] = 1;



                //railgun (real)
                Temp += 1;
                GunOffset[Temp] = 80;
                GunFlash[Temp] = game.Content.Load<Texture2D>("laserflash");
                GunModel[Temp] = game.Content.Load<Model>("laserrifle");
                GunIcon[Temp] = game.Content.Load<Texture2D>("gun_outline/laser_outline");
                Use = 0;
                GunSoundEffect[Temp, Use] = "laser_shot";
                GunSoundEffectRange[Temp, Use] = 2;

                AimHelpAmount[Temp] = 1;
                ROF[Temp, Use] = 3;
                Accuracy[Temp, Use] = 16;
                ClipSize[Temp, Use] = 150;
                ReloadTime[Temp, Use] = 0;
                ChargeTime[Temp, Use] = 0;
                BurstSize[Temp, Use] = 4;
                BurstTime[Temp, Use] = 30;
                ShotBulletNumb[Temp, Use] = 1;
                ShotSize[Temp, Use] = new Vector2(0, 0);
                StartingAmmo[Temp, Use] = 10;
                GainAmmo[Temp, Use] = 5;
                BulletSpeed[Temp, Use] = 120;
                BulletLifeTIme[Temp, Use] = 14;
                BulletType[Temp, Use] = 8;
                BulletExplosionSize[Temp, Use] = 0;
                BulletExplosionDamage[Temp, Use] = 0;
                BulletExplosionPush[Temp, Use] = 0;
                BulletPushVelMult[Temp, Use] = 0;
                BulletDamage[Temp, Use] = 24;
                BulletPush[Temp, Use] = 0.8f;
                BulletPushVelMult[Temp, Use] = 1f;
                Pause[Temp, Use] = 0;
                ShootPause[Temp, Use] = 5;
                GunLight[Temp, Use] = new Vector3(4, 2, 3) * LightMult;
                ShotLight[Temp, Use] = new Vector3(2, 1f, 1.5f) * 0.75f;
                ShotLightDist[Temp, Use] = 250;
                ChargeSpeed[Temp, Use] = 0.5f;
                StopCharge[Temp, Use] = true;
                UseTarget[Temp, Use] = true;

                Use = 1;
                ROF[Temp, Use] = 0;
                Accuracy[Temp, Use] = 0;
                ClipSize[Temp, Use] = 1;
                ChargeTime[Temp, Use] = 20;
                ReloadTime[Temp, Use] = 60;
                ShotBulletNumb[Temp, Use] = 1;
                GunSoundEffect[Temp, Use] = "laser_grenade_launch";
                GunSoundEffectRange[Temp, Use] = 0;
                BurstSize[Temp, Use] = 1;
                BurstTime[Temp, Use] = 0;
                StartingAmmo[Temp, Use] = 8;
                ShotSize[Temp, Use] = new Vector2(0, 0);
                GainAmmo[Temp, Use] = 4;
                BulletSpeed[Temp, Use] = 40;
                BulletLifeTIme[Temp, Use] = 40;
                BulletType[Temp, Use] = 9;
                BulletExplosionSize[Temp, Use] = 475 + 50 + 25 + 100;
                BulletExplosionDamage[Temp, Use] = 75;
                BulletExplosionPush[Temp, Use] = 0.65f + 0.1f;
                BulletExplosionPushVelMult[Temp, Use] = 0.65f;
                BulletDamage[Temp, Use] = 0;
                BulletPush[Temp, Use] = 0;
                BulletPushVelMult[Temp, Use] = 0;
                Pause[Temp, Use] = 0;
                ShootPause[Temp, Use] = 10;
                GunLight[Temp, Use] = new Vector3(2, 2, 4) * LightMult;
                ShotLight[Temp, Use] = new Vector3(2, 2, 4) * 0.5f;
                ShotLightDist[Temp, Use] = 500;
                StopCharge[Temp, Use] = false;
                ChargeSpeed[Temp, Use] = 1;




                //rocket launcher
                Temp += 1;
                GunOffset[Temp] = 100;
                GunFlash[Temp] = game.Content.Load<Texture2D>("rocketflash");
                GunModel[Temp] = game.Content.Load<Model>("rocketlauncher");
                GunIcon[Temp] = game.Content.Load<Texture2D>("gun_outline/rocket_outline");
                Use = 0;
                GunSoundEffect[Temp, Use] = "rocket_launch";
                GunSoundEffectRange[Temp, Use] = 0;
                AimHelpAmount[Temp] = 1;
                ROF[Temp, Use] = 20;
                Accuracy[Temp, Use] = 0;
                ClipSize[Temp, Use] = 18;
                ReloadTime[Temp, Use] = 60;
                ChargeTime[Temp, Use] = 0;
                BurstSize[Temp, Use] = 3;
                BurstTime[Temp, Use] = 45;
                ShotBulletNumb[Temp, Use] = 1;
                ShotSize[Temp, Use] = new Vector2(0, 0);
                StartingAmmo[Temp, Use] = 10;
                GainAmmo[Temp, Use] = 5;
                BulletSpeed[Temp, Use] = 36;
                BulletLifeTIme[Temp, Use] = 40;
                BulletType[Temp, Use] = 15;
                BulletExplosionSize[Temp, Use] = 350;
                BulletExplosionDamage[Temp, Use] = 100;
                BulletExplosionPush[Temp, Use] = 1;
                BulletExplosionPushVelMult[Temp, Use] = 1;
                BulletDamage[Temp, Use] = 0;
                BulletPush[Temp, Use] = 0f;
                BulletPushVelMult[Temp, Use] = 1f;
                Pause[Temp, Use] = 0;
                ShootPause[Temp, Use] = 10;
                GunLight[Temp, Use] = new Vector3(4, 2, 1) * LightMult;
                ShotLight[Temp, Use] = new Vector3(2, 1f, 1f) * 0.75f;
                ShotLightDist[Temp, Use] = 250;
                ChargeSpeed[Temp, Use] = 0.5f;
                StopCharge[Temp, Use] = true;
                UseTarget[Temp, Use] = true;

                Use = 1;
                ROF[Temp, Use] = 0;
                Accuracy[Temp, Use] = 75;
                ClipSize[Temp, Use] = 1;
                ChargeTime[Temp, Use] = 20;
                ReloadTime[Temp, Use] = 60;
                GunSoundEffect[Temp, Use] = "rocket_launch2";
                GunSoundEffectRange[Temp, Use] = 0;
                ShotBulletNumb[Temp, Use] = 5;
                BurstSize[Temp, Use] = 1;
                BurstTime[Temp, Use] = 0;
                StartingAmmo[Temp, Use] = 8;
                ShotSize[Temp, Use] = new Vector2(0, 0);
                GainAmmo[Temp, Use] = 4;
                BulletSpeed[Temp, Use] = 32;
                BulletLifeTIme[Temp, Use] = 44;
                BulletType[Temp, Use] = 16;
                BulletExplosionSize[Temp, Use] = 300;
                BulletExplosionDamage[Temp, Use] = 40;
                BulletExplosionPush[Temp, Use] = 0.65f + 0.1f;
                BulletExplosionPushVelMult[Temp, Use] = 0.65f;
                BulletDamage[Temp, Use] = 0;
                BulletPush[Temp, Use] = 0;
                BulletPushVelMult[Temp, Use] = 0;
                Pause[Temp, Use] = 0;
                ShootPause[Temp, Use] = 10;
                GunLight[Temp, Use] = new Vector3(4, 2, 1) * LightMult;
                ShotLight[Temp, Use] = new Vector3(4, 2, 1) * 0.25f;
                ShotLightDist[Temp, Use] = 250;
                StopCharge[Temp, Use] = false;
                ChargeSpeed[Temp, Use] = 1;





                //Sword
                Temp += 1;

                Use = 0;
                ROF[Temp, Use] = 60;
                Accuracy[Temp, Use] = 1;
                ClipSize[Temp, Use] = 1;
                ReloadTime[Temp, Use] = 0;
                ChargeTime[Temp, Use] = 0;
                BurstSize[Temp, Use] = 1;
                BurstTime[Temp, Use] = 0;
                ShotBulletNumb[Temp, Use] = 1;
                ShotSize[Temp, Use] = new Vector2(200, 200);
                StartingAmmo[Temp, Use] = 10;
                GainAmmo[Temp, Use] = 5;
                BulletSpeed[Temp, Use] = 35;
                BulletLifeTIme[Temp, Use] = 10000;
                BulletType[Temp, Use] = 3;
                BulletExplosionSize[Temp, Use] = 0;
                BulletExplosionDamage[Temp, Use] = 0;
                BulletExplosionPush[Temp, Use] = 0;
                BulletPushVelMult[Temp, Use] = 0;
                BulletDamage[Temp, Use] = 18;
                BulletPush[Temp, Use] = 1f;
                BulletPushVelMult[Temp, Use] = 0.4f;
                Pause[Temp, Use] = 30;
                ShootPause[Temp, Use] = 0;

                Use = 1;
                ROF[Temp, Use] = 5;
                Accuracy[Temp, Use] = 5;
                ClipSize[Temp, Use] = 3;
                ChargeTime[Temp, Use] = 0;
                ReloadTime[Temp, Use] = 45;
                ShotBulletNumb[Temp, Use] = 1;
                BurstSize[Temp, Use] = 1;
                BurstTime[Temp, Use] = 0;
                StartingAmmo[Temp, Use] = 33;
                ShotSize[Temp, Use] = new Vector2(0, 0);
                GainAmmo[Temp, Use] = 15;
                BulletSpeed[Temp, Use] = 160;
                BulletLifeTIme[Temp, Use] = 12;
                BulletType[Temp, Use] = 0;
                BulletExplosionSize[Temp, Use] = 0;
                BulletExplosionDamage[Temp, Use] = 0;
                BulletExplosionPush[Temp, Use] = 0;
                BulletExplosionPushVelMult[Temp, Use] = 0;
                BulletDamage[Temp, Use] = 15;
                BulletPush[Temp, Use] = 1;
                BulletPushVelMult[Temp, Use] = 1;
                Pause[Temp, Use] = 0;
                ShootPause[Temp, Use] = 10;

            }

        }
    }
}
