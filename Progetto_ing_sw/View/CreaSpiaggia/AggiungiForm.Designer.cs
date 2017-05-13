namespace Progetto_ing_sw.View
{
    partial class AggiungiForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AggiungiForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tipoComboBox = new System.Windows.Forms.ComboBox();
            this.qtTxt = new System.Windows.Forms.TextBox();
            this.righeTxt = new System.Windows.Forms.TextBox();
            this.numTxt = new System.Windows.Forms.TextBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.widthTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.heightTxt = new System.Windows.Forms.TextBox();
            this.offsetTxt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.offsetYTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Numero righe:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Quantità totale:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Numero iniziale:";
            // 
            // tipoComboBox
            // 
            this.tipoComboBox.FormattingEnabled = true;
            this.tipoComboBox.Location = new System.Drawing.Point(15, 26);
            this.tipoComboBox.Name = "tipoComboBox";
            this.tipoComboBox.Size = new System.Drawing.Size(108, 21);
            this.tipoComboBox.TabIndex = 4;
            // 
            // qtTxt
            // 
            this.qtTxt.Location = new System.Drawing.Point(15, 67);
            this.qtTxt.Name = "qtTxt";
            this.qtTxt.Size = new System.Drawing.Size(43, 20);
            this.qtTxt.TabIndex = 5;
            // 
            // righeTxt
            // 
            this.righeTxt.Location = new System.Drawing.Point(15, 112);
            this.righeTxt.Name = "righeTxt";
            this.righeTxt.Size = new System.Drawing.Size(43, 20);
            this.righeTxt.TabIndex = 6;
            // 
            // numTxt
            // 
            this.numTxt.Location = new System.Drawing.Point(15, 156);
            this.numTxt.Name = "numTxt";
            this.numTxt.Size = new System.Drawing.Size(43, 20);
            this.numTxt.TabIndex = 7;
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(15, 193);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 8;
            this.addBtn.Text = "Aggiungi";
            this.addBtn.UseVisualStyleBackColor = true;
            // 
            // widthTxt
            // 
            this.widthTxt.Location = new System.Drawing.Point(156, 25);
            this.widthTxt.Name = "widthTxt";
            this.widthTxt.Size = new System.Drawing.Size(43, 20);
            this.widthTxt.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(153, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Larghezza:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(153, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Altezza:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(153, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Offset X:";
            // 
            // heightTxt
            // 
            this.heightTxt.Location = new System.Drawing.Point(156, 67);
            this.heightTxt.Name = "heightTxt";
            this.heightTxt.Size = new System.Drawing.Size(43, 20);
            this.heightTxt.TabIndex = 13;
            // 
            // offsetTxt
            // 
            this.offsetTxt.Location = new System.Drawing.Point(156, 112);
            this.offsetTxt.Name = "offsetTxt";
            this.offsetTxt.Size = new System.Drawing.Size(43, 20);
            this.offsetTxt.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(153, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Offset Y:";
            // 
            // offsetYTxt
            // 
            this.offsetYTxt.Location = new System.Drawing.Point(156, 156);
            this.offsetYTxt.Name = "offsetYTxt";
            this.offsetYTxt.Size = new System.Drawing.Size(43, 20);
            this.offsetYTxt.TabIndex = 16;
            // 
            // AggiungiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 230);
            this.Controls.Add(this.offsetYTxt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.offsetTxt);
            this.Controls.Add(this.heightTxt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.widthTxt);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.numTxt);
            this.Controls.Add(this.righeTxt);
            this.Controls.Add(this.qtTxt);
            this.Controls.Add(this.tipoComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AggiungiForm";
            this.Text = "Aggiungi Pezzi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox tipoComboBox;
        private System.Windows.Forms.TextBox qtTxt;
        private System.Windows.Forms.TextBox righeTxt;
        private System.Windows.Forms.TextBox numTxt;
        private System.Windows.Forms.Button addBtn;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox widthTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox heightTxt;
        private System.Windows.Forms.TextBox offsetTxt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox offsetYTxt;
    }
}