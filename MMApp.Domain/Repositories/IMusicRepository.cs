using System.Collections.Generic;

namespace MMApp.Domain.Repositories
{
    public interface IMusicRepository
    {
        List<IModelInterface> GetAll<T>() where T : IModelInterface;
        List<IModelInterface> GetAllForParent<T>(int id, string subType) where T : IModelInterface;
        List<IModelInterface> GetAllForText<T>(string searchText) where T : IModelInterface;
        IModelInterface Find<T>(int id) where T : IModelInterface;
        void Add<T>(Dictionary<string, string> pars) where T : IModelInterface;
        void Update<T>(Dictionary<string, string> pars) where T : IModelInterface;
        void Remove<T>(int id) where T : IModelInterface;
        bool CheckDelete<T>(int id) where T : IModelInterface;
        bool CheckDuplicate<T>(Dictionary<string, string> paramList) where T : IModelInterface;

    }
}
