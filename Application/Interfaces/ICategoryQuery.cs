﻿using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICategoryQuery
    {
        Task<Category> GetCategoryById(int categoryId);
    }
}
