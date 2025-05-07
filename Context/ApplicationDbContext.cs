using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheraGuide.Entity;
using TheraGuide.Enums;

namespace TheraGuide.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)

        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Anxiety" },
                new Category { Id = 2, Name = "Stress" },
                new Category { Id = 3, Name = "Sleep Disorder" },
                new Category { Id = 4, Name = "Digital Overload" },
                new Category { Id = 5, Name = "Feeling of Sadness or Loneliness" },
                new Category { Id = 6, Name = "Lack of Motivation or Energy" }
            );

            modelBuilder.Entity<Tip>().HasData(
                // Digital Overload (CategoryId = 4)
                new Tip { Id = 1, Type = TypeEnum.Tip, Content = "Set screen time limits: Create specific limits for how much time you spend on devices daily.", CategoryId = 4 },
                new Tip { Id = 2, Type = TypeEnum.Tip, Content = "Schedule 'device-free' times: Dedicate specific periods in your day to disconnect completely from screens.", CategoryId = 4 },
                new Tip { Id = 3, Type = TypeEnum.Tip, Content = "Turn off non-essential notifications: Reduce interruptions that take your attention away from tasks.", CategoryId = 4 },
                new Tip { Id = 4, Type = TypeEnum.Exercise, Content = "Mindful Disconnect: Engage in an activity without screens (e.g., reading, walking).", CategoryId = 4 },
                new Tip { Id = 5, Type = TypeEnum.Exercise, Content = "Digital-Free Zones: Create spaces where devices are not allowed.", CategoryId = 4 },

                // Feeling of Sadness or Loneliness (CategoryId = 5)
                new Tip { Id = 6, Type = TypeEnum.Tip, Content = "Reach out to someone you trust: Share your feelings with a friend or therapist.", CategoryId = 5 },
                new Tip { Id = 7, Type = TypeEnum.Tip, Content = "Join a social group: Find communities with shared interests to build connections.", CategoryId = 5 },
                new Tip { Id = 8, Type = TypeEnum.Exercise, Content = "Emotional Expression: Write about your feelings in a journal.", CategoryId = 5 },
                new Tip { Id = 9, Type = TypeEnum.Exercise, Content = "Connect with a Friend: Schedule a call or meetup to talk.", CategoryId = 5 },

                // Lack of Motivation or Energy (CategoryId = 6)
                new Tip { Id = 10, Type = TypeEnum.Tip, Content = "Set small, achievable goals: Break tasks into smaller steps.", CategoryId = 6 },
                new Tip { Id = 11, Type = TypeEnum.Tip, Content = "Celebrate small wins: Reward yourself for completing tasks.", CategoryId = 6 },
                new Tip { Id = 12, Type = TypeEnum.Exercise, Content = "Morning Stretch: Start your day with gentle stretches.", CategoryId = 6 },
                new Tip { Id = 13, Type = TypeEnum.Exercise, Content = "Physical Activity: Walk or do yoga for 15 minutes.", CategoryId = 6 },

                // Sleep Disorder (CategoryId = 3)
                new Tip { Id = 14, Type = TypeEnum.Tip, Content = "Limit screen time before bed: Avoid devices 30 minutes before sleep.", CategoryId = 3 },
                new Tip { Id = 15, Type = TypeEnum.Exercise, Content = "Breathing Exercises: Practice deep breathing to calm your mind.", CategoryId = 3 },

                // Stress (CategoryId = 2)
                new Tip { Id = 16, Type = TypeEnum.Tip, Content = "Practice mindfulness: Meditate to stay present.", CategoryId = 2 },
                new Tip { Id = 17, Type = TypeEnum.Exercise, Content = "Guided Meditation: Listen to a calming meditation.", CategoryId = 2 },

                // Anxiety (CategoryId = 1)
                new Tip { Id = 18, Type = TypeEnum.Tip, Content = "Focus on breathing: Use deep breaths to reduce anxiety.", CategoryId = 1 },
                new Tip { Id = 19, Type = TypeEnum.Exercise, Content = "Body Scan: Release tension by focusing on each body part.", CategoryId = 1 }
            );





            // Example Question
            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 1, Content = "Do you often feel anxious even without a clear reason?", CategoryId = 1 },
                new Question { Id = 2, Content = "Do you struggle to stop thinking about negative things?", CategoryId = 1 },
                new Question { Id = 3, Content = "Do you feel stressed or anxious when thinking about the future?", CategoryId = 1 },
                new Question { Id = 4, Content = "Do you feel that you are constantly in a state of anxiety or stress?", CategoryId = 1 },
                new Question { Id = 5, Content = "Do you find it hard to enjoy things that used to bring you comfort?", CategoryId = 1 },
                new Question { Id = 6, Content = "Do you feel the need to avoid situations that may trigger your anxiety?", CategoryId = 1 },
                new Question { Id = 7, Content = "Do you tend to think of worst-case scenarios before things happen?", CategoryId = 1 },
                new Question { Id = 8, Content = "Do you expect the worst outcomes in most situations?", CategoryId = 1 },
                new Question { Id = 9, Content = "Does anxiety affect your ability to concentrate?", CategoryId = 1 },
                new Question { Id = 10, Content = "Do you find it hard to relax because of anxiety?", CategoryId = 1 },
                new Question { Id = 11, Content = "Do you struggle with decision-making due to anxiety about the outcomes?", CategoryId = 1 },
                new Question { Id = 12, Content = "Do you feel like you can't control your feelings of anxiety?", CategoryId = 1 },
                new Question { Id = 13, Content = "Does anxiety affect your daily life significantly?", CategoryId = 1 },
                new Question { Id = 14, Content = "Do you feel exhausted due to constant anxiety?", CategoryId = 1 },
                new Question { Id = 15, Content = "Do you have trouble sleeping because of anxiety?", CategoryId = 1 },



                new Question { Id = 16, Content = "Do you often feel too busy to take a break or relax?", CategoryId = 2 },
    new Question { Id = 17, Content = "Does stress affect your sleep quality?", CategoryId = 2 },
    new Question { Id = 18, Content = "Do you find it hard to stay calm in stressful situations?", CategoryId = 2 },
    new Question { Id = 19, Content = "Do you often feel tired due to stress?", CategoryId = 2 },
    new Question { Id = 20, Content = "Do you experience stress even when there is no clear reason for it?", CategoryId = 2 },
    new Question { Id = 21, Content = "Does stress affect your daily performance?", CategoryId = 2 },
    new Question { Id = 22, Content = "Do you feel pressured by deadlines or responsibilities?", CategoryId = 2 },
    new Question { Id = 23, Content = "Do you find it hard to manage difficult situations because of stress?", CategoryId = 2 },
    new Question { Id = 24, Content = "Do you notice changes in your appetite due to stress?", CategoryId = 2 },
    new Question { Id = 25, Content = "Do you feel that stress makes it difficult to control your reactions?", CategoryId = 2 },
    new Question { Id = 26, Content = "Do you have trouble concentrating due to stress?", CategoryId = 2 },
    new Question { Id = 27, Content = "Do you feel like you lack energy because of stress?", CategoryId = 2 },
    new Question { Id = 28, Content = "Does stress interfere with your social life?", CategoryId = 2 },
    new Question { Id = 29, Content = "Do you find yourself losing interest in activities you used to enjoy due to stress?", CategoryId = 2 },
    new Question { Id = 30, Content = "Do you feel like you can’t stop thinking about your problems because of stress?", CategoryId = 2 },

            #region Sleep Disorder Questions
    new Question { Id = 31, Content = "Do you find it hard to fall asleep even when you're tired?", CategoryId = 3 },
    new Question { Id = 32, Content = "Do you wake up frequently throughout the night?", CategoryId = 3 },
    new Question { Id = 33, Content = "Do you feel anxious or have disturbing thoughts when trying to sleep?", CategoryId = 3 },
    new Question { Id = 34, Content = "Do you feel like you’re not getting enough sleep?", CategoryId = 3 },
    new Question { Id = 35, Content = "Do you wake up feeling tired, even after sleeping?", CategoryId = 3 },
    new Question { Id = 36, Content = "Do you have trouble relaxing before bed?", CategoryId = 3 },
    new Question { Id = 37, Content = "Do you often find yourself thinking about your problems during the night?", CategoryId = 3 },
    new Question { Id = 38, Content = "Do you find your sleeping environment uncomfortable?", CategoryId = 3 },
    new Question { Id = 39, Content = "Is it difficult for you to fall asleep quickly?", CategoryId = 3 },
    new Question { Id = 40, Content = "Does poor sleep affect your daily activities?", CategoryId = 3 },
    new Question { Id = 41, Content = "Do you feel fatigued during the day due to sleep problems?", CategoryId = 3 },
    new Question { Id = 42, Content = "Do you use your phone or computer right before bed?", CategoryId = 3 },
    new Question { Id = 43, Content = "Do you find yourself needing caffeine during the day due to lack of sleep?", CategoryId = 3 },
    new Question { Id = 44, Content = "Does lack of sleep affect your performance during the day?", CategoryId = 3 },
    new Question { Id = 45, Content = "Do you have irregular sleep schedules?", CategoryId = 3 },
            #endregion

            #region Digital Overload Questions
    new Question { Id = 46, Content = "Do you often feel the urge to check your phone, even when it’s unnecessary?", CategoryId = 4 },
    new Question { Id = 47, Content = "Do you feel anxious or uneasy when you don’t have access to your devices?", CategoryId = 4 },
    new Question { Id = 48, Content = "Do you spend a significant amount of time on social media or online activities?", CategoryId = 4 },
    new Question { Id = 49, Content = "Do you find it hard to disconnect from screens for long periods?", CategoryId = 4 },
    new Question { Id = 50, Content = "Do you often use digital devices late at night, affecting your sleep?", CategoryId = 4 },
    new Question { Id = 51, Content = "Do you neglect personal relationships due to excessive screen time?", CategoryId = 4 },
    new Question { Id = 52, Content = "Do you feel overwhelmed by constant notifications or updates from apps?", CategoryId = 4 },
    new Question { Id = 53, Content = "Do you experience stress or frustration due to too much time spent online?", CategoryId = 4 },
    new Question { Id = 54, Content = "Do you struggle to focus or complete tasks because of digital distractions?", CategoryId = 4 },
    new Question { Id = 55, Content = "Do you often feel like you’ve wasted time online and regret it later?", CategoryId = 4 },
    new Question { Id = 56, Content = "Do you feel disconnected from reality because of your digital habits?", CategoryId = 4 },
    new Question { Id = 57, Content = "Do you find it hard to engage in offline activities because you’re addicted to screens?", CategoryId = 4 },
    new Question { Id = 58, Content = "Do you feel anxious or stressed when you don’t get a response to your messages or posts?", CategoryId = 4 },
    new Question { Id = 59, Content = "Do you feel like your digital habits are affecting your mental well-being?", CategoryId = 4 },
    new Question { Id = 60, Content = "Do you find it difficult to limit screen time during the day?", CategoryId = 4 },
    #endregion

    // Sadness or Loneliness Questions
    #region Feeling of Sadness or Loneliness
    new Question { Id = 61, Content = "Do you often feel disconnected from others, even when you’re around people?", CategoryId = 5 },
    new Question { Id = 62, Content = "Do you experience feelings of sadness without a clear reason?", CategoryId = 5 },
    new Question { Id = 63, Content = "Do you prefer to isolate yourself from others when feeling down?", CategoryId = 5 },
    new Question { Id = 64, Content = "Do you feel like you lack meaningful connections in your life?", CategoryId = 5 },
    new Question { Id = 65, Content = "Do you find it difficult to express your feelings to others?", CategoryId = 5 },
    new Question { Id = 66, Content = "Do you often feel like you’re the only one experiencing your emotions?", CategoryId = 5 },
    new Question { Id = 67, Content = "Do you struggle to find joy in activities you once enjoyed?", CategoryId = 5 },
    new Question { Id = 68, Content = "Do you find it hard to connect with new people or make new friends?", CategoryId = 5 },
    new Question { Id = 69, Content = "Do you feel like your social relationships are superficial or unfulfilling?", CategoryId = 5 },
    new Question { Id = 70, Content = "Do you often question your self-worth or feel like you’re not good enough?", CategoryId = 5 },
    new Question { Id = 71, Content = "Do you experience frequent mood swings, often feeling down without warning?", CategoryId = 5 },
    new Question { Id = 72, Content = "Do you feel like others don’t understand how you’re feeling?", CategoryId = 5 },
    new Question { Id = 73, Content = "Do you find it hard to share your thoughts and emotions with others?", CategoryId = 5 },
    new Question { Id = 74, Content = "Do you often feel lonely even in social situations?", CategoryId = 5 },
    new Question { Id = 75, Content = "Do you feel like you’re not receiving enough support from those around you?", CategoryId = 5 },
    #endregion

     new Question { Id = 76, Content = "Do you often feel too tired to start tasks or activities?", CategoryId = 6 },
    new Question { Id = 77, Content = "Do you struggle to find motivation to do things that used to excite you?", CategoryId = 6 },
    new Question { Id = 78, Content = "Do you feel like you have no energy to accomplish anything throughout the day?", CategoryId = 6 },
    new Question { Id = 79, Content = "Do you often procrastinate or put off tasks because you lack energy?", CategoryId = 6 },
    new Question { Id = 80, Content = "Do you feel unmotivated to make changes or improvements in your life?", CategoryId = 6 },
    new Question { Id = 81, Content = "Do you feel like you’ve lost interest in things you used to enjoy?", CategoryId = 6 },
    new Question { Id = 82, Content = "Do you experience feelings of exhaustion even after rest?", CategoryId = 6 },
    new Question { Id = 83, Content = "Do you often feel overwhelmed by the thought of starting a new task?", CategoryId = 6 },
    new Question { Id = 84, Content = "Do you have trouble setting goals or following through with them?", CategoryId = 6 },
    new Question { Id = 85, Content = "Do you find it difficult to focus on important tasks?", CategoryId = 6 },
    new Question { Id = 86, Content = "Do you feel like your energy is drained after social interactions or work?", CategoryId = 6 },
    new Question { Id = 87, Content = "Do you struggle to wake up and start your day feeling refreshed?", CategoryId = 6 },
    new Question { Id = 88, Content = "Do you feel like you’re stuck in a rut with no motivation to move forward?", CategoryId = 6 },
    new Question { Id = 89, Content = "Do you avoid activities that could help you feel more energized?", CategoryId = 6 },
    new Question { Id = 90, Content = "Do you feel like you’re running on empty without any passion for life?", CategoryId = 6 }

            );

            var answers = new List<Answer>();
            int answerId = 1; // Start ID counter

            // Define possible answer choices (same for all questions)
            var answerChoices = new[] { "Often", "Sometimes", "No", "Always" };

            // Generate answers for each question (e.g., 10 questions)
            for (int questionId = 1; questionId <= 90; questionId++)
            {
                foreach (var choice in answerChoices)
                {
                    answers.Add(new Answer
                    {
                        Id = answerId++,
                        Content = choice,
                        QuestionId = questionId
                    });
                }
            }

            // Seed the answers
            modelBuilder.Entity<Answer>().HasData(answers);
        }


        public DbSet<Notes> Notes { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserAnswers> UserAnswers { get; set; }
        public DbSet<Tip> Tips { get; set; }

    }
}
