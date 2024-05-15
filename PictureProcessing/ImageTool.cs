using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Bmp;
using System.Collections.Generic;

namespace PictureProcessing
{
    internal class ImageTool : Tool
    {
        List<string> image_adjuster = ["裁剪", "填充", "盒子填充", "最大", "最小", "拉伸", "手动",];
        List<string> image_position = ["中心点", "顶部", "底部", "左侧", "右侧", "左上角", "右上角", "右下角", "左下角",];

        // 输入和输出文件夹路径
        string inputFolder = @".\";
        string outputFolder = @".\output";
        // 要处理的文件扩展名
        string[] extensions = { "*.jpg", "*.jpeg", "*.png", "*.webp" };
        protected void UpdateImageType(string key)
        {
            try
            {
                // 确保输出文件夹存在
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // 获取输入文件夹下所有图片文件
                List<string> imageFiles = new List<string>();
                foreach (string extension in extensions)
                {
                    imageFiles.AddRange(Directory.GetFiles(inputFolder, extension));
                }
                Console.WriteLine($"检测到可处理图片数量：{imageFiles.Count}");


                // 遍历每个图片文件并进行处理
                foreach (string imageFile in imageFiles)
                {
                    Console.WriteLine($"正在处理图片：{Path.GetFileNameWithoutExtension(imageFile) + Path.GetExtension(imageFile)}");

                    // 使用 Image.Load 方法加载图片
                    using (Image<Rgba32> image = Image.Load<Rgba32>(imageFile))
                    {

                        switch (key)
                        {
                            case "1":
                                image.Save(Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(imageFile) + ".jpg"), new JpegEncoder());
                                break;
                            case "2":
                                image.Save(Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(imageFile) + ".png"), new PngEncoder());
                                break;
                            case "3":
                                image.Save(Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(imageFile) + ".webp"), new WebpEncoder());
                                break;
                            default:
                                throw new NotSupportedException($"不支持的文件格式: {key}");
                        }
                    }
                }

                Console.WriteLine("图片处理完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理图片时出错：{ex.Message}");
            }
        }
        protected void UpdateImageSize() {
            // 目标尺寸
            Console.Write("请输入宽度:");
            int targetWidth = InputIntValue();

            Console.Write("请输入高度:");
            int targetHeight = InputIntValue();

            Console.WriteLine("请选择图像显示类型");
            string image_adjuster_key = SelectMenusNumber(image_adjuster);

            try
                {
                // 确保输出文件夹存在
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // 获取输入文件夹下所有图片文件
                List<string> imageFiles = new List<string>();
                foreach (string extension in extensions)
                {
                    imageFiles.AddRange(Directory.GetFiles(inputFolder, extension));
                }
                Console.WriteLine($"检测到可处理图片数量：{imageFiles.Count}");

                if (image_adjuster_key == "7") {
                    string image_position_key = SelectMenusNumber(image_position);

                    // 遍历每个图片文件并进行处理
                    foreach (string imageFile in imageFiles)
                    {
                    Console.WriteLine($"正在处理图片：{Path.GetFileNameWithoutExtension(imageFile) + Path.GetExtension(imageFile)}");
                        // 使用 Image.Load 方法加载图片
                        using (Image<Rgba32> image = Image.Load<Rgba32>(imageFile))
                            {
                                // 调用 Resize 方法进行图片缩放，允许变形
                                image.Mutate(x => x.Resize(new ResizeOptions
                                {

                                    Size = new Size(targetWidth, targetHeight),
                                    Mode = GetResizeMode(image_adjuster_key),
                                    Position = GetAnchorPositionMode(image_position_key), // 手动指定目标位置有BUG

                                }));

                                // 获取文件扩展名并选择合适的编码器
                                string extension = Path.GetExtension(imageFile).ToLower();
                                string outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(imageFile) + extension);

                                switch (extension)
                                {
                                    case ".jpg":
                                    case ".jpeg":
                                        image.Save(outputFile, new JpegEncoder());
                                        break;
                                    case ".png":
                                        image.Save(outputFile, new PngEncoder());
                                        break;
                                    case ".webp":
                                        image.Save(outputFile, new WebpEncoder());
                                        break;
                                    default:
                                        throw new NotSupportedException($"不支持的文件格式: {extension}");
                                }
                            }

                    }
                }
                else
                {
                    // 遍历每个图片文件并进行处理
                    foreach (string imageFile in imageFiles)
                    {
                    Console.WriteLine($"正在处理图片：{Path.GetFileNameWithoutExtension(imageFile) + Path.GetExtension(imageFile)}");
                        // 使用 Image.Load 方法加载图片
                        using (Image<Rgba32> image = Image.Load<Rgba32>(imageFile))
                        {
                            // 调用 Resize 方法进行图片缩放，允许变形
                            image.Mutate(x => x.Resize(new ResizeOptions
                            {

                                Size = new Size(targetWidth, targetHeight),
                                Mode = GetResizeMode(image_adjuster_key),

                            }));

                            // 获取文件扩展名并选择合适的编码器
                            string extension = Path.GetExtension(imageFile).ToLower();
                            string outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(imageFile) + extension);

                            switch (extension)
                            {
                                case ".jpg":
                                case ".jpeg":
                                    image.Save(outputFile, new JpegEncoder());
                                    break;
                                case ".png":
                                    image.Save(outputFile, new PngEncoder());
                                    break;
                                case ".webp":
                                    image.Save(outputFile, new WebpEncoder());
                                    break;
                                default:
                                    throw new NotSupportedException($"不支持的文件格式: {extension}");
                            }
                        }

                    }
                }
                Console.WriteLine("图片处理完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理图片时出错：{ex.Message}");
            }
        }

        public void Test()
        {


        }
    }
}
