namespace Assessment.DAL.Helper
{
    public class EnumHelper
    {
        public enum Difficulty
        {
            Junior = 1,
            medior = 2,
            Senior = 3
        }

        public enum CandidateQuestionStatus
        {
            ToStart = 0,
            CurrentlyAnswering = 1,
            AnsweredByTimer = 2,
            Answered = 3,
            ForcedByServer = 4,
            ForcedByTimer = 5,
            AnsweredByTimerAndDirty = 6,
            AnsweredAndDirty = 7
        };

        public enum CandidateQuestionaryStatus
        {
            ToStart = 0,
            OnGoing = 1,
            CompletedWithoutIncident = 2,
            CompletedWithIncident = 3,
            CompletedWithHiddenIncidents = 4
        };
    }
}
