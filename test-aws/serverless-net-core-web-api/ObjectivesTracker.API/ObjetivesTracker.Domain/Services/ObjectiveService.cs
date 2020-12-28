using ObjetivesTracker.Contracts.Models;
using ObjetivesTracker.Contracts.Services;
using ObjetivesTracker.Repositories.Objetives;
using System;
using System.Collections.Generic;

namespace ObjetivesTracker.Domain.Services
{
    public class ObjectiveService : IObjectiveService
    {
        private readonly IObjetivesRepository _objectiveRepository;

        public ObjectiveService(IObjetivesRepository objectiveRepository)
        {
            _objectiveRepository = objectiveRepository ?? throw new ArgumentNullException(nameof(objectiveRepository));
        }

        public void CreateObjective(Objective obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteObjectiveById(int Id)
        {
            throw new NotImplementedException();
        }

        public Objective GetObjectiveById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Objective> GetObjectives()
            => _objectiveRepository.GetAllObjetives();

        public void UpdateObjective(Objective obj)
        {
            throw new NotImplementedException();
        }
    }
}