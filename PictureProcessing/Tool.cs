using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureProcessing
{
    internal class Tool
    {
        internal string SelectMenusNumber(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{list[i]}");
            }

            Console.Write("请输入功能序号:");
            string? key = Console.ReadLine();
            if (int.TryParse(key, out int number))
            {
                if (number > 0 && number <= list.Count)
                {
                    return key;
                }
                else
                {
                    Console.WriteLine("选择无效");
                    return SelectMenusNumber(list);
                }
            }
            else
            {
                Console.WriteLine("选择无效");
                return SelectMenusNumber(list);
            }
        }

        internal ResizeMode GetResizeMode(string image_adjuster_key)
        {
            switch (image_adjuster_key)
            {
                case "1":
                    return ResizeMode.Crop; // 把图像的中间部分剪下来，使其正好适应新尺寸。
                case "2":
                    return ResizeMode.Pad; // 在图像周围填充空白，使图像适应新尺寸。
                case "3":
                    return ResizeMode.BoxPad;  // 保持图像原大小不变，周围填充空白使其适应新尺寸
                case "4":
                    return ResizeMode.Max;// 调整图像尺寸，使其最大程度地适应新尺寸的边界。
                case "5":
                    return ResizeMode.Min; //调整图像尺寸，使其至少能覆盖新尺寸的一边。
                case "6":
                    return ResizeMode.Stretch; // 直接拉伸或压缩图像，使其完全适应新尺寸。
                case "7":
                    return ResizeMode.Manual; // 手动指定图像的位置和大小，不进行自动调整。
                default:
                    throw new NotSupportedException($"不支持的图像显示类型");
            }
        }
        internal AnchorPositionMode GetAnchorPositionMode(string image_position_key)
        {
            switch (image_position_key)
            {
                case "1":
                    return AnchorPositionMode.Center; // 图像的中心点
                case "2":
                    return AnchorPositionMode.Top; // 图像的顶部
                case "3":
                    return AnchorPositionMode.Bottom;  // 图像的底部
                case "4":
                    return AnchorPositionMode.Left;// 图像的左侧
                case "5":
                    return AnchorPositionMode.Right; // 图像的右侧
                case "6":
                    return AnchorPositionMode.TopLeft; // 图像的左上角
                case "7":
                    return AnchorPositionMode.TopRight; // 图像的右上角
                case "8":
                    return AnchorPositionMode.BottomRight; //  图像的右下角
                case "9":
                    return AnchorPositionMode.BottomLeft; //  图像的左下角
                default:
                    throw new NotSupportedException($"不支持的图像显示类型");
            }
        }

        internal int InputIntValue()
        {

            string? value = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("未提供输入.");
                Console.Write("请重新输入:");
                return InputIntValue();
            }
            if (int.TryParse(value, out int intValue))
            {
                return intValue; // 返回解析后的整数值
            }
            else
            {
                Console.WriteLine($"{value} 不是一个有效的数值.");
                Console.Write("请重新输入:");
                return InputIntValue();
            }
        }
    }
}
