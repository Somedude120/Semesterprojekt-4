using System;

namespace MartoDatabasePrototype
{
    partial class formMain
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
            this.LabelFriends = new System.Windows.Forms.Label();
            this.ListBoxFriends = new System.Windows.Forms.ListBox();
            this.ListBoxFriendInfo = new System.Windows.Forms.ListBox();
            this.LabelFriendInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelFriends
            // 
            this.LabelFriends.AutoSize = true;
            this.LabelFriends.Location = new System.Drawing.Point(13, 13);
            this.LabelFriends.Name = "LabelFriends";
            this.LabelFriends.Size = new System.Drawing.Size(41, 13);
            this.LabelFriends.TabIndex = 0;
            this.LabelFriends.Text = "Friends";
            // 
            // ListBoxFriends
            // 
            this.ListBoxFriends.FormattingEnabled = true;
            this.ListBoxFriends.Location = new System.Drawing.Point(16, 30);
            this.ListBoxFriends.Name = "ListBoxFriends";
            this.ListBoxFriends.Size = new System.Drawing.Size(120, 95);
            this.ListBoxFriends.TabIndex = 1;
            this.ListBoxFriends.SelectedIndexChanged += new System.EventHandler(this.ListBoxFriends_SelectedIndexChanged);
            // 
            // ListBoxFriendInfo
            // 
            this.ListBoxFriendInfo.FormattingEnabled = true;
            this.ListBoxFriendInfo.Location = new System.Drawing.Point(142, 30);
            this.ListBoxFriendInfo.Name = "ListBoxFriendInfo";
            this.ListBoxFriendInfo.Size = new System.Drawing.Size(120, 95);
            this.ListBoxFriendInfo.TabIndex = 3;
            // 
            // LabelFriendInfo
            // 
            this.LabelFriendInfo.AutoSize = true;
            this.LabelFriendInfo.Location = new System.Drawing.Point(139, 13);
            this.LabelFriendInfo.Name = "LabelFriendInfo";
            this.LabelFriendInfo.Size = new System.Drawing.Size(54, 13);
            this.LabelFriendInfo.TabIndex = 2;
            this.LabelFriendInfo.Text = "FriendInfo";

            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 263);
            this.Controls.Add(this.ListBoxFriendInfo);
            this.Controls.Add(this.LabelFriendInfo);
            this.Controls.Add(this.ListBoxFriends);
            this.Controls.Add(this.LabelFriends);
            this.Name = "formMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.formMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LabelFriends;
        private System.Windows.Forms.ListBox ListBoxFriends;
        private System.Windows.Forms.ListBox ListBoxFriendInfo;
        private System.Windows.Forms.Label LabelFriendInfo;
    }
}

