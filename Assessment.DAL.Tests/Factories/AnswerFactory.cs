using Assessment.DAL.Entities;

namespace Assessment.DAL.Tests.Factories
{
	public class AnswerFactory
	{
		#region Private Fields

		private bool _AnswerCorrect = true;
		private int _answerId = 1;
		private int _answerOwnerId1 = 2;
		private int _answerQuestionId = 1;
		private string _answerText = "This is a first answer in the list.";
		private int _aoaId = 5;
		private string _text = "This is a fist answer.";

		#endregion Private Fields

		#region Public Methods

		public Answer Build()
		{
			return new Answer()
			{
				Id = _answerId,
				Correct = _AnswerCorrect,
				Text = _answerText,
				QuestionId = _answerQuestionId
			};
		}

		public AnswerFactory WithAnswerCorrect(bool value)
		{
			_AnswerCorrect = value;
			return this;
		}

		public AnswerFactory WithAnswerId(int answerId)
		{
			_answerId = answerId;
			return this;
		}

		public AnswerFactory WithText(string text)
		{
			_answerText = text;
			return this;
		}

		#endregion Public Methods
	}
}