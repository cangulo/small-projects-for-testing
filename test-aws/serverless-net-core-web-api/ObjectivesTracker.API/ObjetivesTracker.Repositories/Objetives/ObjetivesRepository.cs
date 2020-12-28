using ObjetivesTracker.Contracts.Models;
using System;
using System.Collections.Generic;

namespace ObjetivesTracker.Repositories.Objetives
{
    public interface IObjetivesRepository
    {
        Objective GetObjectiveById(int ObjectiveId);
        IEnumerable<Objective> GetAllObjetives();
        Objective UpdateObjetive(Objective objetive);
        void DeleteById(int ObjectiveId);
    }
    public class ObjetivesRepository : IObjetivesRepository
    {
        public void DeleteById(int ObjectiveId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Objective> GetAllObjetives()
        {
            return new Objective[] {
                new Objective
                {
                    ObjectiveId = 1,
                    Title = "Go to the gym five time per week",
                    Description = "Go to the gym five time per week",
                    DueDate = DateTime.UtcNow.AddDays(10),
                    Tracker = null
                },
                new Objective
                {
                    ObjectiveId = 2,
                    Title = "Read 30m daily",
                    Description = "Read 30m daily",
                    DueDate = DateTime.UtcNow.AddDays(10),
                    Tracker = null
                }};
        }

        public Objective GetObjectiveById(int ObjectiveId)
        {
            throw new NotImplementedException();
        }

        public Objective UpdateObjetive(Objective objetive)
        {
            throw new NotImplementedException();
        }
    }
}
