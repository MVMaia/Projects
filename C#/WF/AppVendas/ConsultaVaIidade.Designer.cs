namespace AppVendas
{
    partial class ConsultaVaIidade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsultaVaIidade));
            this.txtName = new System.Windows.Forms.TextBox();
            this.date2 = new System.Windows.Forms.DateTimePicker();
            this.date3 = new System.Windows.Forms.DateTimePicker();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.date4 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(165, 173);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(209, 29);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // date2
            // 
            this.date2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.date2.Location = new System.Drawing.Point(82, 218);
            this.date2.Name = "date2";
            this.date2.Size = new System.Drawing.Size(88, 20);
            this.date2.TabIndex = 1;
            // 
            // date3
            // 
            this.date3.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.date3.Location = new System.Drawing.Point(248, 218);
            this.date3.Name = "date3";
            this.date3.Size = new System.Drawing.Size(88, 20);
            this.date3.TabIndex = 2;
            // 
            // dataGrid
            // 
            this.dataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(42)))), ((int)(((byte)(48)))));
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(22, 319);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(481, 184);
            this.dataGrid.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSave.Location = new System.Drawing.Point(50, 565);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(187, 36);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Consultar";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // date4
            // 
            this.date4.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.date4.Location = new System.Drawing.Point(176, 255);
            this.date4.Name = "date4";
            this.date4.Size = new System.Drawing.Size(88, 20);
            this.date4.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(283, 565);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(203, 36);
            this.button1.TabIndex = 7;
            this.button1.Text = "Dia de Vencimento";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ConsultaVaIidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(527, 629);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.date4);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.date3);
            this.Controls.Add(this.date2);
            this.Controls.Add(this.txtName);
            this.DoubleBuffered = true;
            this.Name = "ConsultaVaIidade";
            this.Text = "ConsultaVaIidade";
            this.Load += new System.EventHandler(this.ConsultaVaIidade_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.DateTimePicker date2;
        private System.Windows.Forms.DateTimePicker date3;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker date4;
        private System.Windows.Forms.Button button1;
    }
}