﻿using DataGrid.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataGrid.Contracts
{
    /// <summary>
    /// Интерфейс для работы с хранилищем туров.
    /// </summary>
    public interface ITourStorage
    {
        Task<IReadOnlyCollection<Tour>> GetAllAsync();

        Task<Tour> AddAsync(Tour tour);

        Task EditAsync(Tour tour);

        Task<bool> DeleteAsync(Guid id);
    }
}
