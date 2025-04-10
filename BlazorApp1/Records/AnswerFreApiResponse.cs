using English.Enums;

namespace English.Records
{
    public record AnswerFreApiResponse (int statusCode, string Message, Correctness Correctness)
    {
    }
}
