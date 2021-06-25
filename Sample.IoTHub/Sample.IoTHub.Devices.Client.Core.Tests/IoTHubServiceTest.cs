using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sample.IoTHub.Devices.Client.Core.Configs;
using Sample.IoTHub.Devices.Client.Core.Interfaces;
using Sample.IoTHub.Devices.Client.Core.Services;

namespace Sample.IoTHub.Devices.Client.Core.Tests
{
    [TestClass]
    public class IoTHubServiceTest
    {
        private readonly IIotHubService _service;

        public IoTHubServiceTest()
        {
            const string iotHubConnectionString = @"[IoT Hub Connection]";

            _service = new IotHubService(
                new IoTHubConfig()
                {
                    ConnectionString = iotHubConnectionString,
                    DeviceRetryTimes = 10,
                    OperationTimeoutSeconds = 110,
                    RetryWaitSeconds = 10,
                    Encoding = Encoding.ASCII
                },
                OnRetryError
            );

            void OnRetryError(int retryCount, Exception e)
            {
                Trace.WriteLine(string.Empty);
                Trace.WriteLine($"CurrentTime: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                Trace.WriteLine($"RetryCount: {retryCount}");
                Trace.WriteLine($"Exception: {e}");
            }
        }

        [TestMethod]
        [Description("測試 SendEventAsync with List<T>")]
        public void SendEventAsyncWithListTest()
        {
            try
            {
                var dataList = new List<dynamic>();

                for (var i = 0; i < 1; i++)
                {
                    dataList.Add(
                        JsonConvert.DeserializeObject<dynamic>(
                            @"testdata"
                            )
                        );
                }

                var startTime = DateTime.Now;
                _service.SendEventAsync(dataList).Wait();
                Trace.WriteLine($"{(DateTime.Now - startTime).TotalMilliseconds / 1000}s");

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        [Description("測試 SendEventAsync with JSON array string")]
        public void SendEventAsyncWithJsonTest()
        {
            try
            {
                var dataList = new List<JObject>();

                for (var i = 0; i < 1; i++)
                {
                    dataList.Add(
                        JsonConvert.DeserializeObject<JObject>(
                            @"testdata"
                            )
                        );
                }

                var startTime = DateTime.Now;
                _service.SendEventAsync(
                    JsonConvert.SerializeObject(dataList, Formatting.None)
                ).Wait();
                Trace.WriteLine($"{(DateTime.Now - startTime).TotalMilliseconds / 1000}s");

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                Assert.IsTrue(false);
            }
        }
    }
}
