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
1. �Τ@�ϥ�IoTHubConfig�a�J�����]�w�C
2. �]�w���������i�Ѧ�IoTHubConfig���U�ݩʴy�z�C
3. DeviceRetryTimes�O���`�@�nRetry�X���A�p�G�]�wDeviceRetryTimes = 0�h���|Retry�C
4. OperationTimeoutSeconds�O���Ӧ��eMessage��Time out��ơC
5. RetryWaitSeconds�O���⦸Retry�������ݮɶ��C
    <font color="red">�o�ӵ��ݮɶ��O�q1~RetryWaitSeconds�B�e���Ƥ覡�B�v�����W�A�Բӥi�Ѧ�
    [Github - IoT Hub SDK](https://github.com/Azure/azure-iot-sdk-csharp/issues/355)</font>

	�ѦҹϤ��p�U:  
	![](https://user-images.githubusercontent.com/8635911/35535315-ffda8316-04f7-11e8-89b5-196bfbbd4fd7.png)
6. Encoding�O����ưe�X��IoT Hub�ɪ��s�X�覡�A�w�]�OUTF8�A�i�̤��P���Ҧۦ���w�C
7. <font color="red">�ЯS�O�`�N�A�p�GDeviceRetryTimes���ƪ��һݮɶ��B�j��OperationTimeoutSeconds��Ӿާ@���ɶ��A����Ӿާ@�|�]��OperationTimeoutSeconds�]�w�B���o��Timeout�ӵ����u�@�A����Retry�ä��|�~�򧹦��Ҧ����ơC</font>
8. <font color="red">OnRetryError�O�Φb�C����Retry���ѫ�BCallBack�~��Function�A���~���i�H���D�C�����Ѫ�Exception��T�C</font>

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