using System.Timers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace PoQuiz.Service
{
    public class GameService
    {
        private readonly QuestionService _questionService;
        private const int TotalQuestions = 20;

        public int Score { get; private set; }
        public int QuestionNumber { get; private set; }
        public Question CurrentQuestion { get; private set; }
        public bool GameOver { get; private set; }

        public event Action OnQuestionUpdated;
        public event Action OnGameOver;

        public GameService(QuestionService questionService)
        {
            _questionService = questionService;
        }

        public void StartGame()
        {
            Score = 0;
            QuestionNumber = 0;
            GameOver = false;
            GenerateQuestion();
        }

        public void AnswerQuestion(bool answer)
        {
            if (CurrentQuestion.Answer == answer)
            {
                Score++;
            }

            if (QuestionNumber >= TotalQuestions)
            {
                EndGame();
            }
            else
            {
                GenerateQuestion();
            }
        }

        private void GenerateQuestion()
        {
            CurrentQuestion = _questionService.GenerateQuestion();
            QuestionNumber++;
            OnQuestionUpdated?.Invoke();
        }

        private void EndGame()
        {
            GameOver = true;
            OnGameOver?.Invoke();
        }
    }
}
