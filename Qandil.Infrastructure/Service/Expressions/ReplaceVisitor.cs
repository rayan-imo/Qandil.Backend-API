using System.Linq.Expressions;

namespace Qandil.Infrastructure.Service.Expressions
{
    public class ReplaceVisitor : ExpressionVisitor

    {

        private readonly Expression _old;
        private readonly Expression _new;
        public ReplaceVisitor(Expression old, Expression @new) { _old = old; _new = @new; }
        public override Expression Visit(Expression? node) => node == _old ? _new : base.Visit(node!)!;



       
    }
}
