using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NovaLox
{
    public class AstPrinter : IVisitor<String>
    {
        public string Print(Expr expr)
        {
            return expr.Accept(this);
        }

        #region Helpers

        private string Parenthesize(string name, params Expr[] exprs)
        {
            var _exprs = String.Join(" ", exprs.Select(expr => expr.Accept(this)));
            return string.Format("({0} {1})", name, _exprs);
        }

        #endregion

        #region IVisitor Methods

        public string VisitBinaryExpr(Binary expr)
        {
            return Parenthesize(expr.Operator.Lexeme, expr.Left, expr.Right);
        }

        public string VisitGroupingExpr(Grouping expr)
        {
            return Parenthesize("group", expr.Expression);
        }

        public string VisitLiteralExpr(Literal expr)
        {
            if (expr.Value == null)
                return "nil";

            return expr.Value.ToString();
        }

        public string VisitUnaryExpr(Unary expr)
        {
            return Parenthesize(expr.Operator.Lexeme, expr.Right);
        }

        #endregion
    }
}
