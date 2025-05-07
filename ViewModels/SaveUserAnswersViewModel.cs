namespace TheraGuide.ViewModels
{
    public class SaveUserAnswerViewModel
    {
        public string UserId { get; set; }
        public long AnswerId { get; set; }
    }
    public class SaveUserAnswersViewModel
    {
        public string UserId { get; set; }
        public List<long> AnswerIds { get; set; }
    }
}
