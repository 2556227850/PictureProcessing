

namespace PictureProcessing 
{
    internal class Menus : ImageTool
    {
        List<string> menusList = ["一键修改图片类型", "一键修改图片宽高"];
        List<string> imageTypeList = ["jpg/jpeg", "png","webp"];


        public Menus()
        {
            
              ShowMenusList();

        }

        private void ShowMenusList()
        {
            string? menus_key = SelectMenusNumber(menusList);
            if (menus_key != null)
            {
                switch (menus_key)
                {
                    case "1":
                        Console.WriteLine("正在修改图片类型");

                        string? image_type_key = SelectMenusNumber(imageTypeList);
                        if (image_type_key != null)
                        {
                            UpdateImageType(image_type_key);
                        }
                        break;
                    case "2":
                        Console.WriteLine("正在修改图片类型");
                            UpdateImageSize();
                        break;
                    default:
                        Console.WriteLine("选择无效");
                        ShowMenusList();
                        break;
                }
            }


        }

    }
}

