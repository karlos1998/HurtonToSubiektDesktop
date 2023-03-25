namespace HurtonToSubiektDesktop
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.getCategoriesButton = new System.Windows.Forms.Button();
            this.catalogCategoriesTreeView = new System.Windows.Forms.TreeView();
            this.fullSyncXmlButton = new System.Windows.Forms.Button();
            this.hurtonJsonItemsCountLabel = new System.Windows.Forms.Label();
            this.getHurtonJsonButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.loginButton = new System.Windows.Forms.Button();
            this.nexoUserPasswordInput = new System.Windows.Forms.TextBox();
            this.nexoUsersComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.windowsAuthPass = new System.Windows.Forms.TextBox();
            this.windowsAuthLogin = new System.Windows.Forms.TextBox();
            this.windowsAuthBox = new System.Windows.Forms.CheckBox();
            this.nexoConnectButton = new System.Windows.Forms.Button();
            this.connect_server_button = new System.Windows.Forms.Button();
            this.select_db_box = new System.Windows.Forms.ComboBox();
            this.get_servers = new System.Windows.Forms.Button();
            this.select_server_combo = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.hurtonApiKeyBox = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.infoBox = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 421);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Controls.Add(this.getCategoriesButton);
            this.tabPage1.Controls.Add(this.catalogCategoriesTreeView);
            this.tabPage1.Controls.Add(this.fullSyncXmlButton);
            this.tabPage1.Controls.Add(this.hurtonJsonItemsCountLabel);
            this.tabPage1.Controls.Add(this.getHurtonJsonButton);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 395);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Główna";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // getCategoriesButton
            // 
            this.getCategoriesButton.Location = new System.Drawing.Point(31, 71);
            this.getCategoriesButton.Name = "getCategoriesButton";
            this.getCategoriesButton.Size = new System.Drawing.Size(328, 23);
            this.getCategoriesButton.TabIndex = 6;
            this.getCategoriesButton.Text = "Wczytaj liste kategorii";
            this.getCategoriesButton.UseVisualStyleBackColor = true;
            this.getCategoriesButton.Click += new System.EventHandler(this.getCategoriesButton_Click);
            // 
            // catalogCategoriesTreeView
            // 
            this.catalogCategoriesTreeView.Location = new System.Drawing.Point(365, 20);
            this.catalogCategoriesTreeView.Name = "catalogCategoriesTreeView";
            this.catalogCategoriesTreeView.Size = new System.Drawing.Size(392, 340);
            this.catalogCategoriesTreeView.TabIndex = 5;
            this.catalogCategoriesTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.catalogCategoriesTreeView_AfterCheck);
            this.catalogCategoriesTreeView.Click += new System.EventHandler(this.catalogCategoriesTreeView_AfterClick);
            // 
            // fullSyncXmlButton
            // 
            this.fullSyncXmlButton.Location = new System.Drawing.Point(31, 200);
            this.fullSyncXmlButton.Name = "fullSyncXmlButton";
            this.fullSyncXmlButton.Size = new System.Drawing.Size(328, 28);
            this.fullSyncXmlButton.TabIndex = 3;
            this.fullSyncXmlButton.Text = "Wykonaj pełną synchroinzację";
            this.fullSyncXmlButton.UseVisualStyleBackColor = true;
            this.fullSyncXmlButton.Click += new System.EventHandler(this.fullSyncXmlButton_Click);
            // 
            // hurtonJsonItemsCountLabel
            // 
            this.hurtonJsonItemsCountLabel.AutoSize = true;
            this.hurtonJsonItemsCountLabel.Location = new System.Drawing.Point(133, 58);
            this.hurtonJsonItemsCountLabel.Name = "hurtonJsonItemsCountLabel";
            this.hurtonJsonItemsCountLabel.Size = new System.Drawing.Size(188, 13);
            this.hurtonJsonItemsCountLabel.TabIndex = 2;
            this.hurtonJsonItemsCountLabel.Text = "Tu pojawi się ilośc przedmiotów z json.";
            // 
            // getHurtonJsonButton
            // 
            this.getHurtonJsonButton.Location = new System.Drawing.Point(236, 20);
            this.getHurtonJsonButton.Name = "getHurtonJsonButton";
            this.getHurtonJsonButton.Size = new System.Drawing.Size(85, 35);
            this.getHurtonJsonButton.TabIndex = 1;
            this.getHurtonJsonButton.Text = "Pobierz JSON z Hurton";
            this.getHurtonJsonButton.UseVisualStyleBackColor = true;
            this.getHurtonJsonButton.Click += new System.EventHandler(this.getHurtonJsonButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tekst powitalny aplikacji :)";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.loginButton);
            this.tabPage2.Controls.Add(this.nexoUserPasswordInput);
            this.tabPage2.Controls.Add(this.nexoUsersComboBox);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.windowsAuthPass);
            this.tabPage2.Controls.Add(this.windowsAuthLogin);
            this.tabPage2.Controls.Add(this.windowsAuthBox);
            this.tabPage2.Controls.Add(this.nexoConnectButton);
            this.tabPage2.Controls.Add(this.connect_server_button);
            this.tabPage2.Controls.Add(this.select_db_box);
            this.tabPage2.Controls.Add(this.get_servers);
            this.tabPage2.Controls.Add(this.select_server_combo);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 395);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Subiekt";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(25, 338);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(291, 23);
            this.loginButton.TabIndex = 10;
            this.loginButton.Text = "Zaloguj";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // nexoUserPasswordInput
            // 
            this.nexoUserPasswordInput.Location = new System.Drawing.Point(26, 311);
            this.nexoUserPasswordInput.Name = "nexoUserPasswordInput";
            this.nexoUserPasswordInput.Size = new System.Drawing.Size(288, 20);
            this.nexoUserPasswordInput.TabIndex = 9;
            // 
            // nexoUsersComboBox
            // 
            this.nexoUsersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nexoUsersComboBox.FormattingEnabled = true;
            this.nexoUsersComboBox.Location = new System.Drawing.Point(25, 284);
            this.nexoUsersComboBox.Name = "nexoUsersComboBox";
            this.nexoUsersComboBox.Size = new System.Drawing.Size(290, 21);
            this.nexoUsersComboBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hasło";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Login";
            // 
            // windowsAuthPass
            // 
            this.windowsAuthPass.Location = new System.Drawing.Point(174, 115);
            this.windowsAuthPass.Name = "windowsAuthPass";
            this.windowsAuthPass.Size = new System.Drawing.Size(142, 20);
            this.windowsAuthPass.TabIndex = 6;
            // 
            // windowsAuthLogin
            // 
            this.windowsAuthLogin.Location = new System.Drawing.Point(26, 115);
            this.windowsAuthLogin.Name = "windowsAuthLogin";
            this.windowsAuthLogin.Size = new System.Drawing.Size(142, 20);
            this.windowsAuthLogin.TabIndex = 5;
            // 
            // windowsAuthBox
            // 
            this.windowsAuthBox.AutoSize = true;
            this.windowsAuthBox.Location = new System.Drawing.Point(26, 75);
            this.windowsAuthBox.Name = "windowsAuthBox";
            this.windowsAuthBox.Size = new System.Drawing.Size(128, 17);
            this.windowsAuthBox.TabIndex = 2;
            this.windowsAuthBox.Text = "Autoryzacja Windows";
            this.windowsAuthBox.UseVisualStyleBackColor = true;
            this.windowsAuthBox.CheckedChanged += new System.EventHandler(this.windowsAuthBox_CheckedChanged);
            // 
            // nexoConnectButton
            // 
            this.nexoConnectButton.Location = new System.Drawing.Point(25, 230);
            this.nexoConnectButton.Name = "nexoConnectButton";
            this.nexoConnectButton.Size = new System.Drawing.Size(290, 23);
            this.nexoConnectButton.TabIndex = 3;
            this.nexoConnectButton.Text = "Połącz z Bazą";
            this.nexoConnectButton.UseVisualStyleBackColor = true;
            this.nexoConnectButton.Click += new System.EventHandler(this.nexoConnectButton_Click);
            // 
            // connect_server_button
            // 
            this.connect_server_button.Location = new System.Drawing.Point(26, 141);
            this.connect_server_button.Name = "connect_server_button";
            this.connect_server_button.Size = new System.Drawing.Size(290, 23);
            this.connect_server_button.TabIndex = 3;
            this.connect_server_button.Text = "Połącz z Serwerem";
            this.connect_server_button.UseVisualStyleBackColor = true;
            this.connect_server_button.Click += new System.EventHandler(this.connect_server_button_Click);
            // 
            // select_db_box
            // 
            this.select_db_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.select_db_box.FormattingEnabled = true;
            this.select_db_box.Location = new System.Drawing.Point(26, 203);
            this.select_db_box.Name = "select_db_box";
            this.select_db_box.Size = new System.Drawing.Size(289, 21);
            this.select_db_box.TabIndex = 7;
            this.select_db_box.SelectedIndexChanged += new System.EventHandler(this.select_db_box_SelectedIndexChanged);
            // 
            // get_servers
            // 
            this.get_servers.Location = new System.Drawing.Point(26, 18);
            this.get_servers.Name = "get_servers";
            this.get_servers.Size = new System.Drawing.Size(289, 23);
            this.get_servers.TabIndex = 0;
            this.get_servers.Text = "Pobierz listę dostępnych serwerów";
            this.get_servers.UseVisualStyleBackColor = true;
            this.get_servers.Click += new System.EventHandler(this.get_servers_Click);
            // 
            // select_server_combo
            // 
            this.select_server_combo.FormattingEnabled = true;
            this.select_server_combo.Location = new System.Drawing.Point(26, 47);
            this.select_server_combo.Name = "select_server_combo";
            this.select_server_combo.Size = new System.Drawing.Size(289, 21);
            this.select_server_combo.TabIndex = 1;
            this.select_server_combo.Text = "(local)";
            this.select_server_combo.SelectedIndexChanged += new System.EventHandler(this.select_server_combo_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.hurtonApiKeyBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 395);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Hurton";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Wprowadź klucz API Hurton";
            // 
            // hurtonApiKeyBox
            // 
            this.hurtonApiKeyBox.Location = new System.Drawing.Point(24, 69);
            this.hurtonApiKeyBox.Name = "hurtonApiKeyBox";
            this.hurtonApiKeyBox.Size = new System.Drawing.Size(273, 20);
            this.hurtonApiKeyBox.TabIndex = 0;
            // 
            // infoBox
            // 
            this.infoBox.Location = new System.Drawing.Point(4, 427);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(520, 20);
            this.infoBox.TabIndex = 1;
            this.infoBox.Text = "Przykladowy tekst :)";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(31, 234);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(328, 126);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(31, 366);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(726, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.Text = "Hurton to Subiekt";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox select_server_combo;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button get_servers;
        private System.Windows.Forms.ComboBox select_db_box;
        private System.Windows.Forms.Button connect_server_button;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox infoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox windowsAuthPass;
        private System.Windows.Forms.TextBox windowsAuthLogin;
        private System.Windows.Forms.CheckBox windowsAuthBox;
        private System.Windows.Forms.TextBox nexoUserPasswordInput;
        private System.Windows.Forms.ComboBox nexoUsersComboBox;
        private System.Windows.Forms.Button nexoConnectButton;
        private System.Windows.Forms.Button getHurtonJsonButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox hurtonApiKeyBox;
        private System.Windows.Forms.Label hurtonJsonItemsCountLabel;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button fullSyncXmlButton;
        private System.Windows.Forms.TreeView catalogCategoriesTreeView;
        private System.Windows.Forms.Button getCategoriesButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

