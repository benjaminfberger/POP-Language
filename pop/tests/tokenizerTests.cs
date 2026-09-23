using src.parsing;
using NUnit.Framework;
using System.Runtime.Versioning;

namespace tests
{
    [TestFixture]
    public class tokenizerTests
    {
        tokenizer tokenizer;
        [Test]
        public void tokenizeShouldTreatCapitalizedKeywordsAsIndentities()
        {
            string source = "let SUDO run";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.let, "let", 0),
                new token(tokenType.identifier, "SUDO", 0),
                new token(tokenType.run, "run", 0),
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldReturnEofAsLastToken()
        {
            string source = "init";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.init, "init", 0),
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldReturnOnlyEofForEmptyString()
        {
            string source = string.Empty;
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldNotDistinguishKeywordsWithinWords()
        {
            string source = "sudoinit";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.identifier, "sudoinit", 0),
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldHandleKeywordsAdjacentToSymbols()
        {
            string source = "warn{halt genericIdentity}";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.warn, "warn", 0),
                new token(tokenType.leftBrace, "{", 0),
                new token(tokenType.halt, "halt", 0),
                new token(tokenType.identifier, "genericIdentity", 0),
                new token(tokenType.rightBrace, "}", 0),
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldIgnoreWhitespaceBetweenTokens()
        {
            string source = "warn     halt\tinit  \ngenericIdentity";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.warn, "warn", 0),
                new token(tokenType.halt, "halt", 0),
                new token(tokenType.init, "init", 0),
                new token(tokenType.identifier, "genericIdentity", 1),
                new token(tokenType.eof, "eof", 1)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldTreatEofAsWhitespace()
        {
            string source = "warn eof halt";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.warn, "warn", 0),
                new token(tokenType.halt, "halt", 0),
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldNotSplitStringLiteralsIntoMultipleTokens()
        {
            string source = "init\"sudo genericIdentity\"halt program";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.init, "init", 0),
                new token(tokenType.stringLiteral, "\"sudo genericIdentity\"", 0),
                new token(tokenType.halt, "halt", 0),
                new token(tokenType.program, "program", 0),
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldFailOnUnclosedStringLiteral()
        {
            string source = "init\"halt";
            tokenizer = new tokenizer(source);
            Assert.Throws<ArgumentOutOfRangeException>(() => tokenizer.tokenize());
        }
        [Test]
        public void tokenizeShouldTreatEmptyStringLiteralsAsStringLiterals()
        {
            string source = "init\"\"halt";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.init, "init", 0),
                new token(tokenType.stringLiteral, "\"\"", 0),
                new token(tokenType.halt, "halt", 0),
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldNotFailOnStringLiteralAtEndOfSource()
        {
            string source = "\"Hello, World\"";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.stringLiteral, "\"Hello, World\"", 0),
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizerShouldHandleEscapeSequencesWithinStringLiterals()
        {
            string source = "\"Hello,\\ \t World\"";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.stringLiteral, "\"Hello,\\ \t World\"", 0),
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldDiscardComments()
        {
            string source = "init//this is a comment\nhalt";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.init, "init", 0),
                new token(tokenType.halt, "halt", 1),
                new token(tokenType.eof, "eof", 1)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldNotReadDivideAsComment()
        {
            string source = "//this is a comment\n halt /";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.halt, "halt", 1),
                new token(tokenType.divide, "/", 1),
                new token(tokenType.eof, "eof", 1)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldNotTreatNumberLiteralsAsStringLiterals()
        {
            string source = "1234\"Hello, World\"";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.numberLiteral, "1234", 0),
                new token(tokenType.stringLiteral, "\"Hello, World\"", 0),
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void tokenizeShouldNotTreatNumberLiteralsAsIdentifiers()
        {
            string source = "1234 id3nt1fier";
            tokenizer = new tokenizer(source);
            List<token> actual = tokenizer.tokenize();
            List<token> expected = new List<token>
            {
                new token(tokenType.numberLiteral, "1234", 0),
                new token(tokenType.identifier, "id3nt1fier", 0),
                new token(tokenType.eof, "eof", 0)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
