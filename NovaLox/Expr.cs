
using System;

namespace NovaLox
{

    public abstract class Expr {
        public abstract R Accept<R>(IVisitor<R> visitor);
    }


    public interface IVisitor<R> {
        R VisitBinaryExpr(Binary expr);
        R VisitGroupingExpr(Grouping expr);
        R VisitLiteralExpr(Literal expr);
        R VisitUnaryExpr(Unary expr);
    }


    public class Binary
    {
        public readonly Expr Left;
        public readonly Token Operator;
        public readonly Expr Right;

        public Binary(Expr _left, Token _operator, Expr _right)
        {
            this.Left = _left;
            this.Operator = _operator;
            this.Right = _right;
        }

        public R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitBinaryExpr(this);
        }
    }

    public class Grouping
    {
        public readonly Expr Expression;

        public Grouping(Expr _expression)
        {
            this.Expression = _expression;
        }

        public R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitGroupingExpr(this);
        }
    }

    public class Literal
    {
        public readonly Object Value;

        public Literal(Object _value)
        {
            this.Value = _value;
        }

        public R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitLiteralExpr(this);
        }
    }

    public class Unary
    {
        public readonly Token Operator;
        public readonly Expr Right;

        public Unary(Token _operator, Expr _right)
        {
            this.Operator = _operator;
            this.Right = _right;
        }

        public R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitUnaryExpr(this);
        }
    }
}