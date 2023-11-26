﻿using BidMasterOnline.Application.Enums;
using BidMasterOnline.Domain.Entities;
using System.Linq.Expressions;

namespace BidMasterOnline.Application.Specifications
{
    public class SpecificationBuilder<T> where T : EntityBase
    {
        private readonly List<Expression<Func<T, bool>>> _filters = new();
        private Expression<Func<T, object>> _sortBy;
        private SortOrder _sortOrder;
        private bool _isPaginationEnabled;
        private int _take;
        private int _skip;

        public SpecificationBuilder<T> With(Expression<Func<T, bool>> predicate)
        {
            this._filters.Add(predicate);

            return this;
        }

        public SpecificationBuilder<T> OrderBy(Expression<Func<T, object>> sortBy, SortOrder sortOrder)
        {
            this._sortBy = sortBy;
            this._sortOrder = sortOrder;

            return this;
        }

        public SpecificationBuilder<T> WithPagination(int pageSize, int pageNumber)
        {
            this._isPaginationEnabled = true;

            this._take = pageSize;
            this._skip = (pageNumber - 1) * pageSize;

            return this;
        }

        public ISpecification<T> Build()
        {
            var specification = !this._isPaginationEnabled ?
                new Specification<T>(this.GetPredicate(), this._sortBy, this._sortOrder) :
                new Specification<T>(this._take, this._skip, this.GetPredicate(), this._sortBy, this._sortOrder);

            return specification;
        }

        private Expression<Func<T, bool>> GetPredicate()
        {
            if (!this._filters.Any())
                return null;

            Expression combinedExpression = this._filters
                .Select(e => (Expression)Expression.Invoke(e, e.Parameters.Cast<Expression>()))
                .Aggregate((acc, expr) => Expression.AndAlso(acc, expr));

            return Expression.Lambda<Func<T, bool>>(combinedExpression, this._filters[0].Parameters);
        }
    }
}
