﻿using System;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class ConsoleWindow : Window
    {
        public ConsoleWindow() : 
            base(@"file:///")
        {
            
        }

        protected override void OpenIfNotFound()
        {
            throw new Exception("Console window not found!");
        }
    }
}