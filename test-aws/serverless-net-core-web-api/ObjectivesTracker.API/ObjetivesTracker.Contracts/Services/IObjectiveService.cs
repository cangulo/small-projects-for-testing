using ObjetivesTracker.Contracts.Models;
using System.Collections.Generic;

namespace ObjetivesTracker.Contracts.Services
{
    public interface IObjectiveService
    {
        void CreateObjective(Objective obj);

        void DeleteObjectiveById(int Id);

        Objective GetObjectiveById(int Id);

        IEnumerable<Objective> GetObjectives();

        void UpdateObjective(Objective obj);
    }
}