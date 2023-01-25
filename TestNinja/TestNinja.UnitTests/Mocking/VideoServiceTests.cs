using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using TestNinja.Mocking.Helpers;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class VideoServiceTests
    {
        private Mock<IFileReader> _mockFileReader;
        private Mock<IVideoRepository> _videoRepository;
        private VideoService _service;

        [SetUp]
        public void SetUp()
        {
            _mockFileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _service = new VideoService(_mockFileReader.Object, _videoRepository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
        {
            _mockFileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _service.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void ReadVideoTitle_NotEmptyFile_ReturnVideoTitle()
        {
            _mockFileReader.Setup(fr => fr.Read("video.txt")).Returns("{\"Id\":1,\"Title\":\"TestTitle\",\"IsProcessed\":true}");

            var result = _service.ReadVideoTitle();

            Assert.That(result, Does.Contain("testtitle").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            _videoRepository.Setup(x => x.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnAStringWithUnprocessedVideosIds()
        {
            _videoRepository.Setup(x => x.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video { Id = 1},
                new Video { Id = 2},
                new Video { Id = 3}
            });

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }

    }
}