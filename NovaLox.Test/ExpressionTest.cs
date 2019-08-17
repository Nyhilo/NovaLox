using System;
using Xunit;
using NovaLox;

namespace NovaLox.Test
{
    public class ExpressionTest
    {
        [Fact]
        public void AstPrinter_Print_Binary()
        {
            // Arrange
            Expr expression = new Binary(
                                    new Unary(
                                        new Token(TokenType.MINUS, "-", null, 1),
                                        new Literal(123)),
                                    new Token(TokenType.STAR, "*", null, 1),
                                    new Grouping(
                                        new Literal(45.67)));
            // Act
            var actual = new AstPrinter().Print(expression);
            var expected = "(* (- 123) (group 45.67))";

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
