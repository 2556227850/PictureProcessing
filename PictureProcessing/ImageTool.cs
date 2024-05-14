using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Formats.Png;

namespace PictureProcessing
{
    internal class ImageTool
    {
        // 输入和输出文件夹路径
        string inputFolder = @"C:\Users\zhangzhenmin\Desktop\图片";
        string outputFolder = @"C:\Users\zhangzhenmin\Desktop\图片\output";
        // 要处理的文件扩展名
        string[] extensions = { "*.jpg", "*.jpeg", "*.png", "*.bmp", "*.webp" };
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

                // 遍历每个图片文件并进行处理
                foreach (string imageFile in imageFiles)
                {
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
        public void Test()
        {
            // 输入和输出文件夹路径
            string inputFolder = @"C:\Users\zhangzhenmin\Desktop\图片";
            string outputFolder = @"C:\Users\zhangzhenmin\Desktop\图片\output";

            // 目标尺寸
            int targetWidth = 800;
            int targetHeight = 600;

            try
            {
                // 确保输出文件夹存在
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // 获取输入文件夹下所有图片文件
                string[] imageFiles = Directory.GetFiles(inputFolder, "*.png");

                // 遍历每个图片文件并进行处理
                foreach (string imageFile in imageFiles)
                {
                    // 使用 Image.Load 方法加载图片
                    using (Image<Rgba32> image = Image.Load<Rgba32>(imageFile))
                    {
                        // 调用 Resize 方法进行图片缩放，保持原始长宽比
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new Size(targetWidth, targetHeight),
                            Mode = ResizeMode.Stretch // 允许变形
                        }));

                        // 构造输出文件路径
                        string outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(imageFile) + ".webp");

                        // 保存处理后的图片到输出文件夹，指定  格式
                        image.Save(outputFile, new PngEncoder());
                    }
                }

                Console.WriteLine("图片处理完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理图片时出错：{ex.Message}");
            }
        }
    }
}
