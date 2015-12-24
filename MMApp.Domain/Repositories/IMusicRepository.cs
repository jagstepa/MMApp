using System.Collections.Generic;

namespace MMApp.Domain.Repositories
{
    public interface IMusicRepository
    {
        List<IModelInterface> GetAll<T>() where T : IModelInterface;
        List<IModelInterface> GetAllForParent<T>(int id, string subType) where T : IModelInterface;
        List<IModelInterface> GetAllForText<T>(string searchText) where T : IModelInterface;
        IModelInterface Find<T>(int id) where T : IModelInterface;
        void Add<T>(T value) where T : IModelInterface;
        void Update<T>(T value) where T : IModelInterface;
        void Remove<T>(int id) where T : IModelInterface;
        bool CheckDelete<T>(int id) where T : IModelInterface;
        bool CheckDuplicate<T>(string name, string website) where T : IModelInterface;

    }
}
