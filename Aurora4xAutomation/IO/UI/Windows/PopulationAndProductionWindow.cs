using System.Windows.Forms;
using WindowsInput.Native;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO.UI.Controls;
using Aurora4xAutomation.IO.UI.Display;
using Aurora4xAutomation.Settings;
using Button = Aurora4xAutomation.IO.UI.Controls.Button;
using Label = Aurora4xAutomation.IO.UI.Controls.Label;
using RadioButton = Aurora4xAutomation.IO.UI.Controls.RadioButton;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class PopulationAndProductionWindow : Window
    {
        public PopulationAndProductionWindow(IScreen screen, IWindowFinder windowFinder, IInputDevice inputDevice, IOCRReader ocr, ISettingsStore settings)
            : base("Population and Production", screen, windowFinder, inputDevice, settings)
        {
            ResearchTable = new Datagrid(this, inputDevice, ocr, left: 406, right: 776, top: 406, bottom: 613)
            {
                Columns = new[] {406, 687, 754},
                LineHeight = 16,
                TopOfCharactersOffset = 3,
                Settings = PrintSettings.NewResearchTable
            };
            AvailableScientistsTable = new Datagrid(this, inputDevice, ocr, left: 790, right: 1184, top: 406, bottom: 613)
            {
                Columns = new[] {790, 909, 1056},
                LineHeight = 16,
                TopOfCharactersOffset = 3,
                Settings = PrintSettings.AvailableScientistTable
            };
            ConstructionOptions = new Datagrid(this, inputDevice, ocr, left: 398, right: 599, top: 245, bottom: 669)
            {
                Columns = new[] {398, 599},
                LineHeight = 16,
                TopOfCharactersOffset = 3
            };
            AllocatedLabs = new Textbox(this, inputDevice, ocr, left: 885, right: 929, top: 357, bottom: 371)
            {
                CharacterOffset = 3,
                CharacterHeight = 9,
                Colors = new[] {new byte[] {0, 0, 0}}
            };
            AvailableLabs = new Textbox(this, inputDevice, ocr, left: 1037, right: 1073, top: 357, bottom: 371)
            {
                CharacterOffset = 3,
                CharacterHeight = 9,
                Colors = new[] {new byte[] {109, 109, 109}}
            };
            NumberOfIndustrialProject = new Textbox(this, inputDevice, ocr, left: 725, right: 769, top: 696, bottom: 710)
            {
                CharacterOffset = 3,
                CharacterHeight = 9,
                Colors = new[] {new byte[] {0, 0, 0}}
            };
            CreateIndustrialProject = new Button(this, inputDevice, left: 635, right: 707, top: 730, bottom: 754);
            InstallationType = new Combobox(this, inputDevice, ocr, left: 501, right: 801, top: 180, bottom: 196)
            {
                CharacterOffset = 4,
                CharacterHeight = 9,
                Colors = new[] {new byte[] {0, 0, 0}}
            };
            ContractAmount = new Textbox(this, inputDevice, ocr, left: 501, right: 553, top: 220, bottom: 234)
            {
                CharacterOffset = 3,
                CharacterHeight = 9,
                Colors = new[] {new byte[] {0, 0, 0}}
            };
            CivilianContractSupply = new RadioButton(this, inputDevice, left: 696, right: 707, top: 220, bottom: 231);
            CivilianContractDemand = new RadioButton(this, inputDevice, left: 784, right: 795, top: 221, bottom: 232);
            AddCivilianContract = new Button(this, inputDevice, left: 411, right: 491, top: 514, bottom: 538);
            Populations = new TreeList(this, inputDevice, ocr, left: 21, right: 361, top: 100, bottom: 807);
            Populations.Refresh += (sender, args) =>
            {
                MakeActive();
                Empire.SelectOption(1);
            };
            PurchaseMineralOutput = new RadioButton(this, inputDevice, left: 536, right: 547, top: 804, bottom: 815);
            MassDriverDestination = new Combobox(this, inputDevice, ocr, left: 1005, right: 1184, top: 156, bottom: 172)
            {
                CharacterOffset = 4,
                CharacterHeight = 9,
                Colors = new[] {new byte[] {0, 0, 0}}
            };
            Empire = new Combobox(this, inputDevice, ocr, left: 19, right: 363, top: 46, bottom: 66)
            {
                CharacterOffset = 6,
                CharacterHeight = 11,
                Colors = new[] {new byte[] {0, 0, 0}}
            };
            CurrentResearchProject = new Datagrid(this, inputDevice, ocr, left: 399, right: 1158, top: 193, bottom: 289)
            {
                Columns = new[] {399, 632, 799, 859, 929, 999, 1132, 1158},
                LineHeight = 16,
                TopOfCharactersOffset = 3
            };
            RemoveRL = new Button(this, inputDevice, left: 683, right: 763, top: 299, bottom: 323);
            AddRL = new Button(this, inputDevice, left: 587, right: 667, top: 299, bottom: 323);
            NumberOfLabs = new Label(this, inputDevice, ocr, left: 515, right: 541, top: 140, bottom: 148);
        }

        private bool IsSummaryTabRowOnTop()
        {
            return GetPixel(405, 120).EqualsColor(0, 0, 0);
        }

        #region Controls

        public Datagrid ResearchTable { get; set; }
        public Datagrid AvailableScientistsTable { get; set; }
        public Datagrid ConstructionOptions { get; set; }
        public Textbox AllocatedLabs { get; set; }
        public Textbox AvailableLabs { get; set; }
        public Textbox NumberOfIndustrialProject { get; set; }
        public Button CreateIndustrialProject { get; set; }
        public Combobox InstallationType { get; set; }
        public Textbox ContractAmount { get; set; }
        public RadioButton CivilianContractSupply { get; set; }
        public RadioButton CivilianContractDemand { get; set; }
        public RadioButton PurchaseMineralOutput { get; set; }
        public Button AddCivilianContract { get; set; }
        public TreeList Populations { get; set; }
        public Combobox MassDriverDestination { get; set; }
        public Combobox Empire { get; set; }
        public Datagrid CurrentResearchProject { get; set; }
        public Button RemoveRL { get; set; }
        public Button AddRL { get; set; }
        public Label NumberOfLabs { get; set; }

        #endregion

        public void Dirty()
        {
            Populations.Dirty();
        }

        #region Research

        public void SelectResearchTab()
        {
            if (IsSummaryTabRowOnTop())
                this.Click(1165, 101);
            else
                this.Click(1152, 122);
        }

        private void DropDownNewResearchProject()
        {
            this.Click(617, 365);
        }

        public void SelectResearchByCategory(string name)
        {
            if (name == "bio" || name == "bg")
                SelectBiology();
            else if (name == "con" || name == "cp")
                SelectConstruction();
            else if (name == "def" || name == "ds")
                SelectDefensive();
            else if (name == "eng" || name == "ew")
                SelectEnergy();
            else if (name == "log" || name == "lg")
                SelectLogistics();
            else if (name == "mis" || name == "mk")
                SelectMissiles();
            else if (name == "pow" || name == "pp")
                SelectPower();
            else if (name == "sen" || name == "sf")
                SelectSensors();
        }

        public void SelectBiology()
        {
            DropDownNewResearchProject();
            this.Click(524, 383);
        }

        public void SelectConstruction()
        {
            DropDownNewResearchProject();
            this.Click(524, 397);
        }

        public void SelectDefensive()
        {
            DropDownNewResearchProject();
            this.Click(524, 411);
        }

        public void SelectEnergy()
        {
            DropDownNewResearchProject();
            this.Click(524, 423);
        }

        public void SelectLogistics()
        {
            DropDownNewResearchProject();
            this.Click(524, 435);
        }

        public void SelectMissiles()
        {
            DropDownNewResearchProject();
            this.Click(524, 447);
        }

        public void SelectPower()
        {
            DropDownNewResearchProject();
            this.Click(524, 461);
        }

        public void SelectSensors()
        {
            DropDownNewResearchProject();
            this.Click(524, 475);
        }

        public void SetShowMatchingScientistsOnly(bool toggle)
        {
            if ((this.GetPixel(744, 368).EqualsColor(0, 0, 0) && !toggle)
                || this.GetPixel(744, 368).EqualsColor(255, 255, 255) && toggle)
            {
                this.Click(744, 368);
            }
        }

        public void SelectNthResearch(int number)
        {
            this.Click(467, 413 + number * 16);
        }

        public void SelectNthScientist(int number)
        {
            this.Click(986, 413 + number * 16);
        }

        public void SetAllocatedLabs(string labsNum)
        {
            this.Click(907, 364);
            this.Click(907, 364);
            this.PressKeys(labsNum);
        }

        public void CreateResearch()
        {
            this.Click(440, 635);
            this.PressKey(VirtualKeyCode.RETURN);
        }

        #endregion

        #region Manage Shipyards

        public void SelectManageShipyards()
        {
            if (IsSummaryTabRowOnTop())
                this.Click(879, 101);
            else
                this.Click(869, 122);
        }

        public void SelectNthShipyard(int shipyardNumber)
        {
            this.Click(504, 196 + (shipyardNumber - 1) * 16);
        }

        public void AddShipyardTask()
        {
            this.Click(442, 821);
        }

        #endregion

        #region Tabs

        public void SelectIndustry()
        {
            if (IsSummaryTabRowOnTop())
                this.Click(597, 101);
            else
                this.Click(589, 122);
        }

        public void SelectCivilianTab()
        {
            if (IsSummaryTabRowOnTop())
                this.Click(730, 120);
            else
                this.Click(740, 100);
        }

        public void SelectMiningTab()
        {
            if (IsSummaryTabRowOnTop())
                this.Click(740, 100);
            else
                this.Click(730, 120);
        }

        #endregion

        protected override void OpenIfNotFound()
        {
            new BaseAuroraWindow(Screen, WindowFinder, InputDevice, Settings).MakeActive();
            Sleeper.Sleep(1000);
            SendKeys.SendWait("{F2}");
        }
    }
}
