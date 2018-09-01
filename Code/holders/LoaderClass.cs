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
using System.Xml.Serialization;
using System.Diagnostics;

namespace Orb
{
    public class LoaderClass
    {
        public Model Torso;
        public Texture2D Glow;
        public Texture2D CloakWarning;
        public Model FlagModel;
        public Model FlagModel2;
        public Texture2D MenuArrow;
        public Model DomeModel2;
        public Model GlowLines;
        public Texture2D PlayerTexture;
        public Model Leg;
        public Texture2D ScreenPointer;
        public Texture2D DeadTexture;
        public Model LowerLeg;
        public Model GrenadeModel;
        public Model LowerLeg2;
        public Texture2D PlayerBar;
        public Model ForgeFloorModel;
        public Texture2D MenuWindow2;
        public Texture2D ControllerRing;
        public Effect Wave;
        public Effect RingEffect;
        public Model TurretModel;
        public Model TurretModel2;
        public Model TorusModel;
        public Model HealthPackModel;
        public Texture2D HexTexture;
        public Model EnergyPackModel;
        public BasicEffect basicEffect;
        public Model Disk;
        public Model PlayerArrow;
        public Model DomeModel;
        public Texture2D GuiBoxSprite;
        public Texture2D CrossHair;
        public Texture2D GuiBoxSprite2;
        public Texture2D Mouse;
        public SpriteFont font;
        public Texture2D Temp;
        public Effect NormalLighting;
        public Effect Ambient;
        public Effect CircleGlow;
        public Effect ShadowCreator;
        public Effect Fresnel3;
        public Model PlayerRing;
        public Effect Fresnel;
        public Effect Fresnel2;
        public Model SelectBox3;
        public Model AimerModel;
        public Model LightBulb;
        public Texture2D BoxGrad;
      //  public RenderTarget2D DepthTexture;
      //  public RenderTarget2D DepthTexture2;
       // public RenderTarget2D DepthTexture3;
      //  public RenderTarget2D AmbientOcclusionPass;
     //   public RenderTarget2D AmbientOcclusionPass2;
        public Effect OcclusionEffect;
        public Texture2D AimerTexture;
        public Effect DepthMod;
        public Effect OcclusionRefinement;
        public Texture2D RingTexture;
        public Texture2D ArrowTexture;
        public Effect OcclusionRefinement2;
        public RenderTarget2D SceneTexture;
        public TextureCube Skybox;
        public Model Cube;
        public Effect SkyboxEffect;
        public Model BlobRingModel;
        public SpriteFont SmallFont;
        public Texture2D ColorBox;
        public Effect BlobEffect;
        public Texture2D MenuWindow;
        public Texture2D TeamTexture;
        public Texture2D DamageTexture;
        public Model GunShotModel;
        public Effect AmbientTexture;
        public Texture2D CloakTexture;
        public Texture2D XTexture;
        public Model BlackFloor;
        public Model FullPlayerModel;

        public void Load(Game1 game)
        {
            CloakWarning = game.Content.Load<Texture2D>("cloak_warning");
            FullPlayerModel = game.Content.Load<Model>("player_model_full");
            XTexture = game.Content.Load<Texture2D>("X");
            DomeModel2 = game.Content.Load<Model>("dome2");
            MenuArrow = game.Content.Load<Texture2D>("menu_arrow");
            FlagModel2 = game.Content.Load<Model>("flag2");
            ScreenPointer = game.Content.Load<Texture2D>("screen_pointer");
            BlackFloor = game.Content.Load<Model>("black_floor");
            FlagModel = game.Content.Load<Model>("flag");
            Glow = game.Content.Load<Texture2D>("glow");
            CloakTexture = game.Content.Load<Texture2D>("cloak");
            PlayerBar = game.Content.Load<Texture2D>("player_bar");
            PlayerTexture = game.Content.Load<Texture2D>("player_texture");
            GlowLines = game.Content.Load<Model>("player_glow");
            AmbientTexture = game.Content.Load<Effect>("AmbientTexture");
            GunShotModel = game.Content.Load<Model>("gunshot");
            TeamTexture = game.Content.Load<Texture2D>("team");
            DamageTexture = game.Content.Load<Texture2D>("damage");
            DeadTexture = game.Content.Load<Texture2D>("dead");
            GrenadeModel = game.Content.Load<Model>("grenade");
            Torso = game.Content.Load<Model>("player_torso");
            Leg = game.Content.Load<Model>("player_leg");
            LowerLeg = game.Content.Load<Model>("player_lower_leg");
            LowerLeg2 = game.Content.Load<Model>("player_lower_leg2");

            ColorBox = game.Content.Load<Texture2D>("colorbox");
            ControllerRing = game.Content.Load<Texture2D>("controller_ring");
            ArrowTexture = game.Content.Load<Texture2D>("arrow");
            MenuWindow2 = game.Content.Load<Texture2D>("menu_window2");
            MenuWindow = game.Content.Load<Texture2D>("menu_window");
            SmallFont = game.Content.Load<SpriteFont>("SmallFont");
            BlobEffect = game.Content.Load<Effect>("effects/BlobShadow_effect");
            BlobRingModel = game.Content.Load<Model>("blobring");
            RingEffect = game.Content.Load<Effect>("effects/ring_effect");
            RingTexture = game.Content.Load<Texture2D>("ring");
            SelectBox3 = game.Content.Load<Model>("selectbox3");
            basicEffect = new BasicEffect(game.GraphicsDevice);
            //file = game.Content.Load<File>("TempFileName");
            AimerTexture = game.Content.Load<Texture2D>("AimerTexture");
            AimerModel = game.Content.Load<Model>("aimer");
            TorusModel= game.Content.Load<Model>("torus");
            HexTexture = game.Content.Load<Texture2D>("hex");
            Wave = game.Content.Load<Effect>("effects/WaveEffect");
            Fresnel3 = game.Content.Load<Effect>("effects/fresnel3");
            DomeModel = game.Content.Load<Model>("dome");
            PlayerArrow = game.Content.Load<Model>("Player_arrow");
            Disk = game.Content.Load<Model>("disk");
            CrossHair = game.Content.Load<Texture2D>("crosshair");
            HealthPackModel = game.Content.Load<Model>("healthpack");
            EnergyPackModel = game.Content.Load<Model>("energy");
            TurretModel = game.Content.Load<Model>("Turret");
            TurretModel2 = game.Content.Load<Model>("Turret2");
            PlayerRing = game.Content.Load<Model>("Player_ring");
            ForgeFloorModel = game.Content.Load<Model>("ForgeFloor");
            GuiBoxSprite = game.Content.Load<Texture2D>("GUIBox");
            GuiBoxSprite2 = game.Content.Load<Texture2D>("GUIBox2");
            Mouse = game.Content.Load<Texture2D>("mouse");
            font = game.Content.Load<SpriteFont>("SpriteFont1");
            NormalLighting = game.Content.Load<Effect>("Light");
           Ambient = game.Content.Load<Effect>("Ambient");
           CircleGlow = game.Content.Load<Effect>("Effects/Circle_Glow");
           ShadowCreator = game.Content.Load<Effect>("CreateShadowMap");
           Fresnel = game.Content.Load<Effect>("Fresnel");
           Fresnel2 = game.Content.Load<Effect>("effects/Fresnel2");
           LightBulb = game.Content.Load<Model>("lightbulb");
           BoxGrad = game.Content.Load<Texture2D>("tiles_specular");
           Temp = game.Content.Load<Texture2D>("default");
           DepthMod = game.Content.Load<Effect>("DepthMod");
            SkyboxEffect=game.Content.Load<Effect>("Skybox");
           Cube = game.Content.Load<Model>("cube");
            

           foreach (ModelMesh mesh in LightBulb.Meshes)
               foreach (ModelMeshPart part in mesh.MeshParts)
                   part.Effect = Fresnel;

           foreach (ModelMesh mesh in BlobRingModel.Meshes)
               foreach (ModelMeshPart part in mesh.MeshParts)
                   part.Effect = BlobEffect;

           foreach (ModelMesh mesh in Cube.Meshes)
               foreach (ModelMeshPart part in mesh.MeshParts)
                   part.Effect = SkyboxEffect;

           foreach (ModelMesh mesh in DomeModel2.Meshes)
               foreach (ModelMeshPart part in mesh.MeshParts)
                   part.Effect = Fresnel3;

           foreach (ModelMesh mesh in game.Loader.GrenadeModel.Meshes)
               foreach (ModelMeshPart part in mesh.MeshParts)
                   part.Effect = Fresnel3;

           foreach (ModelMesh mesh in game.ShotModel.Meshes)
               foreach (ModelMeshPart part in mesh.MeshParts)
                   part.Effect = Ambient;

           PresentationParameters pp = game.GraphicsDevice.PresentationParameters;

           int width = pp.BackBufferWidth;
           int height = pp.BackBufferHeight;

           SurfaceFormat format = pp.BackBufferFormat;
            
           // Create a texture for rendering the main scene, prior to applying ambient occlusion.

           //DepthTexture = new RenderTarget2D(game.GraphicsDevice, width, height, false,
           //                                       SurfaceFormat.Single, pp.DepthStencilFormat, pp.MultiSampleCount
          //                                        ,
           //                                       RenderTargetUsage.DiscardContents);

        }

        public void Draw(Game1 game, LocalPlayer player)
        {
 
            DrawModel(game, Cube, player, player.CameraPos, Vector3.Zero, 1, new Vector3(2,2,2));
           
        }

        public void DrawShadow(Game1 game, LocalPlayer player)
        {

            DrawModel(game, Cube, player, player.CameraPos, Vector3.Zero, 1, new Vector3(2, 2, 2));

        }

        public void DrawModel(Game1 game, Model mod, LocalPlayer player, Vector3 pos, Vector3 Rot, float Alpha, Vector3 Scale)
        {
            foreach (ModelMesh mesh in mod.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    Matrix world = Matrix.CreateScale(Scale) * Matrix.CreateFromYawPitchRoll(-Rot.Y, Rot.X, Rot.Z) *
                Matrix.CreateTranslation(pos);
                    part.Effect = game.Loader.SkyboxEffect;
                    Effect effect = game.Loader.SkyboxEffect;
                    effect.Parameters["World"].SetValue(world);
                    effect.Parameters["View"].SetValue(player.playerView);
                    effect.Parameters["Projection"].SetValue(player.playerProjection);
                    effect.Parameters["surfaceTexture"].SetValue(Skybox);
                    effect.Parameters["EyePosition"].SetValue(player.CameraPos);
                }
                mesh.Draw();
            }
        }
    }
}
