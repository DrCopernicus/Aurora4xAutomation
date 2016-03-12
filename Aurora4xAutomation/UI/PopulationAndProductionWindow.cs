﻿using WindowsInput.Native;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.UI.Controls;

namespace Aurora4xAutomation.UI
{
    public class PopulationAndProductionWindow : Window
    {
        public PopulationAndProductionWindow() 
            : base("Population and Production")
        {
            
            ResearchTable = new Datagrid(this) { Columns = new[] { 406, 687, 754 }, Top = 406, Bottom = 613, LineHeight = 16, TopOfCharactersOffset = 3, Settings = PrintSettings.NewResearchTable };
            AvailableScientistsTable = new Datagrid(this) { Columns = new[] { 790, 909, 1056 }, Top = 406, Bottom = 613, LineHeight = 16, TopOfCharactersOffset = 3, Settings = PrintSettings.AvailableScientistTable };

        }

        private bool IsSummaryTabRowOnTop()
        {
            return GetPixel(405, 120).EqualsColor(0, 0, 0);
        }

        #region Controls
    
        public Datagrid ResearchTable { get; set; }
        public Datagrid AvailableScientistsTable { get; set; }

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
    }
}
