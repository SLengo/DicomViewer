namespace dicomViewer
{
    partial class Form1
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
            this.openDicomFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Readypanel = new System.Windows.Forms.Panel();
            this.ReadyLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openDICOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTemporaryFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadTimer = new System.Windows.Forms.Timer(this.components);
            this.IsoSurfacePanel = new System.Windows.Forms.Panel();
            this.IsoAdvancedPanel = new System.Windows.Forms.Panel();
            this.isoButton3 = new System.Windows.Forms.RadioButton();
            this.isoButton2 = new System.Windows.Forms.RadioButton();
            this.isoButton1 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.IsoBox = new System.Windows.Forms.TextBox();
            this.IsotrackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.VolumeRenderPanel = new System.Windows.Forms.Panel();
            this.VolumeRenderAdvancedPanel = new System.Windows.Forms.Panel();
            this.RenAdvButton3 = new System.Windows.Forms.RadioButton();
            this.RenAdvButton2 = new System.Windows.Forms.RadioButton();
            this.RenAdvButton1 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.renderingButton3 = new System.Windows.Forms.RadioButton();
            this.renderingButton2 = new System.Windows.Forms.RadioButton();
            this.renderingButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarz = new System.Windows.Forms.TrackBar();
            this.trackBary = new System.Windows.Forms.TrackBar();
            this.trackBarx = new System.Windows.Forms.TrackBar();
            this.DebugOuput = new System.Windows.Forms.TextBox();
            this.vtkFormsWindowControlMain = new vtk.vtkFormsWindowControl();
            this.vtkFormsWindowControlX = new vtk.vtkFormsWindowControl();
            this.vtkFormsWindowControlY = new vtk.vtkFormsWindowControl();
            this.vtkFormsWindowControlZ = new vtk.vtkFormsWindowControl();
            this.trackBarBrightnessX = new System.Windows.Forms.TrackBar();
            this.trackBartrackBarContrastX = new System.Windows.Forms.TrackBar();
            this.labelWLX = new System.Windows.Forms.Label();
            this.labelWWX = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.trackBarBrightnessY = new System.Windows.Forms.TrackBar();
            this.trackBarContrastY = new System.Windows.Forms.TrackBar();
            this.labelWLY = new System.Windows.Forms.Label();
            this.labelWWY = new System.Windows.Forms.Label();
            this.trackBarBrightnessZ = new System.Windows.Forms.TrackBar();
            this.trackBarContrastZ = new System.Windows.Forms.TrackBar();
            this.labelWLZ = new System.Windows.Forms.Label();
            this.labelWWZ = new System.Windows.Forms.Label();
            this.buttonSetRangeChoosenSlice = new System.Windows.Forms.Button();
            this.buttonSaveCurrentPlane = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.openPreprocessingApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Readypanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.IsoSurfacePanel.SuspendLayout();
            this.IsoAdvancedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IsotrackBar)).BeginInit();
            this.VolumeRenderPanel.SuspendLayout();
            this.VolumeRenderAdvancedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightnessX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBartrackBarContrastX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightnessY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrastY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightnessZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrastZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // openDicomFileDialog
            // 
            this.openDicomFileDialog.FileName = "openDicomFileDialog";
            this.openDicomFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openDicomFileDialog_FileOk);
            // 
            // Readypanel
            // 
            this.Readypanel.Controls.Add(this.ReadyLabel);
            this.Readypanel.Location = new System.Drawing.Point(6, 545);
            this.Readypanel.Margin = new System.Windows.Forms.Padding(2);
            this.Readypanel.Name = "Readypanel";
            this.Readypanel.Size = new System.Drawing.Size(106, 28);
            this.Readypanel.TabIndex = 0;
            // 
            // ReadyLabel
            // 
            this.ReadyLabel.AutoSize = true;
            this.ReadyLabel.Location = new System.Drawing.Point(35, 8);
            this.ReadyLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ReadyLabel.Name = "ReadyLabel";
            this.ReadyLabel.Size = new System.Drawing.Size(35, 13);
            this.ReadyLabel.TabIndex = 0;
            this.ReadyLabel.Text = "label1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDICOMToolStripMenuItem,
            this.planesToolStripMenuItem,
            this.surfaceToolStripMenuItem,
            this.setTemporaryFolderToolStripMenuItem,
            this.openPreprocessingApplicationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1106, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openDICOMToolStripMenuItem
            // 
            this.openDICOMToolStripMenuItem.Name = "openDICOMToolStripMenuItem";
            this.openDICOMToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.openDICOMToolStripMenuItem.Text = "Open DICOM";
            this.openDICOMToolStripMenuItem.Click += new System.EventHandler(this.openDICOMToolStripMenuItem_Click);
            // 
            // planesToolStripMenuItem
            // 
            this.planesToolStripMenuItem.Name = "planesToolStripMenuItem";
            this.planesToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.planesToolStripMenuItem.Text = "Planes";
            this.planesToolStripMenuItem.Click += new System.EventHandler(this.planesToolStripMenuItem_Click);
            // 
            // surfaceToolStripMenuItem
            // 
            this.surfaceToolStripMenuItem.Name = "surfaceToolStripMenuItem";
            this.surfaceToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.surfaceToolStripMenuItem.Text = "Surface";
            this.surfaceToolStripMenuItem.Click += new System.EventHandler(this.surfaceToolStripMenuItem_Click);
            // 
            // setTemporaryFolderToolStripMenuItem
            // 
            this.setTemporaryFolderToolStripMenuItem.Name = "setTemporaryFolderToolStripMenuItem";
            this.setTemporaryFolderToolStripMenuItem.Size = new System.Drawing.Size(143, 20);
            this.setTemporaryFolderToolStripMenuItem.Text = "Set temporary directory";
            this.setTemporaryFolderToolStripMenuItem.Click += new System.EventHandler(this.setTemporaryFolderToolStripMenuItem_Click);
            // 
            // LoadTimer
            // 
            this.LoadTimer.Tick += new System.EventHandler(this.LoadTimer_Tick);
            // 
            // IsoSurfacePanel
            // 
            this.IsoSurfacePanel.Controls.Add(this.IsoAdvancedPanel);
            this.IsoSurfacePanel.Controls.Add(this.label5);
            this.IsoSurfacePanel.Controls.Add(this.IsoBox);
            this.IsoSurfacePanel.Controls.Add(this.IsotrackBar);
            this.IsoSurfacePanel.Controls.Add(this.label2);
            this.IsoSurfacePanel.Enabled = false;
            this.IsoSurfacePanel.Location = new System.Drawing.Point(10, 26);
            this.IsoSurfacePanel.Name = "IsoSurfacePanel";
            this.IsoSurfacePanel.Size = new System.Drawing.Size(191, 177);
            this.IsoSurfacePanel.TabIndex = 11;
            this.IsoSurfacePanel.Visible = false;
            // 
            // IsoAdvancedPanel
            // 
            this.IsoAdvancedPanel.Controls.Add(this.isoButton3);
            this.IsoAdvancedPanel.Controls.Add(this.isoButton2);
            this.IsoAdvancedPanel.Controls.Add(this.isoButton1);
            this.IsoAdvancedPanel.Enabled = false;
            this.IsoAdvancedPanel.Location = new System.Drawing.Point(2, 96);
            this.IsoAdvancedPanel.Name = "IsoAdvancedPanel";
            this.IsoAdvancedPanel.Size = new System.Drawing.Size(185, 81);
            this.IsoAdvancedPanel.TabIndex = 6;
            this.IsoAdvancedPanel.Visible = false;
            // 
            // isoButton3
            // 
            this.isoButton3.AutoSize = true;
            this.isoButton3.Location = new System.Drawing.Point(9, 51);
            this.isoButton3.Name = "isoButton3";
            this.isoButton3.Size = new System.Drawing.Size(120, 17);
            this.isoButton3.TabIndex = 2;
            this.isoButton3.Text = "Ultra Quality Render";
            this.isoButton3.UseVisualStyleBackColor = true;
            this.isoButton3.Click += new System.EventHandler(this.isoButton3_Click);
            // 
            // isoButton2
            // 
            this.isoButton2.AutoSize = true;
            this.isoButton2.Location = new System.Drawing.Point(9, 28);
            this.isoButton2.Name = "isoButton2";
            this.isoButton2.Size = new System.Drawing.Size(120, 17);
            this.isoButton2.TabIndex = 1;
            this.isoButton2.Text = "High Quality Render";
            this.isoButton2.UseVisualStyleBackColor = true;
            this.isoButton2.Click += new System.EventHandler(this.isoButton2_Click);
            // 
            // isoButton1
            // 
            this.isoButton1.AutoSize = true;
            this.isoButton1.Checked = true;
            this.isoButton1.Location = new System.Drawing.Point(9, 4);
            this.isoButton1.Name = "isoButton1";
            this.isoButton1.Size = new System.Drawing.Size(114, 17);
            this.isoButton1.TabIndex = 0;
            this.isoButton1.TabStop = true;
            this.isoButton1.Text = "Auto Adjust Quality";
            this.isoButton1.UseVisualStyleBackColor = true;
            this.isoButton1.CheckedChanged += new System.EventHandler(this.isoButton1_CheckedChanged);
            this.isoButton1.Click += new System.EventHandler(this.isoButton1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Advanced Settings";
            // 
            // IsoBox
            // 
            this.IsoBox.Location = new System.Drawing.Point(72, 13);
            this.IsoBox.Name = "IsoBox";
            this.IsoBox.Size = new System.Drawing.Size(40, 20);
            this.IsoBox.TabIndex = 2;
            this.IsoBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IsoBox_KeyPress);
            // 
            // IsotrackBar
            // 
            this.IsotrackBar.Location = new System.Drawing.Point(3, 32);
            this.IsotrackBar.Maximum = 100;
            this.IsotrackBar.Name = "IsotrackBar";
            this.IsotrackBar.Size = new System.Drawing.Size(140, 45);
            this.IsotrackBar.TabIndex = 1;
            this.IsotrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.IsotrackBar_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Iso Value";
            // 
            // VolumeRenderPanel
            // 
            this.VolumeRenderPanel.Controls.Add(this.VolumeRenderAdvancedPanel);
            this.VolumeRenderPanel.Controls.Add(this.renderingButton3);
            this.VolumeRenderPanel.Controls.Add(this.renderingButton2);
            this.VolumeRenderPanel.Controls.Add(this.renderingButton1);
            this.VolumeRenderPanel.Controls.Add(this.label3);
            this.VolumeRenderPanel.Enabled = false;
            this.VolumeRenderPanel.Location = new System.Drawing.Point(10, 210);
            this.VolumeRenderPanel.Name = "VolumeRenderPanel";
            this.VolumeRenderPanel.Size = new System.Drawing.Size(191, 192);
            this.VolumeRenderPanel.TabIndex = 14;
            this.VolumeRenderPanel.Visible = false;
            // 
            // VolumeRenderAdvancedPanel
            // 
            this.VolumeRenderAdvancedPanel.Controls.Add(this.RenAdvButton3);
            this.VolumeRenderAdvancedPanel.Controls.Add(this.RenAdvButton2);
            this.VolumeRenderAdvancedPanel.Controls.Add(this.RenAdvButton1);
            this.VolumeRenderAdvancedPanel.Controls.Add(this.label4);
            this.VolumeRenderAdvancedPanel.Location = new System.Drawing.Point(2, 92);
            this.VolumeRenderAdvancedPanel.Name = "VolumeRenderAdvancedPanel";
            this.VolumeRenderAdvancedPanel.Size = new System.Drawing.Size(188, 100);
            this.VolumeRenderAdvancedPanel.TabIndex = 5;
            // 
            // RenAdvButton3
            // 
            this.RenAdvButton3.AutoSize = true;
            this.RenAdvButton3.Location = new System.Drawing.Point(8, 73);
            this.RenAdvButton3.Name = "RenAdvButton3";
            this.RenAdvButton3.Size = new System.Drawing.Size(111, 17);
            this.RenAdvButton3.TabIndex = 7;
            this.RenAdvButton3.Text = "Fragment Program";
            this.RenAdvButton3.UseVisualStyleBackColor = true;
            // 
            // RenAdvButton2
            // 
            this.RenAdvButton2.AutoSize = true;
            this.RenAdvButton2.Location = new System.Drawing.Point(9, 50);
            this.RenAdvButton2.Name = "RenAdvButton2";
            this.RenAdvButton2.Size = new System.Drawing.Size(100, 17);
            this.RenAdvButton2.TabIndex = 6;
            this.RenAdvButton2.Text = "Nvidia Graphics";
            this.RenAdvButton2.UseVisualStyleBackColor = true;
            // 
            // RenAdvButton1
            // 
            this.RenAdvButton1.AutoSize = true;
            this.RenAdvButton1.Checked = true;
            this.RenAdvButton1.Location = new System.Drawing.Point(8, 27);
            this.RenAdvButton1.Name = "RenAdvButton1";
            this.RenAdvButton1.Size = new System.Drawing.Size(59, 17);
            this.RenAdvButton1.TabIndex = 5;
            this.RenAdvButton1.TabStop = true;
            this.RenAdvButton1.Text = "Default";
            this.RenAdvButton1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Advanced Settings";
            // 
            // renderingButton3
            // 
            this.renderingButton3.AutoSize = true;
            this.renderingButton3.Location = new System.Drawing.Point(8, 73);
            this.renderingButton3.Name = "renderingButton3";
            this.renderingButton3.Size = new System.Drawing.Size(101, 17);
            this.renderingButton3.TabIndex = 3;
            this.renderingButton3.Text = "Fast 3D Texture";
            this.renderingButton3.UseVisualStyleBackColor = true;
            // 
            // renderingButton2
            // 
            this.renderingButton2.AutoSize = true;
            this.renderingButton2.Location = new System.Drawing.Point(8, 50);
            this.renderingButton2.Name = "renderingButton2";
            this.renderingButton2.Size = new System.Drawing.Size(111, 17);
            this.renderingButton2.TabIndex = 2;
            this.renderingButton2.Text = "Maximum Intensity";
            this.renderingButton2.UseVisualStyleBackColor = true;
            // 
            // renderingButton1
            // 
            this.renderingButton1.AutoSize = true;
            this.renderingButton1.Checked = true;
            this.renderingButton1.Location = new System.Drawing.Point(8, 27);
            this.renderingButton1.Name = "renderingButton1";
            this.renderingButton1.Size = new System.Drawing.Size(110, 17);
            this.renderingButton1.TabIndex = 1;
            this.renderingButton1.TabStop = true;
            this.renderingButton1.Text = "Normal Rendering";
            this.renderingButton1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Type Rendering";
            // 
            // trackBarz
            // 
            this.trackBarz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarz.Location = new System.Drawing.Point(881, 534);
            this.trackBarz.Maximum = 99;
            this.trackBarz.Name = "trackBarz";
            this.trackBarz.Size = new System.Drawing.Size(159, 45);
            this.trackBarz.TabIndex = 18;
            this.trackBarz.Value = 45;
            this.trackBarz.Scroll += new System.EventHandler(this.trackBarz_Scroll);
            // 
            // trackBary
            // 
            this.trackBary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBary.Location = new System.Drawing.Point(881, 347);
            this.trackBary.Maximum = 99;
            this.trackBary.Name = "trackBary";
            this.trackBary.Size = new System.Drawing.Size(159, 45);
            this.trackBary.TabIndex = 17;
            this.trackBary.Value = 45;
            this.trackBary.Scroll += new System.EventHandler(this.trackBary_Scroll);
            // 
            // trackBarx
            // 
            this.trackBarx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarx.Location = new System.Drawing.Point(881, 160);
            this.trackBarx.Maximum = 99;
            this.trackBarx.Name = "trackBarx";
            this.trackBarx.Size = new System.Drawing.Size(159, 45);
            this.trackBarx.TabIndex = 16;
            this.trackBarx.Value = 45;
            this.trackBarx.Scroll += new System.EventHandler(this.trackBarx_Scroll);
            // 
            // DebugOuput
            // 
            this.DebugOuput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DebugOuput.Location = new System.Drawing.Point(206, 439);
            this.DebugOuput.Multiline = true;
            this.DebugOuput.Name = "DebugOuput";
            this.DebugOuput.Size = new System.Drawing.Size(649, 58);
            this.DebugOuput.TabIndex = 15;
            this.DebugOuput.Visible = false;
            // 
            // vtkFormsWindowControlMain
            // 
            this.vtkFormsWindowControlMain.Location = new System.Drawing.Point(206, 26);
            this.vtkFormsWindowControlMain.Margin = new System.Windows.Forms.Padding(2);
            this.vtkFormsWindowControlMain.Name = "vtkFormsWindowControlMain";
            this.vtkFormsWindowControlMain.Size = new System.Drawing.Size(648, 408);
            this.vtkFormsWindowControlMain.TabIndex = 19;
            this.vtkFormsWindowControlMain.Text = "vtkFormsWindowControl1";
            // 
            // vtkFormsWindowControlX
            // 
            this.vtkFormsWindowControlX.Cursor = System.Windows.Forms.Cursors.Hand;
            this.vtkFormsWindowControlX.Location = new System.Drawing.Point(897, 26);
            this.vtkFormsWindowControlX.Margin = new System.Windows.Forms.Padding(2);
            this.vtkFormsWindowControlX.Name = "vtkFormsWindowControlX";
            this.vtkFormsWindowControlX.Size = new System.Drawing.Size(122, 132);
            this.vtkFormsWindowControlX.TabIndex = 20;
            this.vtkFormsWindowControlX.Text = "vtkFormsWindowControl1";
            this.vtkFormsWindowControlX.DoubleClick += new System.EventHandler(this.vtkFormsWindowControlX_DoubleClick);
            // 
            // vtkFormsWindowControlY
            // 
            this.vtkFormsWindowControlY.Cursor = System.Windows.Forms.Cursors.Hand;
            this.vtkFormsWindowControlY.Location = new System.Drawing.Point(897, 210);
            this.vtkFormsWindowControlY.Margin = new System.Windows.Forms.Padding(2);
            this.vtkFormsWindowControlY.Name = "vtkFormsWindowControlY";
            this.vtkFormsWindowControlY.Size = new System.Drawing.Size(122, 132);
            this.vtkFormsWindowControlY.TabIndex = 21;
            this.vtkFormsWindowControlY.Text = "vtkFormsWindowControl1";
            this.vtkFormsWindowControlY.DoubleClick += new System.EventHandler(this.vtkFormsWindowControlY_DoubleClick);
            // 
            // vtkFormsWindowControlZ
            // 
            this.vtkFormsWindowControlZ.Cursor = System.Windows.Forms.Cursors.Hand;
            this.vtkFormsWindowControlZ.Location = new System.Drawing.Point(897, 397);
            this.vtkFormsWindowControlZ.Margin = new System.Windows.Forms.Padding(2);
            this.vtkFormsWindowControlZ.Name = "vtkFormsWindowControlZ";
            this.vtkFormsWindowControlZ.Size = new System.Drawing.Size(122, 132);
            this.vtkFormsWindowControlZ.TabIndex = 22;
            this.vtkFormsWindowControlZ.Text = "vtkFormsWindowControl1";
            this.vtkFormsWindowControlZ.DoubleClick += new System.EventHandler(this.vtkFormsWindowControlZ_DoubleClick);
            // 
            // trackBarBrightnessX
            // 
            this.trackBarBrightnessX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarBrightnessX.Location = new System.Drawing.Point(881, 39);
            this.trackBarBrightnessX.Maximum = 99;
            this.trackBarBrightnessX.Name = "trackBarBrightnessX";
            this.trackBarBrightnessX.Size = new System.Drawing.Size(159, 45);
            this.trackBarBrightnessX.TabIndex = 16;
            this.trackBarBrightnessX.Value = 45;
            this.trackBarBrightnessX.Visible = false;
            this.trackBarBrightnessX.Scroll += new System.EventHandler(this.trackBarBrightnessX_Scroll);
            // 
            // trackBartrackBarContrastX
            // 
            this.trackBartrackBarContrastX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBartrackBarContrastX.Location = new System.Drawing.Point(881, 90);
            this.trackBartrackBarContrastX.Maximum = 99;
            this.trackBartrackBarContrastX.Name = "trackBartrackBarContrastX";
            this.trackBartrackBarContrastX.Size = new System.Drawing.Size(159, 45);
            this.trackBartrackBarContrastX.TabIndex = 16;
            this.trackBartrackBarContrastX.Value = 45;
            this.trackBartrackBarContrastX.Visible = false;
            this.trackBartrackBarContrastX.Scroll += new System.EventHandler(this.trackBartrackBarContrastX_Scroll);
            // 
            // labelWLX
            // 
            this.labelWLX.AutoSize = true;
            this.labelWLX.Location = new System.Drawing.Point(1042, 42);
            this.labelWLX.Name = "labelWLX";
            this.labelWLX.Size = new System.Drawing.Size(24, 13);
            this.labelWLX.TabIndex = 25;
            this.labelWLX.Text = "WL";
            this.labelWLX.Visible = false;
            // 
            // labelWWX
            // 
            this.labelWWX.AutoSize = true;
            this.labelWWX.Location = new System.Drawing.Point(1042, 90);
            this.labelWWX.Name = "labelWWX";
            this.labelWWX.Size = new System.Drawing.Size(29, 13);
            this.labelWWX.TabIndex = 25;
            this.labelWWX.Text = "WW";
            this.labelWWX.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1047, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "X Slices";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1042, 347);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Y Slices";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1042, 534);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Z Slices";
            // 
            // trackBarBrightnessY
            // 
            this.trackBarBrightnessY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarBrightnessY.Location = new System.Drawing.Point(881, 232);
            this.trackBarBrightnessY.Maximum = 99;
            this.trackBarBrightnessY.Name = "trackBarBrightnessY";
            this.trackBarBrightnessY.Size = new System.Drawing.Size(159, 45);
            this.trackBarBrightnessY.TabIndex = 16;
            this.trackBarBrightnessY.Value = 45;
            this.trackBarBrightnessY.Visible = false;
            this.trackBarBrightnessY.Scroll += new System.EventHandler(this.trackBarBrightnessY_Scroll);
            // 
            // trackBarContrastY
            // 
            this.trackBarContrastY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarContrastY.Location = new System.Drawing.Point(881, 283);
            this.trackBarContrastY.Maximum = 99;
            this.trackBarContrastY.Name = "trackBarContrastY";
            this.trackBarContrastY.Size = new System.Drawing.Size(159, 45);
            this.trackBarContrastY.TabIndex = 16;
            this.trackBarContrastY.Value = 45;
            this.trackBarContrastY.Visible = false;
            this.trackBarContrastY.Scroll += new System.EventHandler(this.trackBarContrastY_Scroll);
            // 
            // labelWLY
            // 
            this.labelWLY.AutoSize = true;
            this.labelWLY.Location = new System.Drawing.Point(1042, 235);
            this.labelWLY.Name = "labelWLY";
            this.labelWLY.Size = new System.Drawing.Size(24, 13);
            this.labelWLY.TabIndex = 25;
            this.labelWLY.Text = "WL";
            this.labelWLY.Visible = false;
            // 
            // labelWWY
            // 
            this.labelWWY.AutoSize = true;
            this.labelWWY.Location = new System.Drawing.Point(1042, 283);
            this.labelWWY.Name = "labelWWY";
            this.labelWWY.Size = new System.Drawing.Size(29, 13);
            this.labelWWY.TabIndex = 25;
            this.labelWWY.Text = "WW";
            this.labelWWY.Visible = false;
            // 
            // trackBarBrightnessZ
            // 
            this.trackBarBrightnessZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarBrightnessZ.Location = new System.Drawing.Point(881, 407);
            this.trackBarBrightnessZ.Maximum = 99;
            this.trackBarBrightnessZ.Name = "trackBarBrightnessZ";
            this.trackBarBrightnessZ.Size = new System.Drawing.Size(159, 45);
            this.trackBarBrightnessZ.TabIndex = 16;
            this.trackBarBrightnessZ.Value = 45;
            this.trackBarBrightnessZ.Visible = false;
            this.trackBarBrightnessZ.Scroll += new System.EventHandler(this.trackBarBrightnessZ_Scroll);
            // 
            // trackBarContrastZ
            // 
            this.trackBarContrastZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarContrastZ.Location = new System.Drawing.Point(881, 458);
            this.trackBarContrastZ.Maximum = 99;
            this.trackBarContrastZ.Name = "trackBarContrastZ";
            this.trackBarContrastZ.Size = new System.Drawing.Size(159, 45);
            this.trackBarContrastZ.TabIndex = 16;
            this.trackBarContrastZ.Value = 45;
            this.trackBarContrastZ.Visible = false;
            this.trackBarContrastZ.Scroll += new System.EventHandler(this.trackBarContrastZ_Scroll);
            // 
            // labelWLZ
            // 
            this.labelWLZ.AutoSize = true;
            this.labelWLZ.Location = new System.Drawing.Point(1042, 410);
            this.labelWLZ.Name = "labelWLZ";
            this.labelWLZ.Size = new System.Drawing.Size(24, 13);
            this.labelWLZ.TabIndex = 25;
            this.labelWLZ.Text = "WL";
            this.labelWLZ.Visible = false;
            // 
            // labelWWZ
            // 
            this.labelWWZ.AutoSize = true;
            this.labelWWZ.Location = new System.Drawing.Point(1042, 458);
            this.labelWWZ.Name = "labelWWZ";
            this.labelWWZ.Size = new System.Drawing.Size(29, 13);
            this.labelWWZ.TabIndex = 25;
            this.labelWWZ.Text = "WW";
            this.labelWWZ.Visible = false;
            // 
            // buttonSetRangeChoosenSlice
            // 
            this.buttonSetRangeChoosenSlice.Location = new System.Drawing.Point(206, 543);
            this.buttonSetRangeChoosenSlice.Name = "buttonSetRangeChoosenSlice";
            this.buttonSetRangeChoosenSlice.Size = new System.Drawing.Size(106, 32);
            this.buttonSetRangeChoosenSlice.TabIndex = 27;
            this.buttonSetRangeChoosenSlice.Text = "Set range";
            this.buttonSetRangeChoosenSlice.UseVisualStyleBackColor = true;
            this.buttonSetRangeChoosenSlice.Visible = false;
            this.buttonSetRangeChoosenSlice.Click += new System.EventHandler(this.buttonSetRangeChoosenSlice_Click);
            // 
            // buttonSaveCurrentPlane
            // 
            this.buttonSaveCurrentPlane.Location = new System.Drawing.Point(318, 543);
            this.buttonSaveCurrentPlane.Name = "buttonSaveCurrentPlane";
            this.buttonSaveCurrentPlane.Size = new System.Drawing.Size(222, 32);
            this.buttonSaveCurrentPlane.TabIndex = 28;
            this.buttonSaveCurrentPlane.Text = "Open current image for preprocessing";
            this.buttonSaveCurrentPlane.UseVisualStyleBackColor = true;
            this.buttonSaveCurrentPlane.Visible = false;
            this.buttonSaveCurrentPlane.Click += new System.EventHandler(this.buttonSaveCurrentPlane_Click);
            // 
            // openPreprocessingApplicationToolStripMenuItem
            // 
            this.openPreprocessingApplicationToolStripMenuItem.Name = "openPreprocessingApplicationToolStripMenuItem";
            this.openPreprocessingApplicationToolStripMenuItem.Size = new System.Drawing.Size(187, 20);
            this.openPreprocessingApplicationToolStripMenuItem.Text = "Open preprocessing application";
            this.openPreprocessingApplicationToolStripMenuItem.Click += new System.EventHandler(this.openPreprocessingApplicationToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 584);
            this.Controls.Add(this.buttonSaveCurrentPlane);
            this.Controls.Add(this.buttonSetRangeChoosenSlice);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelWWZ);
            this.Controls.Add(this.labelWWY);
            this.Controls.Add(this.labelWWX);
            this.Controls.Add(this.labelWLZ);
            this.Controls.Add(this.labelWLY);
            this.Controls.Add(this.labelWLX);
            this.Controls.Add(this.vtkFormsWindowControlZ);
            this.Controls.Add(this.vtkFormsWindowControlY);
            this.Controls.Add(this.vtkFormsWindowControlX);
            this.Controls.Add(this.vtkFormsWindowControlMain);
            this.Controls.Add(this.trackBarz);
            this.Controls.Add(this.trackBary);
            this.Controls.Add(this.trackBarContrastZ);
            this.Controls.Add(this.trackBarContrastY);
            this.Controls.Add(this.trackBartrackBarContrastX);
            this.Controls.Add(this.trackBarBrightnessZ);
            this.Controls.Add(this.trackBarBrightnessY);
            this.Controls.Add(this.trackBarBrightnessX);
            this.Controls.Add(this.trackBarx);
            this.Controls.Add(this.DebugOuput);
            this.Controls.Add(this.VolumeRenderPanel);
            this.Controls.Add(this.IsoSurfacePanel);
            this.Controls.Add(this.Readypanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1122, 623);
            this.MinimumSize = new System.Drawing.Size(1122, 623);
            this.Name = "Form1";
            this.Text = "DICOM Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Readypanel.ResumeLayout(false);
            this.Readypanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.IsoSurfacePanel.ResumeLayout(false);
            this.IsoSurfacePanel.PerformLayout();
            this.IsoAdvancedPanel.ResumeLayout(false);
            this.IsoAdvancedPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IsotrackBar)).EndInit();
            this.VolumeRenderPanel.ResumeLayout(false);
            this.VolumeRenderPanel.PerformLayout();
            this.VolumeRenderAdvancedPanel.ResumeLayout(false);
            this.VolumeRenderAdvancedPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightnessX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBartrackBarContrastX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightnessY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrastY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightnessZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrastZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel Readypanel;
        public System.Windows.Forms.Label ReadyLabel;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem openDICOMToolStripMenuItem;
        public System.Windows.Forms.OpenFileDialog openDicomFileDialog;
        public System.Windows.Forms.Timer LoadTimer;
        public System.Windows.Forms.Panel IsoSurfacePanel;
        public System.Windows.Forms.Panel IsoAdvancedPanel;
        public System.Windows.Forms.RadioButton isoButton3;
        public System.Windows.Forms.RadioButton isoButton2;
        public System.Windows.Forms.RadioButton isoButton1;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox IsoBox;
        public System.Windows.Forms.TrackBar IsotrackBar;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Panel VolumeRenderPanel;
        public System.Windows.Forms.Panel VolumeRenderAdvancedPanel;
        public System.Windows.Forms.RadioButton RenAdvButton3;
        public System.Windows.Forms.RadioButton RenAdvButton2;
        public System.Windows.Forms.RadioButton RenAdvButton1;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.RadioButton renderingButton3;
        public System.Windows.Forms.RadioButton renderingButton2;
        public System.Windows.Forms.RadioButton renderingButton1;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TrackBar trackBarz;
        public System.Windows.Forms.TrackBar trackBary;
        public System.Windows.Forms.TrackBar trackBarx;
        public System.Windows.Forms.TextBox DebugOuput;
        public vtk.vtkFormsWindowControl vtkFormsWindowControlMain;
        public vtk.vtkFormsWindowControl vtkFormsWindowControlX;
        public vtk.vtkFormsWindowControl vtkFormsWindowControlY;
        public vtk.vtkFormsWindowControl vtkFormsWindowControlZ;
        public System.Windows.Forms.ToolStripMenuItem planesToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem surfaceToolStripMenuItem;
        public System.Windows.Forms.BindingSource bindingSource1;
        public System.Windows.Forms.TrackBar trackBarBrightnessX;
        public System.Windows.Forms.TrackBar trackBartrackBarContrastX;
        private System.Windows.Forms.Label labelWLX;
        private System.Windows.Forms.Label labelWWX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TrackBar trackBarBrightnessY;
        public System.Windows.Forms.TrackBar trackBarContrastY;
        private System.Windows.Forms.Label labelWLY;
        private System.Windows.Forms.Label labelWWY;
        public System.Windows.Forms.TrackBar trackBarBrightnessZ;
        public System.Windows.Forms.TrackBar trackBarContrastZ;
        private System.Windows.Forms.Label labelWLZ;
        private System.Windows.Forms.Label labelWWZ;
        private System.Windows.Forms.Button buttonSetRangeChoosenSlice;
        private System.Windows.Forms.Button buttonSaveCurrentPlane;
        private System.Windows.Forms.ToolStripMenuItem setTemporaryFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPreprocessingApplicationToolStripMenuItem;
    }
}

