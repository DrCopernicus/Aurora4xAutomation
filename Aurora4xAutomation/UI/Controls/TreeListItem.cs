using System;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.OCR;
using Aurora4xAutomation.UI.Screen;

namespace Aurora4xAutomation.UI.Controls
{
    public class TreeListItem : Control
    {
        public TreeListItem ParentItem;
        public int CharacterOffset;
        public int CharacterHeight;
        public byte[][] Colors;

        public TreeListItem(Window parent, TreeListItem parentItem, int left, int right, int top, int offset, int height)
            : base(parent)
        {
            ParentItem = parentItem;
            Left = left;
            Right = right;
            Top = top;
            Bottom = top + 16;
            CharacterOffset = offset;
            CharacterHeight = height;
            Colors = new[] { new byte[] { 0, 0, 0 } };
        }

        public string Text { get; private set; }

        public void Initialize()
        {
            if (PixelGetter.HasPixelsOfColor(
                Parent.Dimensions.Left + Left + Level * 17,
                Parent.Dimensions.Top + Top,
                10,
                10,
                new[] { new byte[] {51, 153, 255} }))
            {
                Text = OCRReader.ReadTableRow(
                            PixelGetter.GetPixelsOfColor(
                                Parent.Dimensions.Left + Left + Level * 17,
                                Parent.Dimensions.Top + Top + CharacterOffset,
                                Right - Left,
                                CharacterHeight,
                                new [] { new byte[] {255, 255, 255} }),
                            OCRReader.Alphabet);
            }
            else
            {
                Text = OCRReader.ReadTableRow(
                            PixelGetter.GetPixelsOfColor(
                                Parent.Dimensions.Left + Left + Level * 17,
                                Parent.Dimensions.Top + Top + CharacterOffset,
                                Right - Left,
                                CharacterHeight,
                                Colors),
                            OCRReader.Alphabet);
            }
            _collapsable = PixelGetter.HasPixelsOfColor(
                                Parent.Dimensions.Left + Left + Level * 17,
                                Parent.Dimensions.Top + Top,
                                17,
                                16,
                                new[] { new byte[] {0, 0, 0} });
        }

        public string LevelledText
        {
            get
            {
                var text = "";
                for (int i = 0; i < Level; i++)
                    text += " ";
                text += Collapsed ? "+" : "-";
                text += Text + "text";
                return text;
            }
        }

        public int Level
        {
            get
            {
                if (_level == null)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (!PixelGetter.OnlyHasPixelsOfColor(
                            Parent.Dimensions.Left + Left + i * 17,
                            Parent.Dimensions.Top + Top,
                            17,
                            16,
                            new[] {new byte[] {255, 255, 255}}))
                        {
                            _level = i;
                            break;
                        }
                    }
                }
                if (_level == null)
                    _level = 6;
                return _level.Value;
            }
        }

        private int? _level;

        public bool EmptyLine
        {
            get
            {
                return PixelGetter.OnlyHasPixelsOfColor(
                    Parent.Dimensions.Left + Left,
                    Parent.Dimensions.Top + Top,
                    Right - Left,
                    16,
                    new[] { new byte[] { 255, 255, 255 } });
            }
        }

        public bool Collapsed
        {
            get
            {
                if (PixelGetter.GetPixel(
                    Parent.Dimensions.Left + Left + Level * 17 + 8,
                    Parent.Dimensions.Top + Top + 7).EqualsColor(0, 0, 0))
                {
                    return true;
                }

                if (!PixelGetter.HasPixelsOfColor(
                            Parent.Dimensions.Left + Left + Level * 17,
                            Parent.Dimensions.Top + Top,
                            17,
                            16,
                            new[] { new byte[] { 0, 0, 0 } }))
                {
                    return true;
                }

                return false;
            }
            set
            {
                if (!_collapsable)
                    return;

                if (value)
                {
                    if (!Collapsed)
                    {
                        Click(Level * 17 + 9, 7, 0);
                        Screenshot.Dirty();
                        Sleeper.Sleep(250);
                    }
                }
                else
                {
                    if (Collapsed)
                    {
                        Click(Level * 17 + 9, 7, 0);
                        Screenshot.Dirty();
                        Sleeper.Sleep(250);
                    }
                }
            }
        }

        private bool _collapsable;

        public void Select()
        {
            Click((Level + 2) * 17, (Bottom - Top) / 2);
        }
    }
}
