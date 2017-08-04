namespace MIDITestCSharp
{
    partial class Main
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.OpenMIDIButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TempoCurrent = new System.Windows.Forms.Label();
            this.Tempo = new System.Windows.Forms.TrackBar();
            this.TrackbarStream = new System.Windows.Forms.TrackBar();
            this.RvAndChr = new System.Windows.Forms.CheckBox();
            this.Position = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LyricsFromStream = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LoadSoundfont = new System.Windows.Forms.Button();
            this.SFInfoLabel = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.CPUBar = new System.Windows.Forms.TrackBar();
            this.CPUUsage = new System.Windows.Forms.Label();
            this.VoicesLabel = new System.Windows.Forms.Label();
            this.VoiceBar = new System.Windows.Forms.TrackBar();
            this.OpenMIDI = new System.Windows.Forms.OpenFileDialog();
            this.MIDITitle = new System.Windows.Forms.Label();
            this.OpenSF = new System.Windows.Forms.OpenFileDialog();
            this.ShowInfo = new System.Windows.Forms.Timer(this.components);
            this.GetInfoFromStream = new System.ComponentModel.BackgroundWorker();
            this.VoiceUnlock = new System.Windows.Forms.ContextMenu();
            this.DoUnlock = new System.Windows.Forms.MenuItem();
            this.ShowTime = new System.Windows.Forms.ContextMenu();
            this.ShowTimeNotTick = new System.Windows.Forms.MenuItem();
            this.ResetEverything = new System.Windows.Forms.Button();
            this.PlayPauseBtn = new System.Windows.Forms.Button();
            this.KSIntegration = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tempo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackbarStream)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CPUBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoiceBar)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenMIDIButton
            // 
            this.OpenMIDIButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenMIDIButton.Location = new System.Drawing.Point(12, 12);
            this.OpenMIDIButton.Name = "OpenMIDIButton";
            this.OpenMIDIButton.Size = new System.Drawing.Size(360, 23);
            this.OpenMIDIButton.TabIndex = 0;
            this.OpenMIDIButton.Text = "Click here to open a file...";
            this.OpenMIDIButton.UseVisualStyleBackColor = true;
            this.OpenMIDIButton.Click += new System.EventHandler(this.OpenMIDIButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TempoCurrent);
            this.groupBox1.Controls.Add(this.Tempo);
            this.groupBox1.Location = new System.Drawing.Point(387, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(54, 167);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tempo";
            // 
            // TempoCurrent
            // 
            this.TempoCurrent.Dock = System.Windows.Forms.DockStyle.Top;
            this.TempoCurrent.Location = new System.Drawing.Point(3, 16);
            this.TempoCurrent.Name = "TempoCurrent";
            this.TempoCurrent.Size = new System.Drawing.Size(48, 23);
            this.TempoCurrent.TabIndex = 6;
            this.TempoCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Tempo
            // 
            this.Tempo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tempo.Location = new System.Drawing.Point(4, 35);
            this.Tempo.Maximum = 20;
            this.Tempo.Name = "Tempo";
            this.Tempo.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Tempo.Size = new System.Drawing.Size(45, 130);
            this.Tempo.TabIndex = 2;
            this.Tempo.Value = 10;
            this.Tempo.Scroll += new System.EventHandler(this.Tempo_Scroll);
            // 
            // TrackbarStream
            // 
            this.TrackbarStream.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrackbarStream.AutoSize = false;
            this.TrackbarStream.Location = new System.Drawing.Point(56, 61);
            this.TrackbarStream.Name = "TrackbarStream";
            this.TrackbarStream.Size = new System.Drawing.Size(219, 24);
            this.TrackbarStream.TabIndex = 2;
            this.TrackbarStream.TickFrequency = 0;
            this.TrackbarStream.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrackbarStream.Scroll += new System.EventHandler(this.TrackbarStream_Scroll);
            // 
            // RvAndChr
            // 
            this.RvAndChr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RvAndChr.AutoSize = true;
            this.RvAndChr.Location = new System.Drawing.Point(281, 64);
            this.RvAndChr.Name = "RvAndChr";
            this.RvAndChr.Size = new System.Drawing.Size(106, 17);
            this.RvAndChr.TabIndex = 3;
            this.RvAndChr.Text = "Reverb && Chorus";
            this.RvAndChr.UseVisualStyleBackColor = true;
            this.RvAndChr.CheckedChanged += new System.EventHandler(this.RvAndChr_CheckedChanged);
            // 
            // Position
            // 
            this.Position.Location = new System.Drawing.Point(322, 35);
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(119, 18);
            this.Position.TabIndex = 4;
            this.Position.Text = "0/0";
            this.Position.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.LyricsFromStream);
            this.groupBox2.Location = new System.Drawing.Point(12, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 76);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lyrics";
            // 
            // LyricsFromStream
            // 
            this.LyricsFromStream.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LyricsFromStream.Enabled = false;
            this.LyricsFromStream.Location = new System.Drawing.Point(3, 16);
            this.LyricsFromStream.Name = "LyricsFromStream";
            this.LyricsFromStream.Size = new System.Drawing.Size(363, 57);
            this.LyricsFromStream.TabIndex = 0;
            this.LyricsFromStream.Text = "Not yet implemented";
            this.LyricsFromStream.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.LoadSoundfont);
            this.groupBox3.Controls.Add(this.SFInfoLabel);
            this.groupBox3.Location = new System.Drawing.Point(12, 169);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(369, 54);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SoundFont";
            // 
            // LoadSoundfont
            // 
            this.LoadSoundfont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadSoundfont.Location = new System.Drawing.Point(285, 19);
            this.LoadSoundfont.Name = "LoadSoundfont";
            this.LoadSoundfont.Size = new System.Drawing.Size(75, 23);
            this.LoadSoundfont.TabIndex = 2;
            this.LoadSoundfont.Text = "Replace";
            this.LoadSoundfont.UseVisualStyleBackColor = true;
            this.LoadSoundfont.Click += new System.EventHandler(this.LoadSoundfont_Click);
            // 
            // SFInfoLabel
            // 
            this.SFInfoLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SFInfoLabel.Location = new System.Drawing.Point(3, 16);
            this.SFInfoLabel.Name = "SFInfoLabel";
            this.SFInfoLabel.Size = new System.Drawing.Size(219, 35);
            this.SFInfoLabel.TabIndex = 1;
            this.SFInfoLabel.Text = "No SoundFont";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.CPUBar);
            this.groupBox4.Controls.Add(this.CPUUsage);
            this.groupBox4.Controls.Add(this.VoicesLabel);
            this.groupBox4.Controls.Add(this.VoiceBar);
            this.groupBox4.Location = new System.Drawing.Point(12, 225);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(429, 64);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Performance";
            // 
            // CPUBar
            // 
            this.CPUBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CPUBar.AutoSize = false;
            this.CPUBar.Location = new System.Drawing.Point(134, 39);
            this.CPUBar.Maximum = 100;
            this.CPUBar.Name = "CPUBar";
            this.CPUBar.Size = new System.Drawing.Size(284, 20);
            this.CPUBar.TabIndex = 9;
            this.CPUBar.TickFrequency = 0;
            this.CPUBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.CPUBar.Value = 95;
            this.CPUBar.Scroll += new System.EventHandler(this.CPUBar_Scroll);
            // 
            // CPUUsage
            // 
            this.CPUUsage.Location = new System.Drawing.Point(6, 42);
            this.CPUUsage.Name = "CPUUsage";
            this.CPUUsage.Size = new System.Drawing.Size(122, 13);
            this.CPUUsage.TabIndex = 8;
            this.CPUUsage.Text = "CPU: 0%/95%";
            this.CPUUsage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // VoicesLabel
            // 
            this.VoicesLabel.Location = new System.Drawing.Point(6, 19);
            this.VoicesLabel.Name = "VoicesLabel";
            this.VoicesLabel.Size = new System.Drawing.Size(122, 13);
            this.VoicesLabel.TabIndex = 7;
            this.VoicesLabel.Text = "Voices: 100000/100000";
            this.VoicesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // VoiceBar
            // 
            this.VoiceBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VoiceBar.AutoSize = false;
            this.VoiceBar.Location = new System.Drawing.Point(134, 16);
            this.VoiceBar.Maximum = 2000;
            this.VoiceBar.Minimum = 1;
            this.VoiceBar.Name = "VoiceBar";
            this.VoiceBar.Size = new System.Drawing.Size(284, 20);
            this.VoiceBar.TabIndex = 6;
            this.VoiceBar.TickFrequency = 0;
            this.VoiceBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.VoiceBar.Value = 1000;
            this.VoiceBar.Scroll += new System.EventHandler(this.VoiceBar_Scroll);
            // 
            // OpenMIDI
            // 
            this.OpenMIDI.Filter = "MIDI files (mid/midi/rmi/kar)|*.mid;*.midi;*.rmi;*.kar|All files|*.*";
            // 
            // MIDITitle
            // 
            this.MIDITitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MIDITitle.Location = new System.Drawing.Point(12, 35);
            this.MIDITitle.Name = "MIDITitle";
            this.MIDITitle.Size = new System.Drawing.Size(300, 18);
            this.MIDITitle.TabIndex = 6;
            this.MIDITitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OpenSF
            // 
            this.OpenSF.Filter = "Soundfonts (sf2/sfz/sf2pack)|*.sf2;*.sfz;*.sf2pack|All files|*.*";
            // 
            // ShowInfo
            // 
            this.ShowInfo.Enabled = true;
            this.ShowInfo.Interval = 1;
            this.ShowInfo.Tick += new System.EventHandler(this.GetInfo_Tick);
            // 
            // GetInfoFromStream
            // 
            this.GetInfoFromStream.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GetInfoFromStream_DoWork);
            // 
            // VoiceUnlock
            // 
            this.VoiceUnlock.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.DoUnlock});
            // 
            // DoUnlock
            // 
            this.DoUnlock.Index = 0;
            this.DoUnlock.Text = "Unlock maximum voices";
            this.DoUnlock.Click += new System.EventHandler(this.DoUnlock_Click);
            // 
            // ShowTime
            // 
            this.ShowTime.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ShowTimeNotTick});
            // 
            // ShowTimeNotTick
            // 
            this.ShowTimeNotTick.Index = 0;
            this.ShowTimeNotTick.Text = "Show passed time instead of ticks";
            this.ShowTimeNotTick.Click += new System.EventHandler(this.ShowTimeNotTick_Click);
            // 
            // ResetEverything
            // 
            this.ResetEverything.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetEverything.Location = new System.Drawing.Point(378, 12);
            this.ResetEverything.Name = "ResetEverything";
            this.ResetEverything.Size = new System.Drawing.Size(63, 23);
            this.ResetEverything.TabIndex = 7;
            this.ResetEverything.Text = "Panic";
            this.ResetEverything.UseVisualStyleBackColor = true;
            this.ResetEverything.Click += new System.EventHandler(this.ResetEverything_Click);
            // 
            // PlayPauseBtn
            // 
            this.PlayPauseBtn.Location = new System.Drawing.Point(12, 60);
            this.PlayPauseBtn.Name = "PlayPauseBtn";
            this.PlayPauseBtn.Size = new System.Drawing.Size(45, 23);
            this.PlayPauseBtn.TabIndex = 8;
            this.PlayPauseBtn.Text = "Play";
            this.PlayPauseBtn.UseVisualStyleBackColor = true;
            this.PlayPauseBtn.Click += new System.EventHandler(this.PlayPauseBtn_Click);
            // 
            // KSIntegration
            // 
            this.KSIntegration.DoWork += new System.ComponentModel.DoWorkEventHandler(this.KSIntegration_DoWork);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 301);
            this.Controls.Add(this.PlayPauseBtn);
            this.Controls.Add(this.ResetEverything);
            this.Controls.Add(this.MIDITitle);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Position);
            this.Controls.Add(this.RvAndChr);
            this.Controls.Add(this.TrackbarStream);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OpenMIDIButton);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BASSMIDI Test C#";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tempo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackbarStream)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CPUBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VoiceBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenMIDIButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar Tempo;
        private System.Windows.Forms.TrackBar TrackbarStream;
        private System.Windows.Forms.CheckBox RvAndChr;
        private System.Windows.Forms.Label Position;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label LyricsFromStream;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button LoadSoundfont;
        private System.Windows.Forms.Label SFInfoLabel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label CPUUsage;
        private System.Windows.Forms.Label VoicesLabel;
        private System.Windows.Forms.TrackBar VoiceBar;
        private System.Windows.Forms.OpenFileDialog OpenMIDI;
        private System.Windows.Forms.Label TempoCurrent;
        private System.Windows.Forms.Label MIDITitle;
        private System.Windows.Forms.OpenFileDialog OpenSF;
        private System.Windows.Forms.Timer ShowInfo;
        private System.ComponentModel.BackgroundWorker GetInfoFromStream;
        private System.Windows.Forms.ContextMenu VoiceUnlock;
        private System.Windows.Forms.MenuItem DoUnlock;
        private System.Windows.Forms.ContextMenu ShowTime;
        private System.Windows.Forms.MenuItem ShowTimeNotTick;
        private System.Windows.Forms.Button ResetEverything;
        private System.Windows.Forms.TrackBar CPUBar;
        private System.Windows.Forms.Button PlayPauseBtn;
        private System.ComponentModel.BackgroundWorker KSIntegration;
    }
}

