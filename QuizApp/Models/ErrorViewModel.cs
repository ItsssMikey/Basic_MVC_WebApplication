namespace QuizApp.Models;

// ViewModel brukt til å vise feildetaljer på feilsiden.
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
