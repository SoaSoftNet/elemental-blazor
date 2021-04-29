﻿using Elemental.Documentation;
using System.Linq;
using Xunit;

namespace Elemental.Tests
{
    public class SampleParsingTests
    {
        private const string _testFilePath = "./Samples/Icons/Index.razor";
        private const int _testFileLineCount = 29;
        private const string _testTitle = "Test Component";
        private const string _testDescription = "Lorem ipsum dolor sit amet.";
        private const int _testHtmlLength = 9;
        private const int _testCodeLength = 14;
        private const int _testScssLength = 3;

        [Fact]
        public void CanReadFile()
        {
            var lines = ParsedFile.ReadLines(_testFilePath);
            
            Assert.NotNull(lines);
            Assert.Equal(_testFileLineCount, lines.Count());
        }

        [Fact]
        public void CanFindTitle()
        {
            var lines = ParsedFile.ReadLines(_testFilePath);
            var title = ParsedFile.ParseTitle(lines);
            
            Assert.Equal(_testTitle, title);
        }

        [Fact]
        public void CanFindDescription()
        {
            var lines = ParsedFile.ReadLines(_testFilePath);
            var description = ParsedFile.ParseDescription(lines);

            Assert.Equal(_testDescription, description);
        }

        [Fact]
        public void CanFindHtml()
        {
            var lines = ParsedFile.ReadLines(_testFilePath);
            var html = ParsedFile.ParseHtml(lines);

            Assert.Equal(_testHtmlLength, html.Count);
            Assert.Contains(@"<div class=""test-1"">one</div>", html.First());
            Assert.Contains(@"</div>", html.Last());
        }

        [Fact]
        public void CanFindCode()
        {
            var lines = ParsedFile.ReadLines(_testFilePath);
            var code = ParsedFile.ParseCode(lines);

            Assert.Equal(_testCodeLength, code.Count);
            Assert.Equal("@code {", code.First());
            Assert.Equal("}", code.Last());
        }

        [Fact]
        public void CanFindScss()
        {
            var shouldBeNull = ParsedFile.ReadScssLines("./garbagePath");
            Assert.Null(shouldBeNull);

            var lines = ParsedFile.ReadScssLines(_testFilePath);
            var code = ParsedFile.ParseScss(lines);
            Assert.Equal(_testScssLength, code.Count);
        }

        [Fact]
        public void CanSetupParsedFile()
        {
            var file = new ParsedFile(_testFilePath, string.Empty);

            Assert.Equal(_testTitle, file.Title);
            Assert.Equal(_testDescription, file.Description);
            Assert.Equal(_testHtmlLength, file.Html.Count);
            Assert.Equal(_testCodeLength, file.Code.Count);
            Assert.Equal(_testScssLength, file.Scss.Count);
        }
    }
}
