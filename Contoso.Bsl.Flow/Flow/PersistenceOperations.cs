using LogicBuilder.Data;
using LogicBuilder.Domain;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using System.Collections.Generic;

namespace Contoso.Bsl.Flow
{
    public static class PersistenceOperations<TModel, TData> where TModel : BaseModel where TData : BaseData
    {
        public static void AddChange(IContextRepository repository, TModel entity) 
            => repository.AddChange<TModel, TData>(entity);

        public static void AddChanges(IContextRepository repository, ICollection<TModel> entities)
            => repository.AddChanges<TModel, TData>(entities);

        public static void AddGraphChange(IContextRepository repository, TModel entity)
            => repository.AddGraphChange<TModel, TData>(entity);

        public static void AddGraphChanges(IContextRepository repository, ICollection<TModel> entities)
            => repository.AddGraphChanges<TModel, TData>(entities);

        public static bool Save(IContextRepository repository, TModel entity)
            => repository.SaveAsync<TModel, TData>(entity).Result;

        public static bool Save(IContextRepository repository, ICollection<TModel> entities)
            => repository.SaveAsync<TModel, TData>(entities).Result;

        public static bool SaveGraph(IContextRepository repository, TModel entity)
            => repository.SaveGraphAsync<TModel, TData>(entity).Result;

        public static bool SaveGraphs(IContextRepository repository, ICollection<TModel> entities)
            => repository.SaveGraphsAsync<TModel, TData>(entities).Result;
    }
}
