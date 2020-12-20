namespace VKMonitor.Model
{
    public enum RelativeType
    {
        Child, Sibling, Parent, Grandparent, Grandchild
    }

    public class Relative
    {
        public RelativeType Type { get; set; }

        public string TypeStr
        {
            get
            {
                switch (Type)
                {
                    case RelativeType.Child:
                        return "Сын/Дочь";
                    case RelativeType.Sibling:
                        return "Брат/Сестра";
                    case RelativeType.Parent:
                        return "Отец/Мать";
                    case RelativeType.Grandparent:
                        return "Дедушка/Бабушка";
                    case RelativeType.Grandchild:
                        return "Внук/Внучка";
                    default:
                        return string.Empty;
                }
            }
        }

        public long? Id { get; set; }

        public string Name { get; set; }

        public Relative(VkNet.Model.Relative relative)
        {
            if (relative == null)
                return;

            Id = relative.Id;
            Name = relative.Name;

            switch (relative.Type.ToString())
            {
                case "Child":
                    Type = RelativeType.Child;
                    break;
                case "Sibling":
                    Type = RelativeType.Sibling;
                    break;
                case "Parent":
                    Type = RelativeType.Parent;
                    break;
                case "Grandparent":
                    Type = RelativeType.Grandparent;
                    break;
                case "Grandchild":
                    Type = RelativeType.Grandchild;
                    break;
            }
        }
    }
}
