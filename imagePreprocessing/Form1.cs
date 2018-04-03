using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Net.Http;
using Newtonsoft.Json;

using System.Security.Cryptography;

namespace imagePreprocessing
{
    public partial class Form1 : Form
    {

        public string filenameFirstImage = "";
        public List<double> arrNormalizeImage = new List<double>();

        int slope = 0;
        int intercept = 0;

        int minScalar = 0;
        int maxScalar = 0;

        int x_sq = -1; int y_sq = -1;int width_sq = -1; int height_sq = -1;

        int[,] CTvaluesOfImage = new int[500, 500];

        Ini IniObjMain = null;
        Ini IniObjCurrent = null;
        string TempDir = "";
        string CurrentHash = "";
        string DICOMDir = "";

        string ApiURL = "";

        int[,] ArrWithPredInt = new int[388, 388];

        public Form1()
        {
            InitializeComponent();

            Slopelabel.Text = "NaN";
            Interceptlabel.Text = "NaN";
            Maxscalarlabel.Text = "NaN";
            Minscalarlabel.Text = "NaN";


            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Ini.ini"))
            {
                IniObjMain = new Ini(AppDomain.CurrentDomain.BaseDirectory + "Ini.ini");
                ApiURL = IniObjMain.IniReadValue("Info", "api_url");
                CurrentHash = IniObjMain.IniReadValue("Info", "curr_work_dir");
                TempDir = IniObjMain.IniReadValue("Info", "temp_folder") + "\\" + CurrentHash;
                IniObjCurrent = new Ini(TempDir + "\\" + "Ini.ini");
                minScalar = Convert.ToInt32(IniObjCurrent.IniReadValue("Info", "min_scalar"));
                maxScalar = Convert.ToInt32(IniObjCurrent.IniReadValue("Info", "max_scalar"));
                DICOMDir = IniObjCurrent.IniReadValue("Info", "dicom_folder");
                if (DICOMDir != "")
                {
                    string[] fileEntriesDICOM = Directory.GetFiles(DICOMDir);
                    foreach (string file in fileEntriesDICOM)
                    {
                        DicomTagParser.DICOMParser myDP = new DicomTagParser.DICOMParser(file);
                        System.Xml.Linq.XDocument mdoc = myDP.GetXDocument();
                        var slope_this = GetElement(mdoc, "(0028,1053)");
                        var intercept_this = GetElement(mdoc, "(0028,1052)");
                        if (slope_this == null || intercept_this == null)
                        {
                            continue;
                        }
                        else
                        {
                            slope = Convert.ToInt32(slope_this);
                            intercept = Convert.ToInt32(intercept_this);
                            Slopelabel.Text = slope_this;
                            Interceptlabel.Text = intercept_this;
                            Maxscalarlabel.Text = Convert.ToString(maxScalar);
                            Minscalarlabel.Text = Convert.ToString(minScalar);
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Variable dicom_folder does not set!");
                }
            }
            else
            {
                MessageBox.Show("Ini file does not exist!");
            }
        }

        //function for get tags from parsed DICOM
        public static String GetElement(System.Xml.Linq.XDocument doc, string elementName)
        {
            foreach (XNode node in doc.DescendantNodes())
            {
                if (node is XElement)
                {
                    XElement element = (XElement)node;
                    if (element.Name.LocalName.Equals("DataElement"))
                    {
                        if (element.Attribute("Tag").Value == elementName)
                        {
                            return element.Attribute("Data").Value;
                        }
                    }
                }
            }
            return null;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Program.arrayForm.Show();
        }

        public void readCTvalues(string filename)
        {
            StreamReader myReader = new StreamReader(filename);
            string currentLine = "";

            int line = 0;
            while ((currentLine = myReader.ReadLine()) != null)
            {
                string[] arrCurrentLine = currentLine.Split(' ');

                for (int i = 0; i < arrCurrentLine.Length - 1; i++)
                {
                    CTvaluesOfImage[line, 499 - i] = Convert.ToInt32(arrCurrentLine[i]);
                }
                line++;
            }
            myReader.Close();
        }

        public Bitmap paddingReflect(Bitmap image, int reflectParam)
        {
            Bitmap resBtm = new Bitmap(image.Width + reflectParam, image.Height + reflectParam);

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    resBtm.SetPixel(i + reflectParam / 2, j + reflectParam / 2, Color.FromArgb(Convert.ToInt32(image.GetPixel(i, j).R),
                        Convert.ToInt32(image.GetPixel(i, j).R),
                        Convert.ToInt32(image.GetPixel(i, j).R)));
                }
            }

            //top
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < reflectParam / 2; j++)
                {
                    resBtm.SetPixel(i + reflectParam / 2, reflectParam / 2 - j,
                        Color.FromArgb(
                            Convert.ToInt32(image.GetPixel(i, j).R),
                            Convert.ToInt32(image.GetPixel(i, j).R),
                            Convert.ToInt32(image.GetPixel(i, j).R)
                    ));
                }
            }

            //left
            for (int i = 0; i < reflectParam / 2; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    resBtm.SetPixel(reflectParam / 2 - i, j + reflectParam / 2,
                        Color.FromArgb(
                            Convert.ToInt32(image.GetPixel(i, j).R),
                            Convert.ToInt32(image.GetPixel(i, j).R),
                            Convert.ToInt32(image.GetPixel(i, j).R)
                    ));
                }
            }

            //right
            for (int i = image.Width - reflectParam / 2; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    resBtm.SetPixel(resBtm.Width - (i - (image.Width - reflectParam / 2)) - 1, j + reflectParam / 2,
                        Color.FromArgb(
                            Convert.ToInt32(image.GetPixel(i, j).R),
                            Convert.ToInt32(image.GetPixel(i, j).R),
                            Convert.ToInt32(image.GetPixel(i, j).R)
                    ));
                }
            }


            //bottom
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = image.Height - reflectParam / 2; j < image.Height; j++)
                {
                    resBtm.SetPixel(i + reflectParam / 2, resBtm.Height - (j - (image.Height - reflectParam / 2)) - 1,
                        Color.FromArgb(
                            Convert.ToInt32(image.GetPixel(i, j).R),
                            Convert.ToInt32(image.GetPixel(i, j).R),
                            Convert.ToInt32(image.GetPixel(i, j).R)
                    ));
                }
            }

            return resBtm;
        }

        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(sourceBMP, 0, 0, width, height);
            }
            return result;
        }

        public Bitmap clipPixels(Bitmap image, int[] houBorders)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);

            if (File.Exists(TempDir + "\\imageScalars.txt"))
            {
                readCTvalues(TempDir + "\\imageScalars.txt");

                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        //CTvaluesOfImage - повернута на 90 градусов по часовой
                        //if (CTvaluesOfImage[j,(image.Height - 1) - i] * slope + intercept > 1200)
                        //if (CTvaluesOfImage[j, (image.Height - 1) - i] > 1200)
                        if (CTvaluesOfImage[i, j] > 1200)
                        {
                            result.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                        }
                    }
                }

                int coefStep = (maxScalar - minScalar) / 255;
                int houUpperToPixel = (houBorders[1] + Math.Abs(minScalar) + 1) / coefStep - 1;
                //int houLowerToPixel = (houBorders[0]  + Math.Abs( minScalar ) + 1) / coefStep - 1;
                int houLowerToPixel = 0;
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        int currCT = CTvaluesOfImage[j, (image.Height - 1) - i];
                        int currU = houBorders[1] - intercept;
                        int currL = houBorders[0] - intercept;
                        if (CTvaluesOfImage[i, j] > houBorders[1])
                        {
                            result.SetPixel(i, j, Color.FromArgb(houUpperToPixel, houUpperToPixel, houUpperToPixel));
                        }
                        else if (CTvaluesOfImage[i, j] < houBorders[0])
                        {
                            result.SetPixel(i, j, Color.FromArgb(houLowerToPixel, houLowerToPixel, houLowerToPixel));
                        }
                        else
                        {
                            result.SetPixel(i, j, Color.FromArgb(image.GetPixel(i, j).R, image.GetPixel(i, j).R, image.GetPixel(i, j).R));
                        }
                    }
                }
            }
            else {
                MessageBox.Show("File imageScalars does not exist!");
            }
            return result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (filenameFirstImage != "" && textBox1.Text != "" && textBox2.Text != "")
            {
                pictureBox1.Image = null;
                //clip pixel
                Bitmap newBtm = clipPixels(new Bitmap(filenameFirstImage), new int[] { Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text) });
                newBtm.Save(TempDir + "\\houclip.png", System.Drawing.Imaging.ImageFormat.Png);
                pictureBox1.Image = Image.FromFile(TempDir + "\\houclip.png");

                DialogResult dr = MessageBox.Show("This image is good?", "Image preprocessing", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dr == DialogResult.OK)
                {
                    //paddint image
                    Bitmap newBtmPad = paddingReflect(ResizeBitmap(new Bitmap(TempDir + "\\houclip.png"), 388, 388),
                        92 * 2);
                    newBtmPad.Save(TempDir + "\\ref.png", System.Drawing.Imaging.ImageFormat.Png);
                    pictureBox1.Image = Image.FromFile(TempDir + "\\ref.png");
                    //normalize to txt file
                    Bitmap bmp = new Bitmap(TempDir + "\\ref.png");
                    arrNormalizeImage.Clear();
                    if (File.Exists(TempDir + "\\normalize.txt")) File.Delete(TempDir + "\\normalize.txt");
                    using (StreamWriter StrW = new StreamWriter(TempDir + "\\normalize.txt"))
                    {
                        for (int i = 0; i < bmp.Width; i++)
                        {
                            for (int j = 0; j < bmp.Height; j++)
                            {
                                var pix = bmp.GetPixel(j, i);
                                double NormPixel = (double)(pix.R) / 255;
                                arrNormalizeImage.Add(NormPixel);
                                StrW.Write(Convert.ToString(NormPixel, System.Globalization.CultureInfo.InvariantCulture) + " ");
                            }
                            StrW.Write("\n");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Pick a image and enter a borders");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (filenameFirstImage != "")
            {
                Bitmap newBtm = paddingReflect(ResizeBitmap(new Bitmap(pictureBox1.Image), 388, 388),
                    92 * 2);
                newBtm.Save(TempDir + "\\ref.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                MessageBox.Show("Pick a image");
            }
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "d:\\";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filenameFirstImage = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(filenameFirstImage);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (filenameFirstImage != "")
            {
                Bitmap bmp = new Bitmap(filenameFirstImage);

                Bitmap newBtm = new Bitmap(500, 500);
                arrNormalizeImage.Clear();
                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        var pix = bmp.GetPixel(i, j);

                        arrNormalizeImage.Add((double)(pix.R) / 255);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pick a image");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(TempDir + @"\image.png"))
            {
                filenameFirstImage = TempDir + @"\image.png";
                pictureBox1.Image = Image.FromFile(TempDir + @"\image.png");
            }
            else
                MessageBox.Show("There is no image for load!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (File.Exists(TempDir + @"\image.png"))
            {
                pictureBox1.Image = ResizeBitmap( new Bitmap (TempDir + @"\image.png"),388,388);
                ArrWithPredInt = new int[388, 388];
                if(File.Exists(TempDir + @"\pred.txt"))
                {
                    string[] PredData = File.ReadAllText(TempDir + @"\pred.txt").Split('\n');
                    for (int i = 0; i < 388; i++)
                    {
                        string[] PredDateRow = PredData[i].Split(' ');
                        for (int j = 0; j < PredDateRow.Length; j++)
                        {
                            ArrWithPredInt[i, j] = Convert.ToInt32(PredDateRow[j]);
                            if (PredDateRow[j] != "0" && x_sq == -1) x_sq = Convert.ToInt32(i);
                            if (PredDateRow[j] != "0") { height_sq = Convert.ToInt32(i); }
                        }
                    }

                    //for (int i = 387; i >= 0; i--)
                    //{
                    //    for (int j = 387; j >= 0; j--)
                    //    {
                    //        if (ArrWithPredInt[j, i] != 0 && y_sq == -1) width_sq = Convert.ToInt32(i);
                    //        if (ArrWithPredInt[j, i] != 0) { y_sq = Convert.ToInt32(i); }
                    //    }
                    //}

                    pictureBox1_Paint(sender, new PaintEventArgs(pictureBox1.CreateGraphics(),new Rectangle(0,0,388,388)));
                }
                else
                    MessageBox.Show("File with prediction data not found!");
            }
            else
                MessageBox.Show("There is no image for load!");
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = ArrWithPredInt.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = ArrWithPredInt.GetLength(1) -1 ; j >= 0 ; j--)
                {
                    if(ArrWithPredInt[j,i] == 1)
                        e.Graphics.DrawRectangle(new Pen(Color.Green, 1), i, j, 1, 1);
                }
            }
            //e.Graphics.DrawRectangle(new Pen(Color.Green,3),x_sq,y_sq,width_sq - y_sq,height_sq - x_sq);
        }


        public string checkMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    byte[] bytes = md5.ComputeHash(stream);
                    StringBuilder result = new StringBuilder(bytes.Length * 2);

                    for (int i = 0; i < bytes.Length; i++)
                        result.Append(bytes[i].ToString("X2"));

                    return result.ToString();
                }
            }
        }

        private void SendFileButton_Click(object sender, EventArgs e)
        {
            System.IO.Stream response = Upload(ApiURL + "application/set-task-in-db",
                CurrentHash,
                File.Open(TempDir + @"\normalize.txt", FileMode.Open),
                "task_hash",
                "file_1",
                "normalize.txt");
            if (response != null)
            {
                using (var reader = new StreamReader(response, Encoding.UTF8))
                {
                    string value = reader.ReadToEnd();
                    Newtonsoft.Json.Linq.JToken token = Newtonsoft.Json.Linq.JObject.Parse(value);
                    MessageBox.Show("Message from server: " + (string)token.SelectToken("message"));
                }
            }
            else {
                MessageBox.Show("File do not send!");
            }
        }
        private System.IO.Stream Upload(string actionUrl, string paramString, Stream paramFileStream, string formParam, string formFileParam, string formFileNameParam)
        {
            HttpContent stringContent = new StringContent(paramString);
            HttpContent fileStreamContent = new StreamContent(paramFileStream);
            //HttpContent bytesContent = new ByteArrayContent(paramFileBytes);
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(stringContent, formParam);
                formData.Add(fileStreamContent, formFileParam, formFileNameParam);
                //formData.Add(bytesContent, "file2", "file2");
                var response = client.PostAsync(actionUrl, formData).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return response.Content.ReadAsStreamAsync().Result;
            }
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                List<TasksJson> json = null;
                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        var uri = ApiURL + "application/gettasks";
                        var response = await httpClient.GetAsync(uri);
                        string JsonFromResponse = await response.Content.ReadAsStringAsync();


                        json = JsonConvert.DeserializeObject<List<TasksJson>>(JsonFromResponse);
                        dataGridView1.DataSource = json;
                        int widthColumn = dataGridView1.Width / dataGridView1.ColumnCount - 5;
                        foreach (DataGridViewColumn item in dataGridView1.Columns)
                        {
                            item.Width = widthColumn;
                        }
                    }
                    catch (Exception ec)
                    {
                        MessageBox.Show(ec.Message, "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 1)
            {
                string pathToImage = IniObjMain.IniReadValue("Info", "temp_folder") + "\\" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + @"\image.png";
                if (File.Exists(pathToImage))
                {
                    TaskPagePicturePictureBox.Image = Image.FromFile(pathToImage);
                }
                else {
                    TaskPagePicturePictureBox.Image = TaskPagePicturePictureBox.InitialImage; 
                }
            }
        }
    }
}