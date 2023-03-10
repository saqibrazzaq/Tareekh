﻿using data.Repository.Interfaces;
using dto.dtos;
using dto.Paging;
using entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository
{
    public class LanguageRepository : RepositoryBase<Language>, ILanguageRepository
    {
        public LanguageRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Language> Search(LanguageReqSearch dto, bool trackChanges)
        {
            var entities = FindAll(trackChanges)
                .Search(dto)
                .Sort(dto.OrderBy)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
            var count = FindAll(trackChanges)
                .Search(dto)
                .Count();
            return new PagedList<Language>(entities, count,
                dto.PageNumber, dto.PageSize);
        }
    }
}
