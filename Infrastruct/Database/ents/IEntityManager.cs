/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.ents
{
    public interface IEntityManager
    {
        T Create<T>(T t) where T : IEntity;
        void Update<T>(T t) where T : IEntity;
        T Load<T>(int id) where T : IEntity;
        void Delete<T>(int id) where T : IEntity;
        List<T> LoadAll<T>() where T : IEntity;
    }
}
