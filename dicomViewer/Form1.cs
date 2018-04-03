using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading; 
using vtk;
using System.IO;
using System.Security.Cryptography;
namespace dicomViewer
{
    public partial class Form1 : Form
    {

        int readtype = 0;
        string SelectedPath = "";
        RenderPlanes RenderPlanesObj = new RenderPlanes();
        RenderVolume RenderVolumeObj = new RenderVolume();
        public Siteplanes SiteplanesObj = new Siteplanes(); 
        public readVoxelData VolumeFileObj = new readVoxelData();
        RenderMarchingCubes RenderMarchingCubesObj = new RenderMarchingCubes();
        Histogram HistogramObj = new Histogram();
        // Selected x,y,z slices in the small side windows
        public int slizez = 0;
        public int slizey = 0;
        public int slizex = 0;
        public int clickWindowX = 0;
        public int clickWindowY = 0;
        public int clickWindowZ = 0;
        int currentSliceDimension = 0;
        int CurrentRender = -1; // The current type of render





        public Form1()
        {
            InitializeComponent();
            string exedir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string sampledir = exedir + "\\Example Data";
            if (System.IO.Directory.Exists(sampledir))
            {
                openDicomFileDialog.InitialDirectory = sampledir;
                //openV3DFileDialog.InitialDirectory = sampledir;
            }
            else
            {
                openDicomFileDialog.InitialDirectory = exedir;
                //openV3DFileDialog.InitialDirectory = exedir;
            }

            // Send this form as object to the classes, 
            // so the classes can control the form

            RenderPlanesObj.setParent(this);
            RenderVolumeObj.setParent(this);
            RenderMarchingCubesObj.setParent(this);

        }
        
        public void ShowBusy(bool b)
        {
            if (b)
            {
                Readypanel.BackColor = Color.Red;
                ReadyLabel.Text = "Busy";
            }
            else
            {
                Readypanel.BackColor = Color.Lime;
                ReadyLabel.Text = "Ready";
            }
            Readypanel.Refresh();
            ReadyLabel.Refresh();
        }

        private void openDICOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ConsoleText("Folder Dialog Open\r\n");

            openDicomFileDialog.ShowDialog();
        }



        public void stopCurrentRender()
        {
            if (CurrentRender == 0)
            {
                RenderPlanesObj.deleterender();
            }
            if (CurrentRender == 1)
            {
                RenderVolumeObj.deleterender();
                IsoSurfacePanel.Visible = false;
                IsoSurfacePanel.Enabled = false;
                IsoAdvancedPanel.Visible = false;
                IsoAdvancedPanel.Enabled = false;
            }
            if (CurrentRender == 2)
            {
                RenderMarchingCubesObj.deleterender();
                IsoSurfacePanel.Visible = false;
                IsoSurfacePanel.Enabled = false;
            }
            if (CurrentRender == 3)
            {
                RenderVolumeObj.deleterender();
                VolumeRenderPanel.Visible = false;
                VolumeRenderPanel.Enabled = false;
                RenderVolumeObj.deleterender();
            }
            GC.Collect();
        }


        private void openDicomFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            stopCurrentRender();

            Program.IniObj.IniWriteValue("Info", "dicom_folder",Path.GetDirectoryName( openDicomFileDialog.FileName ));

            string tempfile = openDicomFileDialog.FileName;
            tempfile = tempfile.Replace("/", "\\");
            string[] fileparts = tempfile.Split('\\');
            string path = "";
            for (int i = 0; i < fileparts.Length - 1; i++)
            {
                path += fileparts[i] + "\\";
            }
            SelectedPath = path;


            readtype = 1;
            openDICOMToolStripMenuItem.Enabled = true;
            LoadTimer.Enabled = true; ShowBusy(true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void LoadProcess()
        {
            ShowBusy(true);
            if (readtype == 0)
            {
                //VolumeFileObj.ReadVTKV3D(openV3DFileDialog.FileName);
            }
            else
            {
                VolumeFileObj.readVTKDicom(SelectedPath);
            }

            //ConsoleText(VolumeFileObj.info);
           // ConsoleText("\r\nVTK voxel data connected \r\n");

            

            UpdateSliceNumber();
            SiteplanesObj.SetWindows(vtkFormsWindowControlX.GetRenderWindow(), vtkFormsWindowControlY.GetRenderWindow(), vtkFormsWindowControlZ.GetRenderWindow());
            SiteplanesObj.SetParameters(slizex, slizey, slizez);
            SiteplanesObj.rendersiteplanes(VolumeFileObj.VoxelData);

            trackBarBrightnessX.Minimum = (int)(SiteplanesObj.rangeXMin*2.5);
            trackBarBrightnessX.Maximum = (-1) * trackBarBrightnessX.Minimum;
            trackBarBrightnessX.Value = 0;

            trackBartrackBarContrastX.Maximum = (int)SiteplanesObj.rangeXMax*15;
            trackBartrackBarContrastX.Value = (int)SiteplanesObj.windowX;

            trackBarBrightnessY.Minimum = (int)(SiteplanesObj.rangeYMin * 2.5);
            trackBarBrightnessY.Maximum = (-1) * trackBarBrightnessY.Minimum;
            trackBarBrightnessY.Value = 0;

            trackBarContrastY.Maximum = (int)SiteplanesObj.rangeYMax * 15;
            trackBarContrastY.Value = (int)SiteplanesObj.windowY;

            trackBarBrightnessZ.Minimum = (int)(SiteplanesObj.rangeZMin * 2.5);
            trackBarBrightnessZ.Maximum = (-1) * trackBarBrightnessZ.Minimum;
            trackBarBrightnessZ.Value = 0;

            trackBarContrastZ.Maximum = (int)SiteplanesObj.rangeZMax * 15;
            trackBarContrastZ.Value = (int)SiteplanesObj.windowZ;
            //ConsoleText("\r\nVTK displayed the slices \r\n");

            /*
            HistogramObj.SetWindows(vtkFormsWindowControlHistogram.GetRenderWindow());
            HistogramObj.renderHistogram(VolumeFileObj.VoxelData);
            */
           // ConsoleText(HistogramObj.info);

            CurrentRender = 0;
            RenderPlanesProcess();
            ShowBusy(false);
            GC.Collect();
            //HistogramObj.starteventloop();
        }

       public void UpdateSliceNumber()
        {
            slizex = (int)((double)trackBarx.Value * (double)VolumeFileObj.dimensions[0] / 100);
            slizey = (int)((double)trackBary.Value * (double)VolumeFileObj.dimensions[1] / 100);
            slizez = (int)((double)trackBarz.Value * (double)VolumeFileObj.dimensions[2] / 100);
        }

        private void RenderPlanesProcess()
        {
            ShowBusy(true);
            RenderPlanesObj.SetWindows(vtkFormsWindowControlMain.GetRenderWindow());
            RenderPlanesObj.SetParameters(slizex, slizey, slizez);
            RenderPlanesObj.render3Dplanes(VolumeFileObj.VoxelData);
            GC.Collect();
            ShowBusy(false);
        }

        private void LoadTimer_Tick(object sender, EventArgs e)
        {
            LoadTimer.Enabled = false;
            LoadProcess();
        }

        private void planesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentRender != 0)
            {
                stopCurrentRender();
                RenderPlanesObj = new RenderPlanes();
                CurrentRender = 0;
                RenderPlanesProcess();
            }
        }

        private void surfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentRender != 1)
            {
                stopCurrentRender();
                CurrentRender = 1;
                IsoSurfacePanel.Visible = true;
                IsoSurfacePanel.Enabled = true;
                IsoAdvancedPanel.Visible = true;
                IsoAdvancedPanel.Enabled = true;
                RenderIsoProcess();
            }
        }

        private void RenderIsoProcess()
        {
            ShowBusy(true);
           
            RenderVolumeObj.setisovalue(VolumeFileObj.metaScalarRange);
            showiso(RenderVolumeObj.isovalue);

            RenderVolumeObj.SetWindows(vtkFormsWindowControlMain.GetRenderWindow());
            RenderVolumeObj.VolumeRenderISO(VolumeFileObj.VoxelData);
           
            GC.Collect();
            ShowBusy(false);
        }
        private void showiso(double isovalue)
        {
            IsoBox.Text = Convert.ToString((int)isovalue);
            IsotrackBar.Value = (int)(100 * (isovalue - (double)VolumeFileObj.metaScalarRange[0]) / ((double)VolumeFileObj.metaScalarRange[1] - (double)VolumeFileObj.metaScalarRange[0]));
        }

        private void IsoBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {
                ShowBusy(true);
                double isovalue = Convert.ToDouble(IsoBox.Text);
                if (isovalue < VolumeFileObj.metaScalarRange[0]) isovalue = VolumeFileObj.metaScalarRange[0];
                if (isovalue > VolumeFileObj.metaScalarRange[1]) isovalue = VolumeFileObj.metaScalarRange[1];
                showiso(isovalue);
                if (CurrentRender == 1)
                {
                    RenderVolumeObj.UpdateIsoValue(isovalue);
                }
                else
                {
                    RenderMarchingCubesObj.UpdateIsoValue(isovalue);
                }
                ShowBusy(false);
                e.Handled = true;
            }

        }

        private void trackBarx_Scroll(object sender, EventArgs e)
        {
            UpdateSliceNumber();
            SiteplanesObj.deleterender();
            SiteplanesObj.rendersiteplanes(VolumeFileObj.VoxelData);
            if (CurrentRender == 0) RenderPlanesObj.updateX(slizex);
            SiteplanesObj.updateX(slizex);
            this.Invalidate();
        }

        private void trackBary_Scroll(object sender, EventArgs e)
        {
            UpdateSliceNumber();
            SiteplanesObj.deleterender();
            SiteplanesObj.rendersiteplanes(VolumeFileObj.VoxelData);
            if (CurrentRender == 0) RenderPlanesObj.updateY(slizey);
            SiteplanesObj.updateY(slizey);
        }

        private void trackBarz_Scroll(object sender, EventArgs e)
        {
            UpdateSliceNumber();
            SiteplanesObj.deleterender();
            SiteplanesObj.rendersiteplanes(VolumeFileObj.VoxelData);
            
            if (CurrentRender == 0) RenderPlanesObj.updateZ(slizez);
            SiteplanesObj.updateZ(slizez);
        }

        private void isoButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void isoButton1_Click(object sender, EventArgs e)
        {
            ShowBusy(true);
            RenderVolumeObj.setIsomode(0);
            ShowBusy(false);
        }

        private void isoButton2_Click(object sender, EventArgs e)
        {
            ShowBusy(true);
            RenderVolumeObj.setIsomode(1);
            ShowBusy(false);
        }

        private void isoButton3_Click(object sender, EventArgs e)
        {
            ShowBusy(true);
            RenderVolumeObj.setIsomode(2);
            ShowBusy(false);
        }

        private void IsotrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            ShowBusy(true);
            double sliderv = (double)IsotrackBar.Value / 100;
            double isovalue = VolumeFileObj.metaScalarRange[1] * sliderv + (1 - sliderv) * VolumeFileObj.metaScalarRange[0];
            showiso(isovalue);
            if (CurrentRender == 1)
            {
                RenderVolumeObj.UpdateIsoValue(isovalue);
            }
            else
            {
                RenderMarchingCubesObj.UpdateIsoValue(isovalue);
            }
            ShowBusy(false);
        }

        private void dICOMToStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void vtkFormsWindowControlX_DoubleClick(object sender, EventArgs e)
        {
            if (clickWindowX == 0)
            {
                clickWindowX = 1;
                currentSliceDimension = 1;
                vtkFormsWindowControlMain.Visible = false;

                buttonSaveCurrentPlane.Visible = true;
                buttonSetRangeChoosenSlice.Visible = true;
                trackBarBrightnessX.Visible = true;
                trackBartrackBarContrastX.Visible = true;
                labelWLX.Visible = true;
                labelWWX.Visible = true;

                vtkFormsWindowControlX.Size = new Size(500, 500);
                vtkFormsWindowControlX.Location = new Point(this.Width / 2 - vtkFormsWindowControlX.Width / 2, 30);
                
                SiteplanesObj.deleterender();
                SiteplanesObj.rendersiteplanes(VolumeFileObj.VoxelData);
                this.Invalidate();
                trackBarx_Scroll(sender,e);
            }
            else if (clickWindowX == 1)
            {
                vtkFormsWindowControlMain.Visible = true;

                buttonSaveCurrentPlane.Visible = false;
                buttonSetRangeChoosenSlice.Visible = false;
                trackBarBrightnessX.Visible = false;
                trackBartrackBarContrastX.Visible = false;
                labelWLX.Visible = false;
                labelWWX.Visible = false;
                clickWindowX = 0;
                vtkFormsWindowControlX.Size = new Size(122, 132);
                vtkFormsWindowControlX.Location = new Point(897, 26);
                this.Invalidate();
                trackBarx_Scroll(sender, e);
            }

           

        }

        private void vtkFormsWindowControlY_DoubleClick(object sender, EventArgs e)
        {
            if (clickWindowY == 0)
            {
                clickWindowY = 1;
                currentSliceDimension = 2;
                buttonSaveCurrentPlane.Visible = true;
                buttonSetRangeChoosenSlice.Visible = true;
                trackBarBrightnessY.Visible = true;
                trackBarContrastY.Visible = true;
                labelWLY.Visible = true;
                labelWWY.Visible = true;


                /*SiteplanesObj.updateYContrast(1500);
                SiteplanesObj.updateYBtightness(-600);*/

                vtkFormsWindowControlY.Size = new Size(500, 500);
                vtkFormsWindowControlY.Location = new Point(this.Width / 2 - vtkFormsWindowControlY.Width / 2, 30);
                SiteplanesObj.deleterender();
                SiteplanesObj.rendersiteplanes(VolumeFileObj.VoxelData);
                this.Invalidate();
                trackBary_Scroll(sender, e);
            }
            else if (clickWindowY == 1)
            {
                clickWindowY = 0;

                buttonSaveCurrentPlane.Visible = false;
                buttonSetRangeChoosenSlice.Visible = false;
                trackBarBrightnessY.Visible = false;
                trackBarContrastY.Visible = false;
                labelWLY.Visible = false;
                labelWWY.Visible = false;

                vtkFormsWindowControlY.Size = new Size(122, 132);
                vtkFormsWindowControlY.Location = new Point(897, 210);
                this.Invalidate();
                trackBary_Scroll(sender, e);
            }
        }

        private void vtkFormsWindowControlZ_DoubleClick(object sender, EventArgs e)
        {
            if (clickWindowZ == 0)
            {
                clickWindowZ = 1;
                currentSliceDimension = 3;
                buttonSaveCurrentPlane.Visible = true;
                buttonSetRangeChoosenSlice.Visible = true;
                trackBarBrightnessZ.Visible = true;
                trackBarContrastZ.Visible = true;
                labelWLZ.Visible = true;
                labelWWZ.Visible = true;

                vtkFormsWindowControlZ.Size = new Size(500, 500);
                vtkFormsWindowControlZ.Location = new Point(this.Width / 2 - vtkFormsWindowControlZ.Width / 2, 30);
                SiteplanesObj.deleterender();
                SiteplanesObj.rendersiteplanes(VolumeFileObj.VoxelData);




                


                /*this is haunsfield values for python
                SiteplanesObj.updateZContrast(2000);
                SiteplanesObj.updateZBtightness(300);*/



                this.Invalidate();
                trackBarz_Scroll(sender, e);
            }
            else if (clickWindowZ == 1)
            {
                clickWindowZ = 0;

                buttonSaveCurrentPlane.Visible = false;
                buttonSetRangeChoosenSlice.Visible = false;
                trackBarBrightnessZ.Visible = false;
                trackBarContrastZ.Visible = false;
                labelWLZ.Visible = false;
                labelWWZ.Visible = false;

                vtkFormsWindowControlZ.Size = new Size(122, 132);
                vtkFormsWindowControlZ.Location = new Point(897, 397);
                this.Invalidate();
                trackBarz_Scroll(sender, e);
            }
        }

        private void trackBarBrightnessX_Scroll(object sender, EventArgs e)
        {
            //double level = (1-trackBarBrightnessX.Value/100)*(SiteplanesObj.rangeXMax - SiteplanesObj.rangeXMin) + SiteplanesObj.rangeXMin;
            double level = trackBarBrightnessX.Value;
            SiteplanesObj.updateXBtightness(level);
            this.Invalidate();
        }

        private void trackBartrackBarContrastX_Scroll(object sender, EventArgs e)
        {
            //double window = (1 - trackBartrackBarContrastX.Value / 100) * (SiteplanesObj.rangeXMax - SiteplanesObj.rangeXMin);
            double window = trackBartrackBarContrastX.Value;
            SiteplanesObj.updateXContrast(window);
            this.Invalidate();
        }

        private void trackBarBrightnessY_Scroll(object sender, EventArgs e)
        {
            double level = trackBarBrightnessY.Value;
            SiteplanesObj.updateYBtightness(level);
            this.Invalidate();
        }

        private void trackBarContrastY_Scroll(object sender, EventArgs e)
        {
            double window = trackBarContrastY.Value;
            SiteplanesObj.updateYContrast(window);
            this.Invalidate();
        }

        private void trackBarBrightnessZ_Scroll(object sender, EventArgs e)
        {
            double level = trackBarBrightnessZ.Value;
            SiteplanesObj.updateZBtightness(level);
            this.Invalidate();
        }

        private void trackBarContrastZ_Scroll(object sender, EventArgs e)
        {
            double window = trackBarContrastZ.Value;
            SiteplanesObj.updateZContrast(window);
            this.Invalidate();
        }

        private void buttonSetRangeChoosenSlice_Click(object sender, EventArgs e)
        {
            SiteplanesObj.updateRangeTableWigdet(currentSliceDimension);
            this.Invalidate();
        }

        public string checkMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(filename));
                StringBuilder result = new StringBuilder(bytes.Length * 2);

                for (int i = 0; i < bytes.Length; i++)
                    result.Append(bytes[i].ToString("X2"));

                return result.ToString();
            }
        }

        private void buttonSaveCurrentPlane_Click(object sender, EventArgs e)
        {
            //make dir
            Random RandNum = new Random();
            string HashForDir = checkMD5(DateTime.Now.ToLongDateString() + Convert.ToString(RandNum.Next(1111, 9999)));
            Directory.CreateDirectory(Program.TempDir + "\\" + HashForDir);
            StreamWriter IniFileInDir = File.CreateText(Program.TempDir + "\\" + HashForDir + "\\Ini.ini");
            IniFileInDir.WriteLine("[Info]");
            IniFileInDir.WriteLine(@"temp_folder="+ Program.TempDir +"\n" +
                                    "max_scalar = \"\"\n"+
                                    "min_scalar = \"\"\n"+
                                    @"dicom_folder =" + Path.GetDirectoryName( openDicomFileDialog.FileName ));
            IniFileInDir.Close();

            Program.IniObj.IniWriteValue("Info", "curr_work_dir", HashForDir);

            Ini localIni = new Ini(Program.TempDir + "\\" + HashForDir + "\\Ini.ini");

            //save scalar data of selected image
            StreamWriter streamWriter = new StreamWriter(Program.TempDir + "\\" + HashForDir + "\\" + "imageScalars.txt");
            for (int i = 0; i < 500; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    streamWriter.Write(Convert.ToString(
                        SiteplanesObj.planeWidgetZ.GetResliceOutput().GetScalarComponentAsDouble(i, j, 0, 0)
                        ) + " ");
                }
                streamWriter.Write("\n");
            }
            streamWriter.Close();

            //save image in png file
            vtkWindowToImageFilter imf = new vtkWindowToImageFilter();
            imf.SetInputBufferTypeToRGBA();
            switch (currentSliceDimension)
            {
                case 1:
                    {
                        imf.SetInput(vtkFormsWindowControlX.GetRenderWindow());
                        localIni.IniWriteValue("Info", "max_scalar",Convert.ToString( SiteplanesObj.rangeXMax ));
                        localIni.IniWriteValue("Info", "min_scalar", Convert.ToString(SiteplanesObj.rangeXMin));
                        break;
                    }
                case 2:
                    {
                        imf.SetInput(vtkFormsWindowControlY.GetRenderWindow());
                        localIni.IniWriteValue("Info", "max_scalar", Convert.ToString(SiteplanesObj.rangeYMax));
                        localIni.IniWriteValue("Info", "min_scalar", Convert.ToString(SiteplanesObj.rangeYMin));
                        break;
                    }
                case 3:
                    {
                        imf.SetInput(vtkFormsWindowControlZ.GetRenderWindow());
                        localIni.IniWriteValue("Info", "max_scalar", Convert.ToString(SiteplanesObj.rangeZMax));
                        localIni.IniWriteValue("Info", "min_scalar", Convert.ToString(SiteplanesObj.rangeZMin));
                        break;
                    }
            }
            vtkPNGWriter pngw = new vtkPNGWriter();
            pngw.SetFileName(Program.TempDir + "\\" + HashForDir + "\\" + "\\image.png");
            pngw.SetInputConnection(imf.GetOutputPort());
            pngw.Write();

            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "imagePreprocessing.exe");

            /*get CT values from
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png|tif files (*.tif)|*.tif|tiff files (*.tiff)|*.tiff";
            saveFileDialog1.FilterIndex = 2;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {           
                


                //get CT values from current slice
                StreamWriter streamWriter = new StreamWriter(Path.GetDirectoryName(saveFileDialog1.FileName) + "\\" + "imageScalars.txt");
                for(int i = 0; i < 500; i++)
                {
                    for(int j = 0; j < 500; j++)
                    {
                        streamWriter.Write(Convert.ToString(
                            SiteplanesObj.planeWidgetZ.GetResliceOutput().GetScalarComponentAsDouble(i, j, 0, 0)
                            ) + " ");
                    }
                    streamWriter.Write("\n");
                }
                streamWriter.Close();
                //get CT values from current slice



                vtkWindowToImageFilter imf = new vtkWindowToImageFilter();
                imf.SetInputBufferTypeToRGBA();
                switch (currentSliceDimension)
                {
                    case 1:
                        {
                            imf.SetInput(vtkFormsWindowControlX.GetRenderWindow());
                            break;
                        }
                    case 2:
                        {
                            imf.SetInput(vtkFormsWindowControlY.GetRenderWindow());
                            break;
                        }
                    case 3:
                        {
                            imf.SetInput(vtkFormsWindowControlZ.GetRenderWindow());
                            break;
                        }
                }

                
                var estns = Path.GetExtension(saveFileDialog1.FileName);
                if (estns == ".jpg")
                {
                    vtkJPEGWriter pngw = new vtkJPEGWriter();
                    pngw.SetFileName(saveFileDialog1.FileName);
                    pngw.SetInputConnection(imf.GetOutputPort());
                    pngw.Write();
                }
                else if (estns == ".png")
                {
                    vtkPNGWriter pngw = new vtkPNGWriter();
                    pngw.SetFileName(saveFileDialog1.FileName);
                    pngw.SetInputConnection(imf.GetOutputPort());
                    pngw.Write();
                }
                else if (estns == ".tif" || estns == ".tiff")
                {
                    vtkTIFFWriter pngw = new vtkTIFFWriter();
                    pngw.SetFileName(saveFileDialog1.FileName);
                    pngw.SetInputConnection(imf.GetOutputPort());
                    pngw.Write();
                }
                
            }*/
        }

        private void setTemporaryFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Program.setTForm.Show();
        }

        private void openPreprocessingApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "imagePreprocessing.exe");
        }
    }


}
