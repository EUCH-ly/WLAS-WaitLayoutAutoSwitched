namespace WLAS___Wait__Layout_s_Auto_Switched
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            chkEnabled = new CheckBox();
            chkUseSlang = new CheckBox();
            btnSave = new Button();
            SuspendLayout();
            // 
            // chkEnabled
            // 
            chkEnabled.AutoSize = true;
            chkEnabled.BackColor = Color.Black;
            chkEnabled.Font = new Font("Montserrat", 7.79999971F, FontStyle.Bold);
            chkEnabled.ForeColor = SystemColors.Control;
            chkEnabled.Location = new Point(15, 14);
            chkEnabled.Margin = new Padding(4);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(287, 24);
            chkEnabled.TabIndex = 0;
            chkEnabled.Text = "WLAS - Wait, Layout Auto-Switched?";
            chkEnabled.ThreeState = true;
            chkEnabled.UseVisualStyleBackColor = false;
            // 
            // chkUseSlang
            // 
            chkUseSlang.AutoSize = true;
            chkUseSlang.Font = new Font("Montserrat", 7.79999971F, FontStyle.Bold);
            chkUseSlang.ForeColor = SystemColors.AppWorkspace;
            chkUseSlang.Location = new Point(15, 50);
            chkUseSlang.Margin = new Padding(4);
            chkUseSlang.Name = "chkUseSlang";
            chkUseSlang.Size = new Size(193, 24);
            chkUseSlang.TabIndex = 1;
            chkUseSlang.Text = "так называемый сленг";
            chkUseSlang.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Black;
            btnSave.FlatAppearance.BorderColor = Color.Black;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Font = new Font("Montserrat", 7.79999971F, FontStyle.Bold);
            btnSave.ForeColor = SystemColors.Window;
            btnSave.Location = new Point(132, 158);
            btnSave.Margin = new Padding(4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(118, 35);
            btnSave.TabIndex = 2;
            btnSave.Text = "сейв!";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(415, 208);
            Controls.Add(btnSave);
            Controls.Add(chkUseSlang);
            Controls.Add(chkEnabled);
            Font = new Font("Montserrat", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "SettingsForm";
            Text = "Customize ur WLAS";
            Load += SettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkEnabled;
        private CheckBox chkUseSlang;
        private Button btnSave;
    }
}