using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO.OCR;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class TreeListItem : Control
    {
        private IOCRReader OCR { get; set; }
        public TreeListItem ParentItem;
        public int CharacterOffset;
        public int CharacterHeight;
        public byte[][] Colors;

        public TreeListItem(IScreenObject parent, IInputDevice inputDevice, IOCRReader ocr, TreeListItem parentItem, int left, int right, int top, int offset, int height)
            : base(parent, inputDevice, top, top + 16, left, right)
        {
            ParentItem = parentItem;
            CharacterOffset = offset;
            CharacterHeight = height;
            Colors = new[] { new byte[] { 0, 0, 0 } };
            OCR = ocr;
        }

        public TreeListItem(IScreen screen, IInputDevice inputDevice, IOCRReader ocr, TreeListItem parentItem, int left, int right, int top, int offset, int height)
            : base(screen, inputDevice, top, top + 16, left, right)
        {
            ParentItem = parentItem;
            CharacterOffset = offset;
            CharacterHeight = height;
            Colors = new[] { new byte[] { 0, 0, 0 } };
            OCR = ocr;
        }

        public string Text { get; private set; }

        public void Initialize()
        {
            if (Screen.HasPixelsOfColor(
                Left + Level * 17,
                Top,
                10,
                10,
                new[] { new byte[] {51, 153, 255} }))
            {
                Text = OCR.ReadTableRow(
                            Screen.GetPixelsOfColor(
                                Left + Level * 17,
                                Top + CharacterOffset,
                                Right - Left - Level * 17,
                                CharacterHeight,
                                new [] { new byte[] {255, 255, 255} }),
                            OCRReader.Alphabet);
            }
            else
            {
                Text = OCR.ReadTableRow(
                            Screen.GetPixelsOfColor(
                                Left + Level * 17,
                                Top + CharacterOffset,
                                Right - Left - Level * 17,
                                CharacterHeight,
                                Colors),
                            OCRReader.Alphabet);
            }
            _collapsable = Screen.HasPixelsOfColor(
                                Left + Level * 17,
                                Top,
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
                text += Text;
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
                        if (!Screen.OnlyHasPixelsOfColor(
                            Left + i * 17,
                            Top,
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
                return Screen.OnlyHasPixelsOfColor(
                    Left,
                    Top,
                    Right - Left,
                    16,
                    new[] { new byte[] { 255, 255, 255 } });
            }
        }

        public bool Collapsed
        {
            get
            {
                if (GetPixel(
                    Level * 17 + 8,
                    7).EqualsColor(0, 0, 0))
                {
                    return true;
                }

                if (!Screen.HasPixelsOfColor(
                            Left + Level * 17,
                            Top,
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
                        Screen.Dirty();
                        Sleeper.Sleep(250);
                    }
                }
                else
                {
                    if (Collapsed)
                    {
                        Click(Level * 17 + 9, 7, 0);
                        Screen.Dirty();
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
