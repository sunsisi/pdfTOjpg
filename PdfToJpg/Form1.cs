using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using O2S.Components.PDFRender4NET;

namespace PdfToJpg
{
    public partial class Form1 : Form
    {
        String address_pdf = "";
        public Form1()
        {
            InitializeComponent();
        }
        public enum Definition
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10
        }

        /// <summary>
        /// 将PDF文档转换为图片的方法
        /// </summary>
        /// <param name="pdfInputPath">PDF文件路径</param>
        /// <param name="imageOutputPath">图片输出路径</param>
        /// <param name="imageName">生成图片的名字</param>
        /// <param name="startPageNum">从PDF文档的第几页开始转换</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换</param>
        /// <param name="imageFormat">设置所需图片格式</param>
        /// <param name="definition">设置图片的清晰度，数字越大越清晰</param>
        public static void ConvertPDF2Image(string pdfInputPath, string imageOutputPath,
            string imageName, int startPageNum, int endPageNum, ImageFormat imageFormat, Definition definition)
        {
            PDFFile pdfFile = PDFFile.Open(pdfInputPath);

            if (!Directory.Exists(imageOutputPath))
            {
                Directory.CreateDirectory(imageOutputPath);
            }

            // 开始的页
            if (startPageNum <= 0)
            {
                startPageNum = 1;
            }

            if (endPageNum > pdfFile.PageCount)
            {
                endPageNum = pdfFile.PageCount;
            }

            if (startPageNum > endPageNum)
            {
                int tempPageNum = startPageNum;
                startPageNum = endPageNum;
                endPageNum = startPageNum;
            }

            // start to convert each page
            for (int i = startPageNum; i <= endPageNum; i++)
            {
                Bitmap pageImage = pdfFile.GetPageImage(i - 1, 56 * (int)definition);
                pageImage.Save(imageOutputPath + imageName + i.ToString() + "." + imageFormat.ToString(), imageFormat);
                pageImage.Dispose();
            } 
            pdfFile.Dispose();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 打开文件
            
            OpenFileDialog addFileDialog = new OpenFileDialog();
            addFileDialog.Filter = "pdf|*.pdf";

            //  
            if (addFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (addFileDialog.FileName != null)
                {
                    //得到地址信息
                    address_pdf = addFileDialog.FileName;
                }
                else
                {
                    ;
                }
            }
            else
            {
                ;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\picture\\";
            ConvertPDF2Image(address_pdf, str, "A", 1, 1000, ImageFormat.Jpeg, Definition.Four);
            MessageBox.Show("PDF转JPEG完成！");
        }
    }
}
