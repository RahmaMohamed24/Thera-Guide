using TheraGuide.Enums;

namespace TheraGuide.ViewModels
{
    public class TipViewModel
    {
        public TypeEnum Type { get; set; }  // "Tip" or "Exercise"
        public string Content { get; set; }
        public string CategoryName { get; set; }
    }

}
