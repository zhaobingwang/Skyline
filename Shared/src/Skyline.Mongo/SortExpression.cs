using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Skyline.Mongo
{
    internal class SortExpression<T> : ExpressionVisitor
    {
        internal List<SortDefinition<T>> SortDefinitionList = new List<SortDefinition<T>>();
        public static List<SortDefinition<T>> GetSortDefinitions(Expression<Func<Sort<T>, Sort<T>>> expression)
        {
            var sort = new SortExpression<T>();
            sort.Resolve(expression);
            return sort.SortDefinitionList;
        }

        private void Resolve(Expression<Func<Sort<T>, Sort<T>>> expression)
        {
            Visit(expression);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            SortDefinitionList.Add(new SortDefinitionBuilder<T>().Ascending(a => a));
            return node;
        }

        /// <summary>
        /// 访问对象初始化表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            var bingdings = node.Bindings;

            foreach (var item in bingdings)
            {
                var memberAssignment = (MemberAssignment)item;

                if (memberAssignment.Expression.NodeType == ExpressionType.MemberInit)
                {
                    SortDefinitionList.Add(new SortDefinitionBuilder<T>().Ascending(a => a));
                }
                else
                {
                    Visit(memberAssignment.Expression);
                }
            }
            return node;
        }
    }
}
