using OpenCvSharp;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Security.Cryptography;
using System.Net.Http;
using System.IO;

namespace ScannMain
{
    public partial class Main : Form
    {
        private static int imgShowIndex = 1;
        private static string showIndex = "";
        private static string showSuqIndex = "";

        public Main()
        {
            InitializeComponent();
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(folderTextBox.Text))
            {
                path.SelectedPath = folderTextBox.Text;
            }
            path.ShowDialog();
            if (!string.IsNullOrEmpty(path.SelectedPath))
            {
                if (!path.SelectedPath.EndsWith('\\'))
                    this.folderTextBox.Text = path.SelectedPath + "\\";
                else
                    this.folderTextBox.Text = path.SelectedPath;
            }
        }

        private void fileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = ttTextBox.Text != null && fileTextBox.Text.IndexOf('.') > 0 ? fileTextBox.Text.Substring(0, ttTextBox.Text.LastIndexOf('\\')) : "";
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择生成文件";
            dialog.Filter = "图片文件(*.png;*.tif;*.jpg)|*.png;*.jpg;*.tif";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileTextBox.Text = dialog.FileName;
                fileTextBox.Refresh();
            }
        }

        private void scannButton_Click(object sender, EventArgs e)
        {
            imgShowIndex = 1;
            showIndex = stepTextBox.Text;//显示步骤下标
            showSuqIndex = eqTextBox.Text;//显示矩形下标
            //if (ScannId <= 0)
            //{
            //    MessageBox.Show("请选择扫描件对应数据");
            //    return;
            //}
            scannFiles = new List<ScannFile>();
            string msg = "";
            int[,] resultArray;
            if (!string.IsNullOrEmpty(fileTextBox.Text))
            {
                try
                {
                    if (!File.Exists(fileTextBox.Text))
                    {
                        MessageBox.Show("所选文件路径不存在");
                        return;
                    }
                    List<Point[]> selectOption;
                    MatchAnswer(fileTextBox.Text, int.Parse(rowTextBox.Text), int.Parse(celTextBox.Text), int.Parse(yzMinTextBox.Text), int.Parse(yzMaxTextBox.Text), int.Parse(heightTextBox.Text), int.Parse(widthTextBox.Text), int.Parse(ttTextBox.Text), out resultArray, out selectOption, out int fzfgCount);
                    scannFiles.Add(new ScannFile()
                    {
                        FilePath = fileTextBox.Text,
                        ResultArray = resultArray,
                        SelectOption = selectOption
                    });
                    msg += "1:" + Path.GetFileName(fileTextBox.Text) + ":" + (showCountCheckBox.Checked ? selectOption.Count.ToString() + "|" + fzfgCount : Newtonsoft.Json.JsonConvert.SerializeObject(resultArray)) + "\r\n\r\n";
                }
                catch (Exception ex)
                {
                    msg += ";" + ex.ToString();
                }
            }
            else if (!string.IsNullOrEmpty(folderTextBox.Text))
            {
                if (!Directory.Exists(folderTextBox.Text))
                {
                    MessageBox.Show("所选文件夹路径不存在");
                    return;
                }
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(folderTextBox.Text);
                System.IO.FileInfo[] allFiles = GetAllFileInfo(dir);
                List<(int num, System.IO.FileInfo file)> listFiles = new List<(int num, FileInfo file)>();
                int defaultIndex = -1;
                foreach (var item in allFiles)
                {
                    string[] names = item.Name.Split('_');
                    if (names.Length >= 2 && names.Last().Split('.').Length == 2 && int.TryParse(names.Last().Split('.')[0], out int nameNum))
                    {
                        listFiles.Add((nameNum, item));
                    }
                    else
                    {
                        listFiles.Add((defaultIndex, item));
                        defaultIndex--;
                    }
                }
                List<(int num, System.IO.FileInfo file)> tempFiles = listFiles.Where(a => a.num >= 0).OrderBy(a => a.num).ToList();
                tempFiles.AddRange(listFiles.Where(a => a.num < 0).OrderByDescending(a => a.num));
                int scannIndex = 0;
                foreach (System.IO.FileInfo file in tempFiles.Select(a => a.file))
                {
                    //Common.ImageHelper.ConvertAndZoomImage(file.FullName, @"C:\Users\ZhuoShang-PC\Desktop\img-Y29160442\test.png",2000,2000);
                    try
                    {
                        if (!string.IsNullOrEmpty(fileStartTextBox.Text) && !file.Name.StartsWith(fileStartTextBox.Text))
                            continue;
                        List<Point[]> selectOption;
                        MatchAnswer(file.FullName, int.Parse(rowTextBox.Text), int.Parse(celTextBox.Text), int.Parse(yzMinTextBox.Text), int.Parse(yzMaxTextBox.Text), int.Parse(heightTextBox.Text), int.Parse(widthTextBox.Text), int.Parse(ttTextBox.Text), out resultArray, out selectOption, out int fzfgCount);
                        scannIndex++;
                        scannButton.Text = "扫描(" + scannIndex + "/" + allFiles.Length + ")";
                        scannButton.Refresh();
                        msg += scannIndex + ":" + file.Name + ":" + (showCountCheckBox.Checked ? selectOption.Count.ToString() + "|" + fzfgCount : Newtonsoft.Json.JsonConvert.SerializeObject(resultArray)) + "\r\n\r\n";
                        scannFiles.Add(new ScannFile()
                        {
                            FilePath = file.FullName,
                            ResultArray = resultArray,
                            SelectOption = selectOption
                        });
                    }
                    catch (Exception ex)
                    {
                        msg += ";" + ex.ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择文件夹或文件");
                return;
            }
            resTextBox.Text = msg;
            resTextBox.Refresh();
            Cv2.WaitKey();
        }

        public List<ScannFile> scannFiles = new List<ScannFile>();

        public class ScannFile
        {
            public string FilePath { get; set; }
            public int[,] ResultArray { get; set; }
            public List<Point[]> SelectOption { get; set; }
        }

        /// <summary>
        /// 获取文件夹下所有的文件
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static System.IO.FileInfo[] GetAllFileInfo(System.IO.DirectoryInfo dir)
        {
            return dir.GetFiles(".", System.IO.SearchOption.AllDirectories);
        }

        /// <summary>
        /// 匹配答案
        /// </summary>
        /// <param name="path">需检测的图片路径</param>
        /// <param name="row">答案的总行数</param>
        /// <param name="column">答案的总列数</param>
        public static Mat MatchAnswer(string path, int row, int column, int par1, int par2, int par3, int par4, int judgeSize, out int[,] resultArray, out List<Point[]> selectOption, out int fzfgCount)
        {
            resultArray = new int[,] { };
            selectOption = new List<Point[]>();

            Mat answerSheet = Cv2.ImRead(path);
            Point[] result_contour = GetBoundaryOfPic(answerSheet);
            Mat birdMat = WarpPerspective(answerSheet, result_contour);
            ShowImg(birdMat, "鸟瞰");

            //OTSU阈值分割
            Mat target = new Mat();
            int selectOptionCount = 0;
            List<Point[]> selected_contour = new List<Point[]>();
            fzfgCount = 0;
            while (selectOptionCount <= 1)
            {
                if (fzfgCount >= 100)
                    return null;
                if (fzfgCount > 0)
                    par1--;
                fzfgCount++;
                Cv2.Threshold(birdMat, target, par1, par2, ThresholdTypes.BinaryInv);//修改thresh或maxval可以调整轮廓取值范围(调的不好会直接取外面的大轮廓)
                ShowImg(target, "阈值分割");
                selected_contour = SelectedContour(target, par3, par4);
                selectOptionCount = selected_contour.Count();
            }
            selectOption = selected_contour;
            //3.验证结果
            Mat answerSheet_con = target.Clone();
            Cv2.CvtColor(answerSheet_con, answerSheet_con, ColorConversionCodes.GRAY2BGR);
            Cv2.DrawContours(answerSheet_con, selected_contour, -1, new Scalar(0, 0, 255), 2);

            ShowImg(answerSheet_con, "选项");
            List<Point>[,] classed_contours = ClassedOfContours(selected_contour, row, column);

            //5.绘制并验证
            List<Scalar> color = new List<Scalar>();
            color.Add(new Scalar(0, 0, 255));
            color.Add(new Scalar(255, 0, 255));
            color.Add(new Scalar(0, 255, 255));
            color.Add(new Scalar(255, 0, 0));
            color.Add(new Scalar(0, 255, 0));
            Mat groupMap = target.Clone();
            Cv2.CvtColor(groupMap, groupMap, ColorConversionCodes.GRAY2BGR);
            for (int i = 0; i < row; i++)
            {
                List<List<Point>> tempGroupPoints = new List<List<Point>>();
                for (int j = 0; j < column; j++)
                {
                    if (classed_contours[i, j].Count > 0)
                        tempGroupPoints.Add(classed_contours[i, j]);
                }
                if (tempGroupPoints.Count > 0)
                    Cv2.DrawContours(groupMap, tempGroupPoints, -1, color[(i >= 5 ? i % 5 : i)], 2);
            }
            ShowImg(groupMap, "分组");


            //检测答题者的选项
            resultArray = GetResultArray(target, classed_contours, row, column, judgeSize);
            Mat resultMat = new Mat();
            Cv2.CvtColor(target, resultMat, ColorConversionCodes.GRAY2BGR);

            List<List<Point>> tempPoints = new List<List<Point>>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (resultArray[i, j] == 1)
                    {
                        tempPoints.Add(classed_contours[i, j]);
                    }
                }
            }
            Cv2.DrawContours(resultMat, tempPoints, -1, new Scalar(255, 0, 0), 2);

            ShowImg(resultMat, "结果");
            return resultMat;
        }

        /// <summary>
        /// 寻找边界
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static Point[] GetBoundaryOfPic(Mat mat, string size = "0,90,3,true")
        {
            //灰度转化
            Mat gray = new Mat();
            Cv2.CvtColor(mat, gray, ColorConversionCodes.RGB2GRAY);
            //进行高斯滤波
            Mat blurred = new Mat();
            Cv2.GaussianBlur(gray, blurred, new Size(3, 3), 0);
            //进行canny边缘检测
            Mat canny = new Mat();
            //Cv2.Canny(blurred, canny, 0, 180);
            string[] str = size.Split(",");
            Cv2.Canny(blurred, canny, int.Parse(str[0]), int.Parse(str[1]), int.Parse(str[2]), bool.Parse(str[3]));
            ShowImg(canny, "canny");

            //寻找矩形边界
            Point[][] contours;
            HierarchyIndex[] hierarchly;
            Cv2.FindContours(canny, out contours, out hierarchly, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            //Cv2.FindContours(canny, out contours, out hierarchly, RetrievalModes.External, ContourApproximationModes.ApproxNone);

            //Cv2.FindContours(canny, out contours, out hierarchly, RetrievalModes.External, ContourApproximationModes.ApproxTC89KCOS);

            //Cv2.FindContours(canny, out contours, out hierarchly, RetrievalModes.External, ContourApproximationModes.ApproxTC89L1);
            Point[] result_contour;
            if (contours.Length == 1)
            {
                result_contour = contours[0];
            }
            else
            {
                double max = -1;
                int index = -1;
                for (int i = 0; i < contours.Length; i++)
                {
                    if (contours[i].Length < 4)
                    {
                        continue;
                    }
                    double tem = Cv2.ArcLength(contours[i], true);
                    bool pass = IsGreatArc(contours[i]);
                    if (tem > max && pass)
                    {
                        max = tem;
                        index = i;
                    }
                    if (!string.IsNullOrEmpty(showSuqIndex))
                    {
                        Mat birdMat = WarpPerspective(mat, contours[i]);
                        ShowImg2(birdMat, "鸟瞰" + i + "|" + tem.ToString("f2") + (pass ? "|通过" : ""));
                    }
                }
                result_contour = contours[index];

            }
            return result_contour;
        }

        class TempXY
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public static bool IsGreatArc(Point[] contours)
        {
            TempXY minXmaxY = new TempXY() { X = -1 };
            TempXY minXminY = new TempXY() { X = -1 };
            TempXY maxXmaxY = new TempXY() { X = -1 };
            TempXY maxXminY = new TempXY() { X = -1 };
            int minX = -1, minY = -1, maxX = -1, maxY = -1;
            foreach (var item in contours)
            {
                if (item.X < minX || minX == -1)
                    minX = item.X;
                if (item.X > maxX || maxX == -1)
                    maxX = item.X;
                if (item.Y < minY || minY == -1)
                    minY = item.Y;
                if (item.Y > maxY || maxY == -1)
                    maxY = item.Y;
            }
            foreach (var item in contours)
            {
                if (Math.Abs(item.X - minX) + Math.Abs(item.Y - maxY) < Math.Abs(minXmaxY.X - minX) + Math.Abs(minXmaxY.Y - maxY) || minXmaxY.X == -1)
                {
                    minXmaxY.X = item.X;
                    minXmaxY.Y = item.Y;
                }
                if (Math.Abs(item.X - minX) + Math.Abs(item.Y - minY) < Math.Abs(minXminY.X - minX) + Math.Abs(minXminY.Y - minY) || minXminY.X == -1)
                {
                    minXminY.X = item.X;
                    minXminY.Y = item.Y;
                }
                if (Math.Abs(item.X - maxX) + Math.Abs(item.Y - maxY) < Math.Abs(maxXmaxY.X - maxX) + Math.Abs(maxXmaxY.Y - maxY) || maxXmaxY.X == -1)
                {
                    maxXmaxY.X = item.X;
                    maxXmaxY.Y = item.Y;
                }
                if (Math.Abs(item.X - maxX) + Math.Abs(item.Y - minY) < Math.Abs(maxXminY.X - maxX) + Math.Abs(maxXminY.Y - minY) || maxXminY.X == -1)
                {
                    maxXminY.X = item.X;
                    maxXminY.Y = item.Y;
                }
            }
            if (Math.Abs(minXminY.X - minXmaxY.X) < 30 && Math.Abs(maxXminY.X - maxXmaxY.X) < 30 && minXmaxY.X > 5 && minXminY.Y > 5 && maxXminY.X - minXminY.X > 50 && minXmaxY.Y - minXminY.Y > 50)
            {
                if (Math.Abs(minXminY.Y - maxXminY.Y) < 30 && Math.Abs(minXmaxY.Y - maxXmaxY.Y) < 30)
                {
                    return true;
                }

            }

            return false;
        }

        /// <summary>
        /// 对图像进行矫正(转为鸟瞰图，删除多余边界)
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="result_contour"></param>
        /// <returns></returns>
        public static Mat WarpPerspective(Mat mat, Point[] result_contour)
        {
            //使用DP算法拟合答题卡的几何轮廓,保存点集pts并顺时针排序
            double result_length = Cv2.ArcLength(result_contour, true);
            Point[] pts = Cv2.ApproxPolyDP(result_contour, result_length * 0.02, true);
            int width = 0;
            int height = 0;
            if (pts.Length == 4)
            {
                if (pts[1].X < pts[3].X)
                {
                    //说明当前为逆时针存储，改为顺时针存储（交换第2、4点）
                    Point p = new Point();
                    p = pts[1];
                    pts[1] = pts[3];
                    pts[3] = p;
                }
                if (Math.Abs(pts[0].X - pts[3].X) > 100)
                {
                    Point temp = pts[pts.Length - 1];
                    for (int i = pts.Length - 1; i >= 0; i--)
                    {
                        if (i == 0)
                            pts[i] = temp;
                        else
                            pts[i] = pts[i - 1];
                    }
                }
                //进行透视变换
                //1.确定变化尺寸的宽度

                float width1 = (pts[0].X - pts[1].X) * (pts[0].X - pts[1].X) + (pts[0].Y - pts[1].Y) * (pts[0].Y - pts[1].Y);
                float width2 = (pts[2].X - pts[3].X) * (pts[2].X - pts[3].X) + (pts[2].Y - pts[3].Y) * (pts[2].Y - pts[3].Y);
                width = width1 > width2 ? (int)Math.Sqrt(width1) : (int)Math.Sqrt(width2);
                //2.确定变化尺寸的高度

                float height1 = (pts[0].X - pts[3].X) * (pts[0].X - pts[3].X) + (pts[0].Y - pts[3].Y) * (pts[0].Y - pts[3].Y);
                float height2 = (pts[2].X - pts[1].X) * (pts[2].X - pts[1].X) + (pts[2].Y - pts[1].Y) * (pts[2].Y - pts[1].Y);
                height = height1 > height2 ? (int)Math.Sqrt(height1) : (int)Math.Sqrt(height2);
            }


            Point2f[] pts_src = Array.ConvertAll(pts.ToArray(), new Converter<Point, Point2f>(PointToPointF));
            Point2f[] pts_target = new Point2f[] { new Point2f(0, 0), new Point2f(width - 1, 0), new Point2f(width - 1, height - 1), new Point2f(0, height - 1) };

            //4.计算透视变换矩阵
            //4.1类型转化
            Mat data = Cv2.GetPerspectiveTransform(pts_src, pts_target);
            //5.进行透视变换
            Mat birdMat = new Mat();
            //进行透视操作
            Mat mat_Perspective = new Mat();
            Mat src_gray = new Mat();
            Cv2.CvtColor(mat, src_gray, ColorConversionCodes.BGR2GRAY);
            Cv2.WarpPerspective(src_gray, birdMat, data, new Size(width, height));
            return birdMat;
        }

        /// <summary>
        /// 获取所有选项位置
        /// </summary>
        /// <param name="target">矫正和OTSU阈值分割后的mat</param>
        /// <returns></returns>
        public static List<Point[]> SelectedContour(Mat target, int height, int width)
        {
            //轮廓筛选
            //1.改善轮廓
            Mat element = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(1, 1));
            Cv2.Dilate(target, target, element);
            ShowImg(target);
            //2.筛选轮廓
            Point[][] target_contour;
            List<Point[]> selected_contour = new List<Point[]>();
            HierarchyIndex[] hierarchly2;
            Cv2.FindContours(target, out target_contour, out hierarchly2, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            foreach (var m in target_contour)
            {
                Rect rect = Cv2.BoundingRect(m);
                double k = (double)rect.Height / rect.Width;
                if (rect.Height > height && rect.Width > width && rect.Width < 100)
                {
                    selected_contour.Add(m);
                }
            }
            return selected_contour;
        }

        /// <summary>
        /// 把选项分为有序的行列数据
        /// </summary>
        /// <param name="selected_contour">位置</param>
        /// <param name="countOfRow">选项行数</param>
        /// <param name="countOfColumn">列数</param>
        /// <returns></returns>
        public static List<Point>[,] ClassedOfContours(List<Point[]> selected_contour, int countOfRow, int countOfColumn)
        {
            //依据圆心的位置来确认答题卡轮廓的位置
            //1.计算所有外接圆基本数据
            float[] radius = new float[selected_contour.Count];
            Point2f[] center = new Point2f[selected_contour.Count];
            for (int i = 0; i < selected_contour.Count; i++)
            {
                float radiusItem;
                Point2f centerItem;
                Cv2.MinEnclosingCircle(selected_contour[i], out centerItem, out radiusItem);//最小外接圆
                center[i] = centerItem;
                radius[i] = radiusItem;
            }
            //2.计算x轴、y轴分割间隔
            float x_min = 999, y_min = 999;
            float x_max = -1, y_max = -1;
            float x_interval = 0, y_interval = 0;//相邻圆心的间距
            foreach (Point2f pf in center)
            {
                //获取所有圆心中的坐标最值
                if (pf.X < x_min) x_min = pf.X;
                if (pf.X > x_max) x_max = pf.X;

                if (pf.Y < y_min) y_min = pf.Y;
                if (pf.Y > y_max) y_max = pf.Y;
            }
            x_interval = (x_max - x_min) / (countOfColumn - 1);//答题卡每列x个，即x-1个间隔
            y_interval = (y_max - y_min) / (countOfRow - 1);//答题卡每行y个圆，即y-1个间隔

            //4.分类
            List<Point>[,] classed_contours = new List<Point>[countOfRow, countOfColumn];
            //初始化VectorOfVectorOfPoint二维数组
            for (int i = 0; i < countOfRow; i++)
            {
                for (int j = 0; j < countOfColumn; j++)
                {
                    classed_contours[i, j] = new List<Point>();
                }
            }
            if (x_interval == 0 || y_interval == 0)
                return classed_contours;
            for (int i = 0; i < selected_contour.Count; i++)
            {
                Point2f pf = center[i];
                int index_r = (int)Math.Round((pf.Y - y_min) / y_interval);//行号
                int index_c = (int)Math.Round((pf.X - x_min) / x_interval);//列号
                Point[] temp = selected_contour[i];
                classed_contours[index_r, index_c].AddRange(temp.ToList());
            }

            return classed_contours;
        }

        /// <summary>
        /// 检测答题者的选项，获取涂选的结果数组
        /// </summary>
        /// <param name="mat_threshold">经阈值处理后的图像</param>
        /// <param name="classed_contours">经排序分类后的轮廓数组</param>
        /// <param name="countOfRow">一行中轮廓的个数</param>
        /// <param name="countOfColumn">一列中轮廓的个数</param>
        /// <returns></returns>
        public static int[,] GetResultArray(Mat mat_threshold, List<Point>[,] classed_contours, int countOfRow, int countOfColumn, int judgeSize)
        {
            int[,] result_count = new int[countOfRow, countOfColumn];//结果数组
            //统计所有答题圆圈外接矩形内非零像素个数
            Rect[,] re_rect = new Rect[countOfRow, countOfColumn];//外接矩形数组
            int[,] count_roi = new int[countOfRow, countOfColumn];//外接矩形内非零像素个数
            int min_count = 999;//非零像素个数最大值，作为已涂选的参照
            int max_count = -1;//非零像素个数最小值，作为未涂选的参照
            for (int i = 0; i < countOfRow; i++)
            {
                for (int j = 0; j < countOfColumn; j++)
                {
                    List<Point> countour = classed_contours[i, j];
                    re_rect[i, j] = Cv2.BoundingRect(countour);
                    Mat temp = new Mat(mat_threshold, re_rect[i, j]);//提取ROI矩形区域
                    int count = Cv2.CountNonZero(temp);//计算图像内非零像素个数
                    count_roi[i, j] = count;

                    if (count > max_count) max_count = count;
                    if (count < min_count) min_count = count;
                }
            }

            if (judgeSize > 0)
            {
                max_count = judgeSize * 2;
            }


            //比对涂选的答案，以涂满圆圈一半以上为标准
            for (int i = 0; i < countOfRow; i++)
            {
                for (int j = 0; j < countOfColumn; j++)
                {
                    if (count_roi[i, j] > max_count / 2)
                    {
                        result_count[i, j] = 1;
                    }
                }
            }

            return result_count;
        }

        public static void ShowImg(Mat mat, string name = "")
        {
            bool show = false;
            foreach (var item in showIndex)
            {
                int i = int.Parse(item.ToString());
                int i2 = (int)item;
                if (imgShowIndex == int.Parse(item.ToString()))
                {
                    show = true;
                }
            }

            if (!show)
            {
                imgShowIndex++;
                return;
            }
            Cv2.ImShow(!string.IsNullOrEmpty(name) ? name : imgShowIndex.ToString(), mat);
            imgShowIndex++;
        }

        private static int suqShowIndex = 1;
        public static void ShowImg2(Mat mat, string name = "")
        {
            bool show = false;
            if (showSuqIndex == "0")
            {
                Cv2.ImShow(name, mat);
                return;
            }
            else if (!string.IsNullOrEmpty(showSuqIndex))
            {
                foreach (var item in showSuqIndex)
                {
                    int i = int.Parse(item.ToString());
                    int i2 = (int)item;
                    if (suqShowIndex == int.Parse(item.ToString()))
                    {
                        show = true;
                    }
                }

                if (!show)
                {
                    suqShowIndex++;
                    return;
                }
                Cv2.ImShow(!string.IsNullOrEmpty(name) ? name : suqShowIndex.ToString(), mat);
                suqShowIndex++;
            }

        }

        /// <summary>
        /// Point转换为PointF类型
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Point2f PointToPointF(Point p)
        {
            return new Point2f(p.X, p.Y);
        }

        public static Mat MyFindContours(Mat srcImage)
        {
            //转化为灰度图
            Mat src_gray = new Mat();
            Cv2.CvtColor(srcImage, src_gray, ColorConversionCodes.RGB2GRAY);

            //滤波
            Cv2.Blur(src_gray, src_gray, new Size(3, 3));

            //Canny边缘检测
            Mat canny_Image = new Mat();
            Cv2.Canny(src_gray, canny_Image, 100, 200);
            //获得轮廓
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchly;
            Cv2.FindContours(canny_Image, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, new Point(0, 0));

            OpenCvSharp.Point[] resContours;
            if (contours.Length == 1)
            {
                resContours = contours[0];
            }
            else
            {
                double maxLength = -1;
                int index = -1;
                for (int i = 0; i < contours.Length; i++)
                {
                    double length = Cv2.ArcLength(contours[i], true);
                    if (Cv2.ArcLength(contours[i], true) > maxLength)
                    {
                        maxLength = length;
                        index = i;
                    }
                }
                resContours = contours[index];
            }

            double result_length = Cv2.ArcLength(resContours, true);
            OpenCvSharp.Point[] resPoint = Cv2.ApproxPolyDP(resContours, result_length * 0.02, true); //几何逼近，获取矩形4个顶点坐标

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(resPoint);
            //将结果画出并返回结果
            Mat dst_Image = Mat.Zeros(canny_Image.Size(), srcImage.Type());
            Random rnd = new Random();
            //for (int i = 0; i < contours.Length; i++)
            for (int i = 0; i < contours.Length; i++)
            {
                Scalar color = new Scalar(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                Cv2.DrawContours(dst_Image, contours, i, color, 2, LineTypes.Link8, hierarchly);
                Cv2.ImShow("contours" + i, dst_Image);
            }
            return dst_Image;
        }
    }
}
