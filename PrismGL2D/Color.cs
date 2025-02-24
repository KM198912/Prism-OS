﻿namespace PrismGL2D
{
    public struct Color
    {
        // Visible values
        public int Saturation
        {
            get
            {
                // Calculate the saturation of the color
                int Max = Math.Max(_R, Math.Max(_G, _B));
                int Min = Math.Min(_R, Math.Min(_G, _B));
                return (Max - Min) / 255;
            }
            set
            {
                // Set the saturation of the color
                int Max = Math.Max(_R, Math.Max(_G, _B));
                int Min = Math.Min(_R, Math.Min(_G, _B));
                int Diff = Max - Min;
                if (Diff == 0)
                {
                    _R = _G = _B = (byte)value;
                }
                else
                {
                    _R = (byte)((Max - _R) * value / Diff + _R);
                    _G = (byte)((Max - _G) * value / Diff + _G);
                    _B = (byte)((Max - _B) * value / Diff + _B);
                }
            }
        }
        public uint ARGB
        {
            get
            {
                return _ARGB;
            }
            set
            {
                _ARGB = value;
                _A = (byte)(_ARGB >> 24);
                _R = (byte)(_ARGB >> 16);
                _G = (byte)(_ARGB >> 8);
                _B = (byte)(_ARGB);
            }
        }
        public byte A
        {
            get
            {
                return _A;
            }
            set
            {
                _A = value;
                _ARGB = (uint)(_A << 24 | _R << 16 | _G << 8 | _B);
            }
        }
        public byte R
        {
            get
            {
                return _R;
            }
            set
            {
                _R = value;
                _ARGB = (uint)(_A << 24 | _R << 16 | _G << 8 | _B);
            }
        }
        public byte G
        {
            get
            {
                return _G;
            }
            set
            {
                _G = value;
                _ARGB = (uint)(_A << 24 | _R << 16 | _G << 8 | _B);
            }
        }
        public byte B
        {
            get
            {
                return _B;
            }
            set
            {
                _B = value;
                _ARGB = (uint)(_A << 24 | _R << 16 | _G << 8 | _B);
            }
        }

        #region Fields

        private uint _ARGB;
        private byte _A;
        private byte _R;
        private byte _G;
        private byte _B;

        #endregion

        #region Methods

        public static Color AlphaBlend(Color Source, Color NewColor)
        {
            if (NewColor.A == 0)
			{
                return Source;
			}

            return FromARGB(
                (byte)((Source.A * (255 - NewColor.A) / 255) + NewColor.A),
                (byte)((Source.R * (255 - NewColor.A) / 255) + NewColor.R),
                (byte)((Source.G * (255 - NewColor.A) / 255) + NewColor.G),
                (byte)((Source.B * (255 - NewColor.A) / 255) + NewColor.B));
        }
        public static Color FromCYMK(byte C, byte Y, byte M, byte K)
        {
            Color T = new();

            if (K != 255)
            {
                T.A = 255;
                T.R = (byte)((255 - C) * (255 - K) / 255);
                T.G = (byte)((255 - M) * (255 - K) / 255);
                T.B = (byte)((255 - Y) * (255 - K) / 255);
            }
            else
            {
                T.A = 255;
                T.R = (byte)(255 - C);
                T.G = (byte)(255 - M);
                T.B = (byte)(255 - Y);
            }
            return T;
        }
        public static Color FromARGB(byte A, byte R, byte G, byte B)
        {
            return new() { A = A, R = R, G = G, B = B };
        }
        public static Color FromARGB(uint ARGB)
        {
            return new() { ARGB = ARGB };
        }
        public static Color FromHex(string Hex)
        {
            if (Hex.StartsWith('#'))
            {
                Hex = Hex[1..];
            }

            return new() { ARGB = uint.Parse(Hex, System.Globalization.NumberStyles.HexNumber) };
        }

        #endregion

        #region Known Colors

        public static readonly Color White = FromARGB(255, 255, 255, 255);
        public static readonly Color Black = FromARGB(255, 0, 0, 0);
        public static readonly Color Red = FromARGB(255, 255, 0, 0);
        public static readonly Color Green = FromARGB(255, 0, 255, 0);
        public static readonly Color Blue = FromARGB(255, 0, 0, 255);
        public static readonly Color CoolGreen = FromARGB(255, 54, 94, 53);
        public static readonly Color HotPink = FromARGB(255, 230, 62, 109);
        public static readonly Color UbuntuPurple = FromARGB(255, 66, 5, 22);
        public static readonly Color GoogleBlue = FromARGB(255, 66, 133, 244);
        public static readonly Color GoogleGreen = FromARGB(255, 52, 168, 83);
        public static readonly Color GoogleYellow = FromARGB(255, 251, 188, 5);
        public static readonly Color GoogleRed = FromARGB(255, 234, 67, 53);
        public static readonly Color DeepOrange = FromARGB(255, 255, 64, 0);
        public static readonly Color RubyRed = FromARGB(255, 204, 52, 45);
        public static readonly Color Transparent = FromARGB(0, 0, 0, 0);
        public static readonly Color StackOverflowOrange = FromARGB(255, 244, 128, 36);
        public static readonly Color StackOverflowBlack = FromARGB(255, 34, 36, 38);
        public static readonly Color StackOverflowWhite = FromARGB(255, 188, 187, 187);
        public static readonly Color DeepGray = FromARGB(255, 25, 25, 25);
        public static readonly Color LightGray = FromARGB(255, 125, 125, 125);
        public static readonly Color SuperOrange = FromARGB(255, 255, 99, 71);
        public static readonly Color FakeGrassGreen = FromARGB(255, 60, 179, 113);
        public static readonly Color DeepBlue = FromARGB(255, 51, 47, 208);
        public static readonly Color BloodOrange = FromARGB(255, 255, 123, 0);
        public static readonly Color LightBlack = FromARGB(255, 25, 25, 25);
        public static readonly Color LighterBlack = FromARGB(255, 50, 50, 50);

        #endregion

        #region Operators

        public static implicit operator Color((byte, byte, byte, byte) Color)
        {
            return FromARGB(Color.Item1, Color.Item2, Color.Item3, Color.Item4);

            throw new ArgumentException("Unsuported Color Format!");
        }
        public static implicit operator Color((byte, byte, byte) RGB)
        {
            return FromARGB(255, RGB.Item1, RGB.Item2, RGB.Item3);
        }
        public static implicit operator Color(string Color)
        {
            if (Color.StartsWith('#'))
            {
                return FromHex(Color[1..]);
            }

            throw new ArgumentException("Not A Hex Color!");
        }
        public static implicit operator Color(uint ARGB)
        {
            return FromARGB(ARGB);
        }

        #endregion

        #region Equality

        public static bool operator ==(Color C1, Color C2)
        {
            return C1.ARGB == C2.ARGB;
        }
        public static bool operator !=(Color C1, Color C2)
        {
            return C1.ARGB != C2.ARGB;
        }

        #endregion

        public override string ToString()
        {
            return $"{typeof(Color).FullName} [A: {A}, R: {R}, G: {G}, B: {B}]";
        }
    }
}