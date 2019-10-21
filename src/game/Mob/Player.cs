using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Space_Station_13
{
    public class Player// : IMob
    {
        //public bool synthetic { get; private set; }
        //public Species species { get; private set; }
        //public Body Body { get; set; }

        //public byte height { get; private set; }
        //public string fullname { get; private set; }
        //public string firstname { get; private set; }
        //public string lastname { get; private set; }

        //public void Rejuvenate()
        //{

        //}
        public Vector2 absPostion { get; set; }
        public Rectangle bounds { get; set; }
        public Texture2D drawTexture { get; set; }
        public Player()
        {
            absPostion = new Vector2(0, 0);
            drawTexture = Game1.human_0;
            bounds = drawTexture.Bounds;
        }
        public Player(Vector2 position)
        {
            absPostion = position;
            drawTexture = Game1.human_0;
            bounds = drawTexture.Bounds;
        }
        public Player(Vector2 position, Texture2D texture)
        {
            absPostion = position;
            drawTexture = texture;
            bounds = drawTexture.Bounds;
        }
        public Player(Vector2 position, Texture2D texture, Texture2D[] clothes)
        {
            absPostion = position;
            for(int i = 0; i < clothes.Length; i++)
            {
                texture = texture.Overlay(clothes[i]);
            }
            drawTexture = texture;
            bounds = drawTexture.Bounds;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(drawTexture, absPostion, Color.White);
        }
    }

    public enum Species
    {
        Human,
        Vox,
        Tajaran,
        Skrell,
        IPC,
        Unathi,
        Dionaea,
        GAS
    }
    public struct Body
    {
        public Head head { get; set; }
        public UpperBody upperBody { get; set; }
        public LeftHand leftHand { get; set; }
        public RightHand rightHand { get; set; }
        public LeftPalm leftPalm { get; set; }
        public RightPalm rightPalm { get; set; }
        public LowerBody lowerBody { get; set; }
        public LeftLeg leftLeg { get; set; }
        public RightLeg rightLeg { get; set; }
        public LeftFoot leftFoot { get; set; }
        public RightFoot rightFoot { get; set; }
    }
    public struct Head : IBodyPart
    {

    }
    public struct UpperBody : IBodyPart
    {

    }
    public struct LeftHand : IBodyPart
    {

    }
    public struct RightHand : IBodyPart
    {

    }
    public struct LeftPalm : IBodyPart
    {

    }
    public struct RightPalm : IBodyPart
    {

    }
    public struct LowerBody : IBodyPart
    {

    }
    public struct LeftLeg : IBodyPart
    {

    }
    public struct RightLeg : IBodyPart
    {

    }
    public struct LeftFoot : IBodyPart
    {

    }
    public struct RightFoot : IBodyPart
    {

    }
    public interface IBodyPart
    {

    }
    public interface IMob
    {
        void Rejuvenate();
    }
    
}
