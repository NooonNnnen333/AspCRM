namespace CRMSolution.Application;

public class QuestionSeeders : ISeeder
{
    private readonly QuestionSeeders _questionSeeders;

    public QuestionSeeders(QuestionSeeders _questionSeeders)
    {
        _questionSeeders = _questionSeeders;
    }

    public Task SeedAsync()
    {
        throw new NotImplementedException();
    }
}