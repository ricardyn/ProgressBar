#region Header
// Revit MEP API sample application
//
// Copyright (C) 2007-2010 by Jeremy Tammik, Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software
// for any purpose and without fee is hereby granted, provided
// that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  
// AUTODESK, INC. DOES NOT WARRANT THAT THE OPERATION OF THE 
// PROGRAM WILL BE UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject
// to restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
#endregion // Header

using System;
using System.Windows.Forms;
using System.Drawing;

namespace AdnRme
{
    public partial class ProgressForm : Form
    {
        private bool abortFlag;
        private Color DEFAULT_INFO_LABEL_COLOR = Color.Green;
        
        public string InformationText { get; set; } = "Processing...";
        

        /// <summary>
        /// Set up progress bar form and immediately display it modelessly.
        /// </summary>
        /// <param name="caption">Form caption</param>
        /// <param name="infoText">Progress message string</param>
        /// <param name="max">Number of elements to process</param>
        public ProgressForm(string caption, string infoText, int max)
        {
            InformationText = infoText;
            InitializeComponent();
            Text = caption;
            label1.Text = (null == infoText) ? caption : string.Format(infoText, 0);
            progressBar1.Minimum = 0;
            progressBar1.Maximum = max;
            progressBar1.Value = 0;
            Show();
            Application.DoEvents();
        }

        public void Increment(Color? statusColor = null)
        {
            progressBar1.Value++;
            
            if (!string.IsNullOrEmpty(InformationText))
            {
                label1.Text = InformationText;
            }

            label1.ForeColor = statusColor?? DEFAULT_INFO_LABEL_COLOR;
            
            Application.DoEvents();
        }

        public bool getAbortFlag()
        {
            return abortFlag;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Aborting...";
            abortFlag = true;
            this.Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                button1.Text = "Aborting...";
                abortFlag = true;
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

#if USE_MARTINS_PROGRESS_FORM
    public void SetText(string text)
    {
      label1.Text = text;
      System.Windows.Forms.Application.DoEvents();
    }

    public void SetProgressBarMinMax(int min, int max)
    {
      progressBar1.Minimum = min;
      progressBar1.Maximum = max;
      progressBar1.Value = 0;
    }

    public void IncrementProgressBar()
    {
      progressBar1.Value++;
      System.Windows.Forms.Application.DoEvents();
    }

    public void HideProgressBar()
    {
      progressBar1.Visible = false;
      System.Windows.Forms.Application.DoEvents();
    }

    public void ShowProgressBar()
    {
      progressBar1.Visible = true;
      System.Windows.Forms.Application.DoEvents();
    }
#endif // USE_MARTINS_PROGRESS_FORM

    }
}
