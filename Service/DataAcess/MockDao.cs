using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroTracker.Model;

namespace MacroTracker.Service.DataAcess;
public class MockDao : IDao
{
    public ObservableCollection<ExerciseInfo> Exercises = new ObservableCollection<ExerciseInfo>
    {
        new ExerciseInfo
        {
            IconFileName = "basketball.png",
            Name = "Basketball",
        },
        new ExerciseInfo
        {
            IconFileName = "climbing.png",
            Name = "Climbing",
        },
        new ExerciseInfo
        {
            IconFileName = "martialarts.png",
            Name = "Martial Arts",
        },
        new ExerciseInfo
        {
            IconFileName = "running.png",
            Name = "Running",
        },
        new ExerciseInfo
        {
            IconFileName = "swimming.png",
            Name = "Swimming",
        },
        new ExerciseInfo
        {
            IconFileName = "pickleball.png",
            Name = "Pickle Ball",
        },
        new ExerciseInfo
        {
            IconFileName = "tennis.png",
            Name = "Tennis",
        },
        new ExerciseInfo
        {
            IconFileName = "volleyball.png",
            Name = "Volleyball",
        },
        new ExerciseInfo
        {
            IconFileName = "walking.png",
            Name = "Walking",
        },
        new ExerciseInfo
        {
            IconFileName = "weightlifting.png",
            Name = "Weight Lifting",
        },
        new ExerciseInfo
        {
            IconFileName = "yoga.png",
            Name = "Yoga",
        },
        new ExerciseInfo
        {
            IconFileName = "pilates.png",
            Name = "Pilates",
        },
        new ExerciseInfo
        {
            IconFileName = "baseball.png",
            Name = "Baseball",
        },
    };

    public ObservableCollection<ExerciseInfo> GetExercises()
    {
        return Exercises;
    }
    public List<Food> GetFoods() => throw new NotImplementedException();
}
