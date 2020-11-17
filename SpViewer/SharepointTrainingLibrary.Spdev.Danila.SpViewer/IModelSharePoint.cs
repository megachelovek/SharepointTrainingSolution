// Copyright © iSys.Spdev 2018 All rights reserved.

namespace iSys.Spdev.Danila.Spviewer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Интерфейс сущности SharePoint
    /// </summary>
    public interface IModelSharePoint
    {
        string Name { get; }

        Type SpViewerType { get; }

        object SharePointEntity { get; }

        Dictionary<string, Type> CategoryDictionary { get; }
    }
}