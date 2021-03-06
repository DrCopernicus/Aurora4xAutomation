﻿using Server.Common;
using Server.IO.UI.Controls;
using Server.IO.UI.Display;
using Server.Settings;
using System;
using System.Windows.Forms;
using WindowsInput.Native;
using Button = Server.IO.UI.Controls.Button;
using Label = Server.IO.UI.Controls.Label;
using RadioButton = Server.IO.UI.Controls.RadioButton;

namespace Server.IO.UI.Windows
{
    public class PopulationAndProductionWindow : Window
    {
        public PopulationAndProductionWindow(IScreen screen, IWindowFinder windowFinder, IInputDevice inputDevice, IOCRReader ocr, ISettingsStore settings)
            : base("Population and Production", screen, windowFinder, inputDevice, settings)
        {
            ResearchTable = new Datagrid(this, inputDevice, ocr, 406, 613, new [] { 406, 687, 754, 776 })
            {
                LineHeight = 16,
                TopOfCharactersOffset = 3,
                Settings = PrintSettings.NewResearchTable
            };
            AvailableScientistsTable = new Datagrid(this, inputDevice, ocr, 406, 613, new[] { 790, 909, 1056, 1184 })
            {
                LineHeight = 16,
                TopOfCharactersOffset = 3,
                Settings = PrintSettings.AvailableScientistTable
            };
            ConstructionOptions = new Datagrid(this, inputDevice, ocr, 245, 669, new []{ 398, 599 })
            {
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
            CurrentResearchProject = new Datagrid(this, inputDevice, ocr, 193, 289, new[] { 399, 632, 799, 859, 929, 999, 1132, 1158 })
            {
                LineHeight = 16,
                TopOfCharactersOffset = 3
            };
            RemoveRL = new Button(this, inputDevice, left: 683, right: 763, top: 299, bottom: 323);
            AddRL = new Button(this, inputDevice, left: 587, right: 667, top: 299, bottom: 323);
            NumberOfLabs = new Label(this, inputDevice, ocr, left: 515, right: 541, top: 140, bottom: 148);
            MatchingScientistsOnly = new RadioButton(this, inputDevice, left: 740, right: 750, top: 362, bottom: 372 );
        }

        [Obsolete]
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
        public RadioButton MatchingScientistsOnly { get; set; }

        #endregion

        public void Dirty()
        {
            Populations.Dirty();
            ResearchTable.Dirty();
        }

        #region Research

        [Obsolete]
        public void SelectResearchTab()
        {
            if (IsSummaryTabRowOnTop())
                this.Click(1165, 101);
            else
                this.Click(1152, 122);
        }

        [Obsolete]
        private void DropDownNewResearchProject()
        {
            this.Click(617, 365);
        }

        [Obsolete]
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

            StaticSleeper.Sleep(1000);
            Dirty();
        }

        [Obsolete]
        public void SelectBiology()
        {
            DropDownNewResearchProject();
            this.Click(524, 383);
        }

        [Obsolete]
        public void SelectConstruction()
        {
            DropDownNewResearchProject();
            this.Click(524, 397);
        }

        [Obsolete]
        public void SelectDefensive()
        {
            DropDownNewResearchProject();
            this.Click(524, 411);
        }

        [Obsolete]
        public void SelectEnergy()
        {
            DropDownNewResearchProject();
            this.Click(524, 423);
        }

        [Obsolete]
        public void SelectLogistics()
        {
            DropDownNewResearchProject();
            this.Click(524, 435);
        }

        [Obsolete]
        public void SelectMissiles()
        {
            DropDownNewResearchProject();
            this.Click(524, 447);
        }

        [Obsolete]
        public void SelectPower()
        {
            DropDownNewResearchProject();
            this.Click(524, 461);
        }

        [Obsolete]
        public void SelectSensors()
        {
            DropDownNewResearchProject();
            this.Click(524, 475);
        }

        [Obsolete]
        public void SelectNthResearch(int number)
        {
            this.Click(467, 413 + number * 16);
        }

        [Obsolete]
        public void SelectNthScientist(int number)
        {
            this.Click(986, 413 + number * 16);
        }

        [Obsolete]
        public void SetAllocatedLabs(string labsNum)
        {
            this.Click(907, 364);
            this.Click(907, 364);
            this.PressKeys(labsNum);
        }

        [Obsolete]
        public void CreateResearch()
        {
            this.Click(440, 635);
            this.PressKey(VirtualKeyCode.RETURN);
        }

        #endregion

        #region Manage Shipyards

        [Obsolete]
        public void SelectManageShipyards()
        {
            if (IsSummaryTabRowOnTop())
                this.Click(879, 101);
            else
                this.Click(869, 122);
        }

        [Obsolete]
        public void SelectNthShipyard(int shipyardNumber)
        {
            this.Click(504, 196 + (shipyardNumber - 1) * 16);
        }

        [Obsolete]
        public void AddShipyardTask()
        {
            this.Click(442, 821);
        }

        #endregion

        #region Tabs

        [Obsolete]
        public void SelectIndustry()
        {
            if (IsSummaryTabRowOnTop())
                this.Click(597, 101);
            else
                this.Click(589, 122);
        }

        [Obsolete]
        public void SelectCivilianTab()
        {
            if (IsSummaryTabRowOnTop())
                this.Click(730, 120);
            else
                this.Click(740, 100);
        }

        [Obsolete]
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
            StaticSleeper.Sleep(1000);
            SendKeys.SendWait("{F2}");
        }
    }
}
