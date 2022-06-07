﻿using SolutionFramework.EFcore.Operations;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Abstractions.IRepository
{
    public interface ICompanyRepopsitory : IOperationBase<Guid, string, Company>
    {
    }
}
