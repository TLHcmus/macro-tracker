namespace MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;

public interface IDaoReceiver
{
    string GetFoods();

    void AddFood(string foodJson);

    void RemoveFood(string foodNameJson);

    string GetExercises();

    void AddExercise(string exerciseJson);

    void RemoveExercise(string exerciseNameJson);

    string GetGoal();

    void UpdateGoal(string goalJson);

    string GetUsers();

    string DoesUserMatchPassword(string userJson);

    string DoesUsernameExist(string usernameJson);

    void AddUser(string userJson);

    string GetLogs();

    void AddLog(string logJson);

    void DeleteLog(string logIdJson);

    void DeleteLogFood(string idDeleteJson);

    void DeleteLogExercise(string idDeleteJson);

    string GetLogWithPagination(string pageOffsetJson);

    string GetNLogWithPagination(string pageOffsetJson);

    void UpdateTotalCalories(string logIdJson);
}
