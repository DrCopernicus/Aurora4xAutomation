using System;
using System.Collections.Generic;
using System.Linq;
using Aurora4xAutomation.Command;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.IO.UI.Controls
{
    public class TreeList : Control
    {
        public int CharacterHeight;
        public int CharacterOffset;
        public int BottomOffset;
        public byte[][] Colors;
        public event EventHandler Refresh;

        public TreeList(IWindow parent, int top, int bottom, int left, int right)
            : base(parent, top, bottom, left, right)
        {
            CharacterHeight = 11;
            CharacterOffset = 3;
            BottomOffset = 2;
            Colors = new [] { new byte[] { 0, 0, 0 } };
        }

        public void Select(string name)
        {
            var trail = GetParents(Children.FirstOrDefault(child => child.Text.SimilarContains(name)));
            if (trail.Any())
            {
                foreach (var parent in trail)
                    parent.Collapsed = false;
                trail.Last().Select();
                trail.Reverse();
                foreach (var parent in trail)
                    parent.Collapsed = true;

                return;
            }

            throw new Exception(string.Format("Couldn't find {0} in a tree list. Tree list text: \n{1}", name, Text));
        }

        private List<TreeListItem> GetParents(TreeListItem item)
        {
            var list = new List<TreeListItem>();
            while (item != null)
            {
                list.Add(item);
                item = item.ParentItem;
            }
            list.Reverse();
            return list;
        }

        public List<TreeListItem> Children
        {
            get
            {
                if (_children == null)
                {
                    if (Refresh != null)
                        Refresh(this, EventArgs.Empty);

                    _children = new List<TreeListItem>();
                    var parents = new List<TreeListItem>();

                    var index = 0;
                    while (true)
                    {
                        var item = new TreeListItem(this, parents.LastOrDefault(), 0, Right, index * (BottomOffset + CharacterOffset + CharacterHeight), CharacterOffset, CharacterHeight);
                        if (item.Level != 6)
                        {
                            for (int i = 0; i < parents.Count - item.Level; i++)
                            {
                                parents.Last().Collapsed = true;
                                parents.RemoveAt(parents.Count - 1);
                            }

                            item.Initialize();

                            _children.Add(item);
                            parents.Add(item);

                            item.Collapsed = false;

                            index++;
                        }
                        else
                        {
                            while (parents.Count > 0)
                            {
                                parents.Last().Collapsed = true;
                                parents.RemoveAt(parents.Count - 1);
                            }
                            break;
                        }
                    }
                }
                return _children;
            }
        }

        private List<TreeListItem> _children;
        
        public string Text
        {
            get
            {
                if (!Children.Any())
                    return "No children.\n";
                var text = "";
                foreach (var child in Children)
                    text += child.LevelledText + "\n";
                return text;
            }
        }

        public void Dirty()
        {
            _children = null;
        }

    }
}
