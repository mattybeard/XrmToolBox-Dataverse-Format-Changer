﻿
namespace DataverseFormatChangerTool.Modals
{
    partial class ChangeFormatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.formatTypeChoice = new System.Windows.Forms.ComboBox();
            this.currentLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okayButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // formatTypeChoice
            // 
            this.formatTypeChoice.FormattingEnabled = true;
            this.formatTypeChoice.Items.AddRange(new object[] {
            "Text",
            "Textarea",
            "Email",
            "Url",
            "Tickersymbol",
            "Phone"});
            this.formatTypeChoice.Location = new System.Drawing.Point(12, 48);
            this.formatTypeChoice.Name = "formatTypeChoice";
            this.formatTypeChoice.Size = new System.Drawing.Size(372, 21);
            this.formatTypeChoice.TabIndex = 4;
            // 
            // currentLabel
            // 
            this.currentLabel.AutoSize = true;
            this.currentLabel.Location = new System.Drawing.Point(138, 9);
            this.currentLabel.Name = "currentLabel";
            this.currentLabel.Size = new System.Drawing.Size(114, 13);
            this.currentLabel.TabIndex = 3;
            this.currentLabel.Text = "Current Datatype: Text";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "What would you like to change to?";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(229, 75);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okayButton
            // 
            this.okayButton.Location = new System.Drawing.Point(116, 75);
            this.okayButton.Name = "okayButton";
            this.okayButton.Size = new System.Drawing.Size(75, 23);
            this.okayButton.TabIndex = 7;
            this.okayButton.Text = "Submit";
            this.okayButton.UseVisualStyleBackColor = true;
            this.okayButton.Click += new System.EventHandler(this.okayButton_Click);
            // 
            // ChangeFormatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 123);
            this.Controls.Add(this.formatTypeChoice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okayButton);
            this.Name = "ChangeFormatForm";
            this.Text = "Changing X Datatype";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox formatTypeChoice;
        private System.Windows.Forms.Label currentLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okayButton;
    }
}