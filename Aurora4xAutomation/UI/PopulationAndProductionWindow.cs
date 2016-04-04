﻿using System.Threading;
using System.Windows.Forms;
using WindowsInput.Native;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.UI.Controls;
using Button = Aurora4xAutomation.UI.Controls.Button;
using RadioButton = Aurora4xAutomation.UI.Controls.RadioButton;

namespace Aurora4xAutomation.UI
{
    public class PopulationAndProductionWindow : Window
    {
        public PopulationAndProductionWindow() 
            : base("Population and Production")
        {
            ResearchTable = new Datagrid(this) { Columns = new[] { 406, 687, 754 }, Top = 406, Bottom = 613, LineHeight = 16, TopOfCharactersOffset = 3, Settings = PrintSettings.NewResearchTable };
            AvailableScientistsTable = new Datagrid(this) { Columns = new[] { 790, 909, 1056 }, Top = 406, Bottom = 613, LineHeight = 16, TopOfCharactersOffset = 3, Settings = PrintSettings.AvailableScientistTable };
            ConstructionOptions = new Datagrid(this) { Columns = new []{ 398, 599 }, Left = 398, Right = 599, Top = 245, Bottom = 669, LineHeight = 16, TopOfCharactersOffset = 3 };
            AllocatedLabs = new Textbox(this) { Left = 885, Right = 929, Top = 357, Bottom = 371, CharacterOffset = 3, CharacterHeight = 9, Colors = new[] { new byte[] { 0, 0, 0 } } };
            AvailableLabs = new Textbox(this) { Left = 1037, Right = 1073, Top = 357, Bottom = 371, CharacterOffset = 3, CharacterHeight = 9, Colors = new[] { new byte[] { 109, 109, 109 } } };
            NumberOfIndustrialProject = new Textbox(this) { Left = 725, Right = 769, Top = 696, Bottom = 710, CharacterOffset = 3, CharacterHeight = 9, Colors = new[] { new byte[] { 0, 0, 0 } } };
            CreateIndustrialProject = new Button(this) { Left = 635, Right = 707, Top = 730, Bottom = 754 };
            InstallationType = new Combobox(this) { Left = 501, Right = 801, Top = 180, Bottom = 196, CharacterOffset = 4, CharacterHeight = 9, Colors = new[] { new byte[] { 0, 0, 0 } } };
            ContractAmount = new Textbox(this) { Left = 501, Right = 553, Top = 220, Bottom = 234, CharacterOffset = 3, CharacterHeight = 9, Colors = new[] { new byte[] { 0, 0, 0 } } };
            CivilianContractSupply = new RadioButton(this) { Left = 696, Right = 707, Top = 220, Bottom = 231 };
            CivilianContractDemand = new RadioButton(this) { Left = 784, Right = 795, Top = 221, Bottom = 232 };
            AddCivilianContract = new Button(this) { Left = 411, Right = 491, Top = 514, Bottom = 538 };
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
        public Button AddCivilianContract { get; set; }

        #endregion

        #region Research

        public void SelectResearchTab()
        {
            if (IsSummaryTabRowOnTop())
                Click(1165, 101);
            else
                Click(1152, 122);
        }

        private void DropDownNewResearchProject()
        {
            Click(617, 365);
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
            Click(524, 383);
        }

        public void SelectConstruction()
        {
            DropDownNewResearchProject();
            Click(524, 397);
        }

        public void SelectDefensive()
        {
            DropDownNewResearchProject();
            Click(524, 411);
        }

        public void SelectEnergy()
        {
            DropDownNewResearchProject();
            Click(524, 423);
        }

        public void SelectLogistics()
        {
            DropDownNewResearchProject();
            Click(524, 435);
        }

        public void SelectMissiles()
        {
            DropDownNewResearchProject();
            Click(524, 447);
        }

        public void SelectPower()
        {
            DropDownNewResearchProject();
            Click(524, 461);
        }

        public void SelectSensors()
        {
            DropDownNewResearchProject();
            Click(524, 475);
        }

        public void SetShowMatchingScientistsOnly(bool toggle)
        {
            if ((GetPixel(744, 368).EqualsColor(0, 0, 0) && !toggle)
                || GetPixel(744, 368).EqualsColor(255, 255, 255) && toggle)
            {
                Click(744, 368);
            }
        }

        public void SelectNthResearch(int number)
        {
            Click(467, 413 + number * 16);
        }

        public void SelectNthScientist(int number)
        {
            Click(986, 413 + number * 16);
        }

        public void SetAllocatedLabs(string labsNum)
        {
            Click(907, 364);
            Click(907, 364);
            Input.Keyboard.TextEntry(labsNum);
        }

        public void CreateResearch()
        {
            Click(440, 635);
            Input.Keyboard.KeyPress(VirtualKeyCode.RETURN);
        }

        #endregion

        #region Manage Shipyards

        public void SelectManageShipyards()
        {
            if (IsSummaryTabRowOnTop())
                Click(879, 101);
            else
                Click(869, 122);
        }

        public void SelectNthShipyard(int shipyardNumber)
        {
            Click(504, 196 + (shipyardNumber - 1)*16);
        }

        public void AddShipyardTask()
        {
            Click(442, 821);
        }

        #endregion

        #region Industry

        public void SelectIndustry()
        {
            if (IsSummaryTabRowOnTop())
                Click(597, 101);
            else
                Click(589, 122);
        }

        #endregion

        #region Civilians / Ind Status

        public void SelectCivilianTab()
        {
            if (IsSummaryTabRowOnTop())
                Click(730, 120);
            else
                Click(740, 100);
        }

        #endregion

        protected override void OpenIfNotFound()
        {
            new AuroraWrapperWindow().OpenBase();
            Thread.Sleep(1000);
            SendKeys.SendWait("{F2}");
        }
    }
}
