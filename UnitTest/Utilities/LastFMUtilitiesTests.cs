using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLog.LastFM;

namespace MusicLog.Tests.Utilities
{
    [TestClass]
    public class LastFMUtilitiesTests
    {
        [TestMethod]
        public void GetUserTracksTest()
        {
            List<LastFMAPIClient.Track> tracks = LastFMApi.GetUserTracks("eriejar", "Caravan Palace");
            Assert.IsNotNull(tracks[0]);
        }

    }
}
