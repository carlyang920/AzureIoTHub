# Samples

### Initializing
```csharp
IIotHubService _service = new IotHubService(
    new IoTHubConfig()
    {
        ConnectionString = iotHubConnectionString,
        DeviceRetryTimes = 10,
        OperationTimeoutSeconds = 600,
        RetryWaitSeconds = 30,
        Encoding = Encoding.UTF8
    },
    OnRetryError
);

void OnRetryError(int retryCount, Exception e)
{
    //Custom process
}
```

Note:
1. 統一使用IoTHubConfig帶入相關設定。
2. 設定相關說明可參考IoTHubConfig的各屬性描述。
3. DeviceRetryTimes是指總共要Retry幾次，如果設定DeviceRetryTimes = 0則不會Retry。
4. OperationTimeoutSeconds是指該次送Message的Time out秒數。
5. RetryWaitSeconds是指兩次Retry間的等待時間。
    <font color="red">這個等待時間是從1~RetryWaitSeconds、呈指數方式、逐次遞增，詳細可參考
    [Github - IoT Hub SDK](https://github.com/Azure/azure-iot-sdk-csharp/issues/355)</font>

	參考圖片如下:  
	![](https://user-images.githubusercontent.com/8635911/35535315-ffda8316-04f7-11e8-89b5-196bfbbd4fd7.png)
6. Encoding是指資料送出給IoT Hub時的編碼方式，預設是UTF8，可依不同情境自行指定。
7. <font color="red">請特別注意，如果DeviceRetryTimes次數的所需時間、大於OperationTimeoutSeconds整個操作的時間，那整個操作會因為OperationTimeoutSeconds設定、先發生Timeout而結束工作，此時Retry並不會繼續完成所有次數。</font>
8. <font color="red">OnRetryError是用在每次的Retry失敗後、CallBack外部Function，讓外部可以知道每次失敗的Exception資訊。</font>

### Send message with JSON array string
```csharp
_service.SendEventAsync(
    JsonConvert.SerializeObject(dataList, Formatting.None)
).Wait();
```

### Send message with generic List<T>
```csharp
_service.SendEventAsync(dataList).Wait();
```