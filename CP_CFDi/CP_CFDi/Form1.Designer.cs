using System;
namespace CP_CFDi
{
    partial class CP_CFDi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CP_CFDi));
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.lblSerieB = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.lblFactura = new System.Windows.Forms.Label();
            this.rdbS = new System.Windows.Forms.RadioButton();
            this.rdbN = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.lblPara = new System.Windows.Forms.Label();
            this.chbPdf = new System.Windows.Forms.CheckBox();
            this.chbXml = new System.Windows.Forms.CheckBox();
            this.lblCSAT = new System.Windows.Forms.Label();
            this.lblFF = new System.Windows.Forms.Label();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.gridConceptos = new System.Windows.Forms.DataGridView();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.gridCartPort = new System.Windows.Forms.DataGridView();
            this.lblCarta = new System.Windows.Forms.Label();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.btnTimbrar = new System.Windows.Forms.Button();
            this.gridDescripciones = new System.Windows.Forms.DataGridView();
            this.lblSSAT = new System.Windows.Forms.Label();
            this.lblCCS = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSCFDI = new System.Windows.Forms.Label();
            this.lblCertificado = new System.Windows.Forms.Label();
            this.lblComprobante = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtT = new System.Windows.Forms.TextBox();
            this.lblFechTimb = new System.Windows.Forms.Label();
            this.cbFechTimb = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.servidor_lbl = new System.Windows.Forms.Label();
            this.vpn_lbl = new System.Windows.Forms.Label();
            this.rutaEjecu_lbl = new System.Windows.Forms.Label();
            this.dirDropBox_chb = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ayuda_lbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridConceptos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCartPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDescripciones)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSerie
            // 
            resources.ApplyResources(this.txtSerie, "txtSerie");
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.TextChanged += new System.EventHandler(this.txtSerie_TextChanged_1);
            this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSerie_KeyPress);
            // 
            // lblSerieB
            // 
            resources.ApplyResources(this.lblSerieB, "lblSerieB");
            this.lblSerieB.Name = "lblSerieB";
            // 
            // txtFactura
            // 
            resources.ApplyResources(this.txtFactura, "txtFactura");
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFactura_KeyPress_1);
            this.txtFactura.LostFocus += new System.EventHandler(this.txtFactura_LostFocus_1);
            // 
            // lblFactura
            // 
            resources.ApplyResources(this.lblFactura, "lblFactura");
            this.lblFactura.Name = "lblFactura";
            // 
            // rdbS
            // 
            resources.ApplyResources(this.rdbS, "rdbS");
            this.rdbS.Checked = true;
            this.rdbS.Name = "rdbS";
            this.rdbS.TabStop = true;
            this.rdbS.UseVisualStyleBackColor = true;
            // 
            // rdbN
            // 
            resources.ApplyResources(this.rdbN, "rdbN");
            this.rdbN.Name = "rdbN";
            this.rdbN.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblBuscar
            // 
            resources.ApplyResources(this.lblBuscar, "lblBuscar");
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Click += new System.EventHandler(this.lblBuscar_Click_1);
            // 
            // lblPara
            // 
            resources.ApplyResources(this.lblPara, "lblPara");
            this.lblPara.Name = "lblPara";
            // 
            // chbPdf
            // 
            resources.ApplyResources(this.chbPdf, "chbPdf");
            this.chbPdf.Checked = true;
            this.chbPdf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbPdf.Name = "chbPdf";
            this.chbPdf.UseVisualStyleBackColor = true;
            this.chbPdf.CheckedChanged += new System.EventHandler(this.chbPdf_CheckedChanged_1);
            // 
            // chbXml
            // 
            resources.ApplyResources(this.chbXml, "chbXml");
            this.chbXml.Checked = true;
            this.chbXml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbXml.Name = "chbXml";
            this.chbXml.UseVisualStyleBackColor = true;
            this.chbXml.CheckedChanged += new System.EventHandler(this.chbXml_CheckedChanged_1);
            // 
            // lblCSAT
            // 
            resources.ApplyResources(this.lblCSAT, "lblCSAT");
            this.lblCSAT.Name = "lblCSAT";
            // 
            // lblFF
            // 
            resources.ApplyResources(this.lblFF, "lblFF");
            this.lblFF.Name = "lblFF";
            // 
            // btnConsultar
            // 
            resources.ApplyResources(this.btnConsultar, "btnConsultar");
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click_1);
            // 
            // gridConceptos
            // 
            this.gridConceptos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.gridConceptos, "gridConceptos");
            this.gridConceptos.Name = "gridConceptos";
            this.gridConceptos.RowTemplate.Height = 24;
            // 
            // txtCorreo
            // 
            resources.ApplyResources(this.txtCorreo, "txtCorreo");
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Click += new System.EventHandler(this.txtCorreo_Click_1);
            this.txtCorreo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCorreo_KeyPress);
            this.txtCorreo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCorreo_KeyUp);
            // 
            // gridCartPort
            // 
            this.gridCartPort.AllowUserToAddRows = false;
            this.gridCartPort.AllowUserToDeleteRows = false;
            this.gridCartPort.AllowUserToOrderColumns = true;
            this.gridCartPort.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gridCartPort.BackgroundColor = System.Drawing.SystemColors.ScrollBar;
            this.gridCartPort.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCartPort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gridCartPort.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridCartPort.GridColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.gridCartPort, "gridCartPort");
            this.gridCartPort.MultiSelect = false;
            this.gridCartPort.Name = "gridCartPort";
            this.gridCartPort.ReadOnly = true;
            this.gridCartPort.RowTemplate.Height = 24;
            this.gridCartPort.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // lblCarta
            // 
            resources.ApplyResources(this.lblCarta, "lblCarta");
            this.lblCarta.Name = "lblCarta";
            // 
            // txtFolio
            // 
            this.txtFolio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txtFolio, "txtFolio");
            this.txtFolio.Name = "txtFolio";
            // 
            // btnTimbrar
            // 
            this.btnTimbrar.BackColor = System.Drawing.Color.Silver;
            this.btnTimbrar.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnTimbrar, "btnTimbrar");
            this.btnTimbrar.Name = "btnTimbrar";
            this.btnTimbrar.UseVisualStyleBackColor = false;
            this.btnTimbrar.Click += new System.EventHandler(this.btnTimbrar_Click);
            // 
            // gridDescripciones
            // 
            this.gridDescripciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.gridDescripciones, "gridDescripciones");
            this.gridDescripciones.Name = "gridDescripciones";
            this.gridDescripciones.RowTemplate.Height = 24;
            // 
            // lblSSAT
            // 
            resources.ApplyResources(this.lblSSAT, "lblSSAT");
            this.lblSSAT.Name = "lblSSAT";
            // 
            // lblCCS
            // 
            resources.ApplyResources(this.lblCCS, "lblCCS");
            this.lblCCS.Name = "lblCCS";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtFactura);
            this.groupBox3.Controls.Add(this.txtSerie);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnConsultar);
            this.groupBox3.Controls.Add(this.rdbN);
            this.groupBox3.Controls.Add(this.rdbS);
            this.groupBox3.Controls.Add(this.lblFactura);
            this.groupBox3.Controls.Add(this.lblSerieB);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // lblSCFDI
            // 
            resources.ApplyResources(this.lblSCFDI, "lblSCFDI");
            this.lblSCFDI.Name = "lblSCFDI";
            // 
            // lblCertificado
            // 
            resources.ApplyResources(this.lblCertificado, "lblCertificado");
            this.lblCertificado.Name = "lblCertificado";
            // 
            // lblComprobante
            // 
            resources.ApplyResources(this.lblComprobante, "lblComprobante");
            this.lblComprobante.Name = "lblComprobante";
            // 
            // txtContrasena
            // 
            resources.ApplyResources(this.txtContrasena, "txtContrasena");
            this.txtContrasena.Name = "txtContrasena";
            // 
            // lblContrasena
            // 
            resources.ApplyResources(this.lblContrasena, "lblContrasena");
            this.lblContrasena.Name = "lblContrasena";
            // 
            // txtT
            // 
            resources.ApplyResources(this.txtT, "txtT");
            this.txtT.Name = "txtT";
            // 
            // lblFechTimb
            // 
            resources.ApplyResources(this.lblFechTimb, "lblFechTimb");
            this.lblFechTimb.Name = "lblFechTimb";
            this.lblFechTimb.Click += new System.EventHandler(this.lblFechTimb_Click);
            // 
            // cbFechTimb
            // 
            resources.ApplyResources(this.cbFechTimb, "cbFechTimb");
            this.cbFechTimb.Name = "cbFechTimb";
            this.cbFechTimb.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Name = "label1";
            // 
            // servidor_lbl
            // 
            resources.ApplyResources(this.servidor_lbl, "servidor_lbl");
            this.servidor_lbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.servidor_lbl.Name = "servidor_lbl";
            // 
            // vpn_lbl
            // 
            resources.ApplyResources(this.vpn_lbl, "vpn_lbl");
            this.vpn_lbl.Cursor = System.Windows.Forms.Cursors.Help;
            this.vpn_lbl.Name = "vpn_lbl";
            // 
            // rutaEjecu_lbl
            // 
            resources.ApplyResources(this.rutaEjecu_lbl, "rutaEjecu_lbl");
            this.rutaEjecu_lbl.Name = "rutaEjecu_lbl";
            // 
            // dirDropBox_chb
            // 
            this.dirDropBox_chb.AutoCheck = false;
            resources.ApplyResources(this.dirDropBox_chb, "dirDropBox_chb");
            this.dirDropBox_chb.Name = "dirDropBox_chb";
            this.dirDropBox_chb.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dirDropBox_chb);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ayuda_lbl
            // 
            this.ayuda_lbl.Cursor = System.Windows.Forms.Cursors.Help;
            resources.ApplyResources(this.ayuda_lbl, "ayuda_lbl");
            this.ayuda_lbl.Image = global::CP_CFDi.Properties.Resources.help;
            this.ayuda_lbl.Name = "ayuda_lbl";
            this.ayuda_lbl.Click += new System.EventHandler(this.ayuda_lbl_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // CP_CFDi
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ayuda_lbl);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rutaEjecu_lbl);
            this.Controls.Add(this.vpn_lbl);
            this.Controls.Add(this.servidor_lbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbFechTimb);
            this.Controls.Add(this.lblFechTimb);
            this.Controls.Add(this.txtT);
            this.Controls.Add(this.lblContrasena);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.lblComprobante);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblSSAT);
            this.Controls.Add(this.lblSCFDI);
            this.Controls.Add(this.lblCertificado);
            this.Controls.Add(this.lblCCS);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.lblPara);
            this.Controls.Add(this.chbPdf);
            this.Controls.Add(this.chbXml);
            this.Controls.Add(this.lblCSAT);
            this.Controls.Add(this.lblFF);
            this.Controls.Add(this.gridConceptos);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.gridCartPort);
            this.Controls.Add(this.lblCarta);
            this.Controls.Add(this.txtFolio);
            this.Controls.Add(this.btnTimbrar);
            this.Controls.Add(this.gridDescripciones);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CP_CFDi";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CP_CFDi_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CP_CFDi_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.gridConceptos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCartPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDescripciones)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label lblSerieB;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label lblFactura;
        private System.Windows.Forms.RadioButton rdbS;
        private System.Windows.Forms.RadioButton rdbN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.Label lblPara;
        private System.Windows.Forms.CheckBox chbPdf;
        private System.Windows.Forms.CheckBox chbXml;
        private System.Windows.Forms.Label lblCSAT;
        private System.Windows.Forms.Label lblFF;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView gridConceptos;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.DataGridView gridCartPort;
        private System.Windows.Forms.Label lblCarta;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.Button btnTimbrar;
        private System.Windows.Forms.DataGridView gridDescripciones;
        private System.Windows.Forms.Label lblSSAT;
        private System.Windows.Forms.Label lblCCS;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblSCFDI;
        private System.Windows.Forms.Label lblCertificado;
        private System.Windows.Forms.Label lblComprobante;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtT;
        private System.Windows.Forms.Label lblFechTimb;
        private System.Windows.Forms.CheckBox cbFechTimb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label servidor_lbl;
        private System.Windows.Forms.Label vpn_lbl;
        private System.Windows.Forms.Label rutaEjecu_lbl;
        private System.Windows.Forms.CheckBox dirDropBox_chb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label ayuda_lbl;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

