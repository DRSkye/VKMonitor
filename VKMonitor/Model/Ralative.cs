namespace VKMonitor.Model
{
    public enum RelativeType
    {
        Child, Sibling, Parent, Grandparent, Grandchild
    }

    public class Relative
    {
        public RelativeType Type { get; set; }

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
