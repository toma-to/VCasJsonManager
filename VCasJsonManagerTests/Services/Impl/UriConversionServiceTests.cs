//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VCasJsonManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VCasJsonManagerTests.Stubs;

namespace VCasJsonManager.Services.Impl.Tests
{
    [TestClass()]
    public class UriConversionServiceTests
    {
        UserSettingServiceStub userSetting;
        UriConversionService target;

        [TestInitialize()]
        public void Setup()
        {
            userSetting = new UserSettingServiceStub();
            target = new UriConversionService(userSetting);
        }

        [TestMethod()]
        public void ExtractNico3dIdTest_ID直接()
        {
            var result = target.ExtractNico3dId("12345");

            Assert.AreEqual(12345, result);
        }

        [TestMethod()]
        public void ExtractNico3dIdTest_立体URL()
        {
            var result = target.ExtractNico3dId("https://3d.nicovideo.jp/works/td40937?ref=");

            Assert.AreEqual(40937, result);
        }

        [TestMethod()]
        public void ExtractNico3dIdTest_不正URL()
        {
            var result = target.ExtractNico3dId("https://www.nicovideo.jp/watch/sm34191876");

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void ExtractNico3dIdTest_立体URL_NG()
        {
            var result = target.ExtractNico3dId("https://3d.nicovideo.jp/works/td409379999999999999999");

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void BuildNico3dUrlTest()
        {
            var result = target.BuildNico3dUrl(40937);

            Assert.AreEqual("https://3d.nicovideo.jp/works/td40937", result);
        }

        [TestMethod()]
        public void ExtractNicoVideoIdTest_ID直接()
        {
            var result = target.ExtractNicoVideoId("sm6");

            Assert.AreEqual("sm6", result);
        }

        [TestMethod()]
        public void ExtractNicoVideoIdTest_動画URL()
        {
            var result = target.ExtractNicoVideoId("https://www.nicovideo.jp/watch/sm34290012");

            Assert.AreEqual("sm34290012", result);
        }

        [TestMethod()]
        public void ExtractNicoVideoIdTest_URL不正()
        {
            var result = target.ExtractNicoVideoId("https://3d.nicovideo.jp/works/td40937");

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void BuildNicoViedoFormatTest()
        {
            var result = target.BuildNicoViedoUrl("sm34290012");

            Assert.AreEqual("https://www.nicovideo.jp/watch/sm34290012", result);
        }

        [TestMethod()]
        public void ExtractMylistIdTest_ID直接()
        {
            var result = target.ExtractMylistId("5419617");

            Assert.AreEqual(5419617, result);
        }

        [TestMethod()]
        public void ExtractMylistIdTest_公開マイリストURL()
        {
            var result = target.ExtractMylistId("https://www.nicovideo.jp/mylist/5419617?ref");

            Assert.AreEqual(5419617, result);
        }

        [TestMethod()]
        public void ExtractMylistIdTest_マイページマイリストURL()
        {
            var result = target.ExtractMylistId("https://www.nicovideo.jp/my/mylist/#/5419617");

            Assert.AreEqual(5419617, result);
        }

        [TestMethod()]
        public void ExtractMylistIdTest_不正URL()
        {
            var result = target.ExtractMylistId("https://www.nicovideo.jp/watch/sm34290012");

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void ExtractMylistIdTest_公開マイリストURL_NG()
        {
            var result = target.ExtractMylistId("https://www.nicovideo.jp/mylist/541961799999999");

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void ExtractNiconareIdTest_ID直接()
        {
            var result = target.ExtractNiconareId("3685");

            Assert.AreEqual(3685, result);
        }

        [TestMethod()]
        public void ExtractNiconareIdTest_ニコナレURL()
        {
            var result = target.ExtractNiconareId("https://niconare.nicovideo.jp/watch/kn3685");

            Assert.AreEqual(3685, result);
        }

        [TestMethod()]
        public void ExtractNiconareIdTest_不正URL()
        {
            var result = target.ExtractNiconareId("https://www.nicovideo.jp/watch/sm34290012");

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void ExtractNiconareIdTest_ニコナレURL_NG()
        {
            var result = target.ExtractNiconareId("https://niconare.nicovideo.jp/watch/kn3680000000000000005");

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void BuildNiconareUrlTest()
        {
            var result = target.BuildNiconareUrl(3685);

            Assert.AreEqual("https://niconare.nicovideo.jp/watch/kn3685", result);
        }

        [TestMethod()]
        public void BuildUriTest_GoogleDraive_変換有り()
        {
            userSetting.UserSettings.ConvertGoogleDriveUri = true;
            var result = target.BuildUri("https://drive.google.com/open?id=1fQC0vygVvYbaNa8ADBykcFNuDXWgWjGb");

            Assert.AreEqual(new Uri("https://drive.google.com/uc?export=view&id=1fQC0vygVvYbaNa8ADBykcFNuDXWgWjGb"), result);
        }

        [TestMethod()]
        public void BuildUriTest_GoogleDraive_変換無し()
        {
            userSetting.UserSettings.ConvertGoogleDriveUri = false;
            var result = target.BuildUri("https://drive.google.com/open?id=1fQC0vygVvYbaNa8ADBykcFNuDXWgWjGb");

            Assert.AreEqual(new Uri("https://drive.google.com/open?id=1fQC0vygVvYbaNa8ADBykcFNuDXWgWjGb"), result);
        }

        [TestMethod()]
        public void BuildUriTest_GoogleDraiv以外()
        {
            userSetting.UserSettings.ConvertGoogleDriveUri = true;
            var result = target.BuildUri("https://marshmallow-qa.com/system/images/a23a2bf5-b22f-4d1d-90c0-ba821989ec23.png");

            Assert.AreEqual(new Uri("https://marshmallow-qa.com/system/images/a23a2bf5-b22f-4d1d-90c0-ba821989ec23.png"), result);
        }
    }
}
