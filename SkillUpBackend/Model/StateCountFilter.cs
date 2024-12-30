namespace SkillUpBackend.Model
{
    public class StateCountFilter
    {
            public int OpenCount { get; set; }
            public int InProgressCount { get; set; }
            public int CompletedCount { get; set; }
            public int ReviewCount { get; set; }
            public List<UserSubtopic> UnassignedTrainees { get; set; }
            public List<UserSubtopic> InProgressTrainees { get; set; }
            public List<UserSubtopic> CompletedTrainees { get; set; }
            public List <UserSubtopic> ReviewTrainees  { get; set; }

    }
}
