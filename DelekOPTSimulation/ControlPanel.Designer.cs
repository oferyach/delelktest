namespace DelekOPTSimulation
{
    partial class ControlPanel
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CardTag = new System.Windows.Forms.Button();
            this.CardType = new System.Windows.Forms.ListBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CardTag);
            this.groupBox2.Controls.Add(this.CardType);
            this.groupBox2.Location = new System.Drawing.Point(253, 264);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(318, 146);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "הכנס כרטיס / הצג תג / דלקן";
            // 
            // CardTag
            // 
            this.CardTag.Location = new System.Drawing.Point(6, 109);
            this.CardTag.Name = "CardTag";
            this.CardTag.Size = new System.Drawing.Size(75, 23);
            this.CardTag.TabIndex = 37;
            this.CardTag.Text = "הצג / הכנס";
            this.CardTag.UseVisualStyleBackColor = true;
            // 
            // CardType
            // 
            this.CardType.FormattingEnabled = true;
            this.CardType.Items.AddRange(new object[] {
            "אשראי",
            "מוכרן",
            "כב\"ת סולר",
            "כב\"ת + כל הפרומפט",
            "מנהל",
            "כרטיס פגום",
            "דלקן"});
            this.CardType.Location = new System.Drawing.Point(87, 19);
            this.CardType.Name = "CardType";
            this.CardType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CardType.Size = new System.Drawing.Size(226, 95);
            this.CardType.TabIndex = 36;
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 440);
            this.Controls.Add(this.groupBox2);
            this.Name = "ControlPanel";
            this.Text = "ControlPanel";
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button CardTag;
        private System.Windows.Forms.ListBox CardType;
    }
}