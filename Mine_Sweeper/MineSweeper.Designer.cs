namespace Mine_Sweeper
{
    partial class MineSweeper
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.gamefieldPanel = new MyPanel();
            this.alertTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.levelDiffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newbeeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expertMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mineLeftLabel = new System.Windows.Forms.Label();
            this.gamefieldPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gamefieldPanel
            // 
            this.gamefieldPanel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.gamefieldPanel.Controls.Add(this.alertTextBox);
            this.gamefieldPanel.Location = new System.Drawing.Point(16, 34);
            this.gamefieldPanel.Name = "gamefieldPanel";
            this.gamefieldPanel.Size = new System.Drawing.Size(271, 271);
            this.gamefieldPanel.TabIndex = 0;
            this.gamefieldPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GamefieldPanel_MouseClick);
            // 
            // alertTextBox
            // 
            this.alertTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.alertTextBox.Location = new System.Drawing.Point(85, 125);
            this.alertTextBox.Name = "alertTextBox";
            this.alertTextBox.ReadOnly = true;
            this.alertTextBox.Size = new System.Drawing.Size(100, 22);
            this.alertTextBox.TabIndex = 0;
            this.alertTextBox.Text = "Игра окончена";
            this.alertTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.alertTextBox.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.levelDiffMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(304, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // levelDiffMenuItem
            // 
            this.levelDiffMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newbeeMenuItem,
            this.mediumMenuItem,
            this.expertMenuItem});
            this.levelDiffMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.levelDiffMenuItem.Name = "levelDiffMenuItem";
            this.levelDiffMenuItem.Size = new System.Drawing.Size(138, 21);
            this.levelDiffMenuItem.Text = "Уровень сложности";
            // 
            // newbeeMenuItem
            // 
            this.newbeeMenuItem.Name = "newbeeMenuItem";
            this.newbeeMenuItem.Size = new System.Drawing.Size(293, 22);
            this.newbeeMenuItem.Text = "Новичок - 10 мин, поле 9х9";
            this.newbeeMenuItem.Click += new System.EventHandler(this.NewbeeMenuItem_Click);
            // 
            // mediumMenuItem
            // 
            this.mediumMenuItem.Name = "mediumMenuItem";
            this.mediumMenuItem.Size = new System.Drawing.Size(293, 22);
            this.mediumMenuItem.Text = "Любитель - 40 мин, поле 16х16";
            this.mediumMenuItem.Click += new System.EventHandler(this.MediumMenuItem_Click);
            // 
            // expertMenuItem
            // 
            this.expertMenuItem.Name = "expertMenuItem";
            this.expertMenuItem.Size = new System.Drawing.Size(293, 22);
            this.expertMenuItem.Text = "Профессионал - 99 мин, поле 16х30";
            this.expertMenuItem.Click += new System.EventHandler(this.ExpertMenuItem_Click);
            // 
            // mineLeftLabel
            // 
            this.mineLeftLabel.AutoSize = true;
            this.mineLeftLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mineLeftLabel.Location = new System.Drawing.Point(176, 4);
            this.mineLeftLabel.Name = "mineLeftLabel";
            this.mineLeftLabel.Size = new System.Drawing.Size(99, 17);
            this.mineLeftLabel.TabIndex = 2;
            this.mineLeftLabel.Text = "Мин осталось: ";
            // 
            // MineSweeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 322);
            this.Controls.Add(this.mineLeftLabel);
            this.Controls.Add(this.gamefieldPanel);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MineSweeper";
            this.Text = "Сапер";
            this.Load += new System.EventHandler(this.MineSweeper_Load);
            this.gamefieldPanel.ResumeLayout(false);
            this.gamefieldPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel gamefieldPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem levelDiffMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newbeeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expertMenuItem;
        private System.Windows.Forms.Label mineLeftLabel;
        private System.Windows.Forms.TextBox alertTextBox;
    }
}

