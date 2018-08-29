using MMApp.Domain.Models;
using System.Collections.Generic;

namespace MMApp.Domain.Repositories
{
    public interface IMusicRepository
    {
        List<IModelInterface> GetAll<T>() where T : IModelInterface;
        List<IModelInterface> GetAllForParent<T>(int id, string subType) where T : IModelInterface;
        List<IModelInterface> GetAllForText<T>(string searchText) where T : IModelInterface;
        IModelInterface Find<T>(int id) where T : IModelInterface;
        int Add<T>(IModelInterface entity) where T : IModelInterface;
        int AddRelationship(Relationship relation);
        void Update<T>(IModelInterface entity) where T : IModelInterface;
        void Remove<T>(IModelInterface entity) where T : IModelInterface;
        bool CheckDelete<T>(IModelInterface entity) where T : IModelInterface;
        bool CheckDuplicate<T>(IModelInterface entity) where T : IModelInterface;
        int GetEntityType<T>() where T : IModelInterface;
        int GetEntityRelationType<T>() where T : IModelInterface;
        List<IModelInterface> GetEntityRelationList<T>(int entityId, int entiyTypeId, int entiyRelationTypeId) where T : IModelInterface;
    }
}
