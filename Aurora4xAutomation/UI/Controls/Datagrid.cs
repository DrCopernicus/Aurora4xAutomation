using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.UI.Controls
{
    public class Datagrid : Control
    {
        public int[] Columns { get; set; }
        public int Top { get; set; } 
        public int Bottom { get; set; }
        public int LineHeight { get; set; }
        public int TopOfCharactersOffset { get; set; }
        public PrintSettings Settings;

        public Datagrid(Window parent) : base(parent)
        {
            
        }

        public string GetText()
        {
            return ReadDataTable(Columns, Top, Bottom, LineHeight, TopOfCharactersOffset).Print(Settings);
        }
    }
}
