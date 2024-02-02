namespace PoQuiz.Service
{
    public class QuestionService
    {
        private readonly ApplicationDbContext _context;
        private readonly Random _random;

        public QuestionService(ApplicationDbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public Question GenerateQuestion()
        {
            // Get a random record from the database
            var allStarGamesCount = _context.NbaAllStarGames.Count();
            var randomRecordIndex = _random.Next(allStarGamesCount);
            var randomRecord = _context.NbaAllStarGames.Skip(randomRecordIndex).Take(1).First();

            // Decide whether to create a true question or a false question
            var makeTrueQuestion = _random.Next(2) == 0;

            // If we're making a false question, randomize the year
            var yearForQuestion = makeTrueQuestion ? randomRecord.Year : GenerateRandomYear(randomRecord.Year);

            // Construct the question
            var question = new Question
            {
                Text = $"Did {randomRecord.Player} play in the NBA All-Star game in {yearForQuestion}?",
                Answer = makeTrueQuestion
            };

            return question;
        }

        private int GenerateRandomYear(int excludeYear)
        {
            int year;
            do
            {
                year = _random.Next(1951, DateTime.Now.Year + 1); // NBA All-Star games started in 1951
            } while (year == excludeYear);

            return year;
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public bool Answer { get; set; }
    }
}
