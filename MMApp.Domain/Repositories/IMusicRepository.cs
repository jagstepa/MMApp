using System.Collections.Generic;
using System.Data;

namespace MMApp.Domain.Repositories
{
    public interface IMusicRepository
    {
        List<IModelInterface> GetAll<T>() where T : IModelInterface;
        List<IModelInterface> GetAllForParent<T>(int id, string subType) where T : IModelInterface;
        List<IModelInterface> GetAllForText<T>(string searchText) where T : IModelInterface;
        IModelInterface Find<T>(int id) where T : IModelInterface;
        void Add<T>(IModelInterface entity) where T : IModelInterface;
        void Update<T>(IModelInterface entity) where T : IModelInterface;
        void Remove<T>(IModelInterface entity) where T : IModelInterface;
        bool CheckDelete<T>(IModelInterface entity) where T : IModelInterface;
        bool CheckDuplicate<T>(IModelInterface entity) where T : IModelInterface;

    }
}
