﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 15.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace ICHCodedUITest
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public partial class UIMap
    {
        
        /// <summary>
        /// AssertMethod1 - Use 'AssertMethod1ExpectedValues' to pass parameters into this method.
        /// </summary>
        public void AssertMethod1()
        {
            #region Variable Declarations
            WpfWindow uIWpfWindow = this.UIWpfWindow;
            var Components = UIWpfWindow.GetChildren();
            #endregion

            // Verify that the 'ControlType' property of 'Wpf' window equals 'Window'
            Assert.AreEqual(this.AssertMethod1ExpectedValues.UIWpfWindowControlType, uIWpfWindow.ControlType.ToString());
        }
        
        #region Properties
        public virtual AssertMethod1ExpectedValues AssertMethod1ExpectedValues
        {
            get
            {
                if ((this.mAssertMethod1ExpectedValues == null))
                {
                    this.mAssertMethod1ExpectedValues = new AssertMethod1ExpectedValues();
                }
                return this.mAssertMethod1ExpectedValues;
            }
        }
        
        public UIWpfWindow UIWpfWindow
        {
            get
            {
                if ((this.mUIWpfWindow == null))
                {
                    this.mUIWpfWindow = new UIWpfWindow();
                }
                return this.mUIWpfWindow;
            }
        }
        #endregion
        
        #region Fields
        private AssertMethod1ExpectedValues mAssertMethod1ExpectedValues;
        
        private UIWpfWindow mUIWpfWindow;
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'AssertMethod1'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class AssertMethod1ExpectedValues
    {
        
        #region Fields
        /// <summary>
        /// Verify that the 'ControlType' property of 'Wpf' window equals 'Window'
        /// </summary>
        public string UIWpfWindowControlType = "Window";
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UIWpfWindow : WpfWindow
    {
        
        public UIWpfWindow()
        {
            #region Search Criteria
            this.SearchProperties.Add(new PropertyExpression(WpfWindow.PropertyNames.ClassName, "HwndWrapper", PropertyExpressionOperator.Contains));
            #endregion
        }
    }
}