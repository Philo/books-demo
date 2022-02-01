using System.Linq.Expressions;
using Books.Service.Core.Entites;
using Books.Service.Core.Interfaces;

namespace Books.Service.Core.Options;

public class BookOrderBy<T> : IBookOrderBy
{
	private readonly Expression<Func<Book, T>> expression;
	
	public BookOrderBy(Expression<Func<Book, T>> expression)
	{
		this.expression = expression;
	}

	public dynamic Expression => this.expression;
}