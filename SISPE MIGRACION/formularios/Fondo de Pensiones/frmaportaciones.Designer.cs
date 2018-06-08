namespace SISPE_MIGRACION.formularios.Fondo_de_Pensiones
{
    partial class frmaportaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmaportaciones));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dtggrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label45 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtnue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtfechaing = new System.Windows.Forms.TextBox();
            this.txttiporel = new System.Windows.Forms.TextBox();
            this.txtsueldob = new System.Windows.Forms.TextBox();
            this.txtdependencia = new System.Windows.Forms.TextBox();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.txtnap = new System.Windows.Forms.TextBox();
            this.txtproyecto = new System.Windows.Forms.TextBox();
            this.txtrfc = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LABEL01 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtggrid)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label45);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1238, 584);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(12, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(266, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "DESGLOSE DE MOVIMIENTOS";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dtggrid);
            this.panel6.Location = new System.Drawing.Point(9, 254);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1217, 327);
            this.panel6.TabIndex = 14;
            // 
            // dtggrid
            // 
            this.dtggrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtggrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtggrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtggrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.dtggrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtggrid.Location = new System.Drawing.Point(0, 0);
            this.dtggrid.Name = "dtggrid";
            this.dtggrid.Size = new System.Drawing.Size(1217, 327);
            this.dtggrid.TabIndex = 0;
            this.dtggrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtggrid_CellEnter);
            this.dtggrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtggrid_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "INICIO";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "FINAL";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "MOVIMIENTO";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "ENTRADA";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "SALIDA";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "CUENTA";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "COMENTARIO";
            this.Column7.Name = "Column7";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1461, 34);
            this.panel2.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "FONDO DE PENSIONES/APORTACIONES";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1238, 104);
            this.panel3.TabIndex = 10;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(963, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(275, 104);
            this.panel4.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(275, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label45.Location = new System.Drawing.Point(15, 119);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(104, 20);
            this.label45.TabIndex = 13;
            this.label45.Text = "SOLICITUD";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.txtnue);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.txtfechaing);
            this.panel5.Controls.Add(this.txttiporel);
            this.panel5.Controls.Add(this.txtsueldob);
            this.panel5.Controls.Add(this.txtdependencia);
            this.panel5.Controls.Add(this.txtnombre);
            this.panel5.Controls.Add(this.txtnap);
            this.panel5.Controls.Add(this.txtproyecto);
            this.panel5.Controls.Add(this.txtrfc);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.LABEL01);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(9, 131);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1217, 99);
            this.panel5.TabIndex = 11;
            // 
            // txtnue
            // 
            this.txtnue.Cursor = System.Windows.Forms.Cursors.No;
            this.txtnue.Location = new System.Drawing.Point(285, 61);
            this.txtnue.Name = "txtnue";
            this.txtnue.ReadOnly = true;
            this.txtnue.Size = new System.Drawing.Size(110, 20);
            this.txtnue.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(237, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "NUE";
            // 
            // txtfechaing
            // 
            this.txtfechaing.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtfechaing.Cursor = System.Windows.Forms.Cursors.No;
            this.txtfechaing.Location = new System.Drawing.Point(603, 64);
            this.txtfechaing.MaxLength = 6;
            this.txtfechaing.Name = "txtfechaing";
            this.txtfechaing.ReadOnly = true;
            this.txtfechaing.Size = new System.Drawing.Size(169, 20);
            this.txtfechaing.TabIndex = 7;
            // 
            // txttiporel
            // 
            this.txttiporel.Cursor = System.Windows.Forms.Cursors.No;
            this.txttiporel.Location = new System.Drawing.Point(974, 37);
            this.txttiporel.Name = "txttiporel";
            this.txttiporel.ReadOnly = true;
            this.txttiporel.Size = new System.Drawing.Size(165, 20);
            this.txttiporel.TabIndex = 5;
            // 
            // txtsueldob
            // 
            this.txtsueldob.Cursor = System.Windows.Forms.Cursors.No;
            this.txtsueldob.Location = new System.Drawing.Point(715, 37);
            this.txtsueldob.MaxLength = 20;
            this.txtsueldob.Name = "txtsueldob";
            this.txtsueldob.ReadOnly = true;
            this.txtsueldob.Size = new System.Drawing.Size(118, 20);
            this.txtsueldob.TabIndex = 4;
            this.txtsueldob.Text = "0.00";
            // 
            // txtdependencia
            // 
            this.txtdependencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtdependencia.Cursor = System.Windows.Forms.Cursors.No;
            this.txtdependencia.Location = new System.Drawing.Point(407, 35);
            this.txtdependencia.Name = "txtdependencia";
            this.txtdependencia.ReadOnly = true;
            this.txtdependencia.Size = new System.Drawing.Size(190, 20);
            this.txtdependencia.TabIndex = 3;
            // 
            // txtnombre
            // 
            this.txtnombre.Cursor = System.Windows.Forms.Cursors.No;
            this.txtnombre.Location = new System.Drawing.Point(375, 9);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.ReadOnly = true;
            this.txtnombre.Size = new System.Drawing.Size(764, 20);
            this.txtnombre.TabIndex = 0;
            // 
            // txtnap
            // 
            this.txtnap.Cursor = System.Windows.Forms.Cursors.No;
            this.txtnap.Location = new System.Drawing.Point(99, 61);
            this.txtnap.Name = "txtnap";
            this.txtnap.ReadOnly = true;
            this.txtnap.Size = new System.Drawing.Size(110, 20);
            this.txtnap.TabIndex = 0;
            this.txtnap.TextChanged += new System.EventHandler(this.txtAdscripcion_TextChanged);
            // 
            // txtproyecto
            // 
            this.txtproyecto.Cursor = System.Windows.Forms.Cursors.No;
            this.txtproyecto.Location = new System.Drawing.Point(99, 35);
            this.txtproyecto.Name = "txtproyecto";
            this.txtproyecto.ReadOnly = true;
            this.txtproyecto.Size = new System.Drawing.Size(195, 20);
            this.txtproyecto.TabIndex = 2;
            // 
            // txtrfc
            // 
            this.txtrfc.Cursor = System.Windows.Forms.Cursors.No;
            this.txtrfc.Location = new System.Drawing.Point(99, 9);
            this.txtrfc.Name = "txtrfc";
            this.txtrfc.ReadOnly = true;
            this.txtrfc.Size = new System.Drawing.Size(195, 20);
            this.txtrfc.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(469, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "FECHA DE INGRESO";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(845, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "TIPO DE RELACIÓN";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(616, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "SUELDO BASE";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(306, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "DEPENDENCIA";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(309, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "NOMBRE";
            // 
            // LABEL01
            // 
            this.LABEL01.AutoSize = true;
            this.LABEL01.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LABEL01.Location = new System.Drawing.Point(51, 64);
            this.LABEL01.Name = "LABEL01";
            this.LABEL01.Size = new System.Drawing.Size(32, 13);
            this.LABEL01.TabIndex = 28;
            this.LABEL01.Text = "NAP";
            this.LABEL01.Click += new System.EventHandler(this.LABEL01_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "PROYECTO ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "RFC";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "ID";
            this.Column8.Name = "Column8";
            // 
            // frmaportaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 584);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frmaportaciones";
            this.Text = "frmaportaciones";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmaportaciones_FormClosing);
            this.Load += new System.EventHandler(this.frmaportaciones_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmaportaciones_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtggrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txttiporel;
        private System.Windows.Forms.TextBox txtsueldob;
        private System.Windows.Forms.TextBox txtdependencia;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.TextBox txtnap;
        private System.Windows.Forms.TextBox txtproyecto;
        private System.Windows.Forms.TextBox txtrfc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LABEL01;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dtggrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.TextBox txtnue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtfechaing;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}