

namespace PictureProcessing 
{
    internal class Menus : ImageTool
    {
        List<string> menusList = ["一键修改图片类型"];
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
                        Console.WriteLine("正在执行修改图片类型");

                        string? image_type_key = SelectMenusNumber(imageTypeList);
                        if (image_type_key != null)
                        {
                            UpdateImageType(image_type_key);
                        }
                        break;
                    default:
                        Console.WriteLine("选择无效");
                        ShowMenusList();
                        break;
                }
            }


        }

        private string SelectMenusNumber(List<string> list)
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


    }
}

